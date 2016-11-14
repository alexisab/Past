using Past.Common.Data;
using Past.Game.Network;
using Past.Protocol;
using Past.Protocol.Messages;
using System.Collections.Generic;

namespace Past.Game.Engine
{
    public class MapEngine
    {
        public Map Map { get; set; }
        private List<Client> Clients;

        public MapEngine(Map map)
        {
            Map = map;
            Clients = new List<Client>();
        }

        public void Send(NetworkMessage message)
        {
            Clients.ForEach(client => client.Send(message));
        }

        public void SendGameRolePlayShowActorMessage(Client client)
        {
            foreach (Client _client in Clients)
            {
                client.Send(new GameRolePlayShowActorMessage(_client.Character.GetGameRolePlayCharacterInformations));
            }
        }

        public void AddClient(Client client)
        {
            lock (Clients)
            {
                if (!Clients.Contains(client))
                {
                    Clients.Add(client);
                    SendGameRolePlayShowActorMessage(client);
                    Send(new GameRolePlayShowActorMessage(client.Character.GetGameRolePlayCharacterInformations));
                }
            }
        }

        public void RemoveClient(Client client)
        {
            lock (Clients)
            {
                if (Clients.Contains(client))
                {
                    Clients.Remove(client);
                    Send(new GameContextRemoveElementMessage(client.Character.Id));
                }
            }
        }

        public static void Initialize()
        {
            foreach (Map map in Map.Maps.Values)
            {
                map.Instance = new MapEngine(map);
            }
        }
    }
}
