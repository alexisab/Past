using Past.Common.Data;
using Past.Game.Network;
using Past.Protocol;
using Past.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            Parallel.ForEach(Clients, (Action<Client>)(client => client.Send(message)));
        }

        public void SendMapGameRolePlayShowActorMessage(Client client)
        {
            foreach (Client Client in Clients)
            {
                client.Send(new GameRolePlayShowActorMessage(Client.Character.GetGameRolePlayCharacterInformations()));
            }
        }

        public void AddClient(Client client)
        {
            lock (Clients)
            {
                if (!Clients.Contains(client))
                {
                    Clients.Add(client);
                    SendMapGameRolePlayShowActorMessage(client);
                    Send(new GameRolePlayShowActorMessage(client.Character.GetGameRolePlayCharacterInformations()));
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
