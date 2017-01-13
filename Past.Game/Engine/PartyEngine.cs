using System;
using Past.Game.Network;
using Past.Protocol;
using Past.Protocol.Messages;
using System.Collections.Generic;
using Past.Common.Utils;

namespace Past.Game.Engine
{
    public class PartyEngine
    {
        private readonly int _id;
        public int Id { get; set; }
        public GameClient Leader { get; set; }
        private readonly List<GameClient> _guests;
        private readonly List<GameClient> _members;

        public PartyEngine(GameClient client, GameClient target)
        {
            Id = _id++;
            Leader = client;
            _members = new List<GameClient>();
            _guests = new List<GameClient>();
            InviteClient(client, target);
            SendPartyJoinMessage(client);
        }

        public void Send(NetworkMessage message)
        {
            _members.ForEach(client => client.Send(message));
        }
        
        public void SendPartyJoinMessage(GameClient client)
        {
            _members.Add(client);
            client.Send(new PartyJoinMessage(Leader.Character.Id, _members.ConvertAll(member => member.Character.GetPartyMemberInformations).ToArray()));
        }

        public void SendPartyChatServerMessage(string content, int senderId, string senderName)
        {
            Send(new ChatServerMessage(4, content, Functions.ReturnUnixTimeStamp(DateTime.Now), "", senderId, senderName));
        }

        public void InviteClient(GameClient client, GameClient target)
        {
            lock (_guests)
            {
                if (!_guests.Contains(target))
                {
                    _guests.Add(target);
                    target.Character.Party = this;
                    target.Send(new PartyInvitationMessage(client.Character.Id, client.Character.Name, target.Character.Id, target.Character.Name));
                }
            }
        }

        public void AcceptInvitation(GameClient client)
        {
            if (client.Character.Party != null)
            {
                Leave(client);
                RemoveGuestClient(client);
                SendPartyJoinMessage(client);
                Send(new PartyUpdateMessage(client.Character.GetPartyMemberInformations));
            }
            else
            {
                RemoveGuestClient(client);
                SendPartyJoinMessage(client);
                Send(new PartyUpdateMessage(client.Character.GetPartyMemberInformations));
            }
        }

        public void RefuseInvitation(GameClient client)
        {
            RemoveGuestClient(client);
            if (_members.Count <= 1)
            {
                Disband();
            }
        }

        public void RemoveMember(GameClient client)
        {
            lock (_members)
            {
                if (_members.Contains(client))
                {
                    _members.Remove(client);
                    if (_members.Count <= 1)
                    {
                        Disband();
                    }
                    client.Send(new PartyLeaveMessage());
                    Send(new PartyMemberRemoveMessage(client.Character.Id));
                    client.Character.Party = null;
                }
            }
        }

        public void RemoveGuestClient(GameClient client)
        {
            lock (_guests)
            {
                if (_guests.Contains(client))
                {
                    _guests.Remove(client);
                }
             }
        }

        public void Leave(GameClient client)
        {
            if (client.Character.Party.Leader == client)
            {
                client.Character.Party.Disband();
            }
            client.Character.Party.RemoveMember(client);
        }

        public void Disband()
        {
            _members.ForEach(client => client.Send(new PartyLeaveMessage()));
            _members.ForEach(client => client.Character.Party = null);
        }
    }
}
