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
        public Client Leader { get; set; }
        private List<Client> TemporaryClients;
        private List<Client> Members;

        public PartyEngine(Client client, Client target)
        {
            Id = id++;
            Leader = client;
            Members = new List<Client>();
            TemporaryClients = new List<Client>();
            InviteClient(client, target);
            SendPartyJoinMessage(client);
        }

        public void Send(NetworkMessage message)
        {
            Members.ForEach(client => client.Send(message));
        }
        
        public void SendPartyJoinMessage(Client client)
        {
            Members.Add(client);
            client.Send(new PartyJoinMessage(Leader.Character.Id, Members.ConvertAll<PartyMemberInformations>(member => member.Character.GetPartyMemberInformations).ToArray()));
        }

        public void SendPartyChatServerMessage(string content, int senderId, string senderName)
        {
            Send(new ChatServerMessage(4, content, Functions.ReturnUnixTimeStamp(DateTime.Now), "", senderId, senderName));
        }

        public void InviteClient(Client client, Client target)
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

        public void AcceptInvitation(Client client)
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

        public void RefuseInvitation(Client client)
        {
            RemoveTemporaryClient(client);
            if (Members.Count <= 1)
            {
                Disband();
            }
        }

        public void RemoveMember(Client client)
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

        public void RemoveTemporaryClient(Client client)
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
