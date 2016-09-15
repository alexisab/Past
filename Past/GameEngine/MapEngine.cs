using Past.Database;
using Past.Network.Game;
using Past.Protocol;
using Past.Protocol.Messages;
using System;
using System.Collections.Generic;

namespace Past.GameEngine
{
    public class MapEngine
    {
        public Map Map { get; set; }
        public List<GameClient> Clients;

        public MapEngine(Map map)
        {
            Map = map;
            Clients = new List<GameClient>();
        }

        public void Send(NetworkMessage message)
        {
            Clients.ForEach((Action<GameClient>)(x => x.Send(message)));
        }

        public void SendGameRolePlayShowActorMessage(GameClient client)
        {
            var clients = Clients.FindAll(x => x.Character.Map.Id == client.Character.Map.Id);
            foreach (var x in clients)
            {
                client.Send(new GameRolePlayShowActorMessage(x.Character.CharacterInformations));
            }
        }

        public void AddClient(GameClient client)
        {
            lock(Clients)
            {
                if (!Clients.Contains(client))
                {
                    Clients.Add(client);
                    SendGameRolePlayShowActorMessage(client);
                    Send(new GameRolePlayShowActorMessage(client.Character.CharacterInformations));
                }
            }
        }

        public void RemoveClient(GameClient client)
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
    }
}
