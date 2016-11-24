using Past.Game.Network;
using Past.Protocol.Messages;
using Past.Protocol.Types;
using System.Collections.Generic;

namespace Past.Game.Engine
{
    public class PartyEngine
    {
        private int id = 0;
        private int Id { get; set; }
        private Client Leader { get; set; }
        private List<Client> InvitedClients;

        public PartyEngine(Client client, Client target)
        {
            Id = id++;
            Leader = client;
            InvitedClients = new List<Client>();
            InviteCharacter(client, target);
            SendPartyJoinMessage(client);
        }

        public void SendPartyJoinMessage(Client client)
        {
            client.Send(new PartyJoinMessage(Leader.Character.Id, InvitedClients.ConvertAll<PartyMemberInformations>(invitedClients => invitedClients.Character.GetPartyMemberInformations).ToArray()));
        }

        public void InviteCharacter(Client client, Client target)
        {
            lock (InvitedClients)
            {
                if (!InvitedClients.Contains(target))
                {
                    InvitedClients.Add(client);
                    InvitedClients.Add(target);
                    target.Send(new PartyInvitationMessage(client.Character.Id, client.Character.Name, target.Character.Id, target.Character.Name));
                }
            }
        }

        public void RemoveCharacter(Client client)
        {
            lock (InvitedClients)
            {
                if (InvitedClients.Contains(client))
                {
                    InvitedClients.Remove(client);
                }
            }
        }
    }
}
