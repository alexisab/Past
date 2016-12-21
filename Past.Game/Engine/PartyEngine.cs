using System;
using Past.Game.Network;
using Past.Protocol;
using Past.Protocol.Messages;
using Past.Protocol.Types;
using System.Collections.Generic;
using Past.Common.Utils;

namespace Past.Game.Engine
{
    public class PartyEngine
    {
        private int id = 0;
        public int Id { get; set; }
        public GameClient Leader { get; set; }
        private List<GameClient> TemporaryClients;
        private List<GameClient> Members;

        public PartyEngine(GameClient client, GameClient target)
        {
            Id = id++;
            Leader = client;
            Members = new List<GameClient>();
            TemporaryClients = new List<GameClient>();
            InviteClient(client, target);
            SendPartyJoinMessage(client);
        }

        public void Send(NetworkMessage message)
        {
            Members.ForEach(client => client.Send(message));
        }
        
        public void SendPartyJoinMessage(GameClient client)
        {
            Members.Add(client);
            client.Send(new PartyJoinMessage(Leader.Character.Id, Members.ConvertAll<PartyMemberInformations>(member => member.Character.GetPartyMemberInformations).ToArray()));
        }

        public void SendPartyChatServerMessage(string content, int senderId, string senderName)
        {
            Send(new ChatServerMessage(4, content, Functions.ReturnUnixTimeStamp(DateTime.Now), "", senderId, senderName));
        }

        public void InviteClient(GameClient client, GameClient target)
        {
            lock (TemporaryClients)
            {
                if (!TemporaryClients.Contains(target))
                {
                    TemporaryClients.Add(target);
                    target.Character.Party = this;
                    target.Send(new PartyInvitationMessage(client.Character.Id, client.Character.Name, target.Character.Id, target.Character.Name));
                }
            }
        }

        public void AcceptInvitation(GameClient client)
        {
            if (client.Character.Party != null)
            {

            }
            else
            {
                RemoveTemporaryClient(client);
                SendPartyJoinMessage(client);
                Send(new PartyUpdateMessage(client.Character.GetPartyMemberInformations));
            }
        }

        public void RefuseInvitation(GameClient client)
        {
            RemoveTemporaryClient(client);
            if (Members.Count <= 1)
            {
                Disband();
            }
        }

        public void RemoveMember(GameClient client)
        {
            lock (Members)
            {
                if (Members.Contains(client))
                {
                    Members.Remove(client);
                    if (Members.Count <= 1)
                    {
                        Disband();
                    }
                    client.Send(new PartyLeaveMessage());
                    Send(new PartyMemberRemoveMessage(client.Character.Id));
                    client.Character.Party = null;
                }
            }
        }

        public void RemoveTemporaryClient(GameClient client)
        {
            lock (TemporaryClients)
            {
                if (TemporaryClients.Contains(client))
                {
                    TemporaryClients.Remove(client);
                }
             }
        }

        public void Disband()
        {
            Members.ForEach(client => client.Send(new PartyLeaveMessage()));
            Members.ForEach(client => client.Character.Party = null);
        }
    }
}
