using Past.Common.Data;
using Past.Game.Network;
using Past.Protocol;
using Past.Protocol.Messages;
using System.Collections.Generic;

namespace Past.Game.Engine
{
    public class MapEngine
    {
        private readonly List<GameClient> _clients;

        public MapEngine()
        {
            _clients = new List<GameClient>();
        }

        public void Send(NetworkMessage message)
        {
            _clients.ForEach(client => client.Send(message));
        }

        public void SendGameRolePlayShowActorMessage(GameClient client)
        {
            foreach (GameClient mapClient in _clients)
            {
                client.Send(new GameRolePlayShowActorMessage(mapClient.Character.GetGameRolePlayCharacterInformations));
            }
        }

        public void SendCharacterLevelUpInformation(GameClient client)
        {
            foreach (GameClient mapClient in _clients)
            {
                if (mapClient != client)
                {
                    Send(new CharacterLevelUpInformationMessage(client.Character.Level, client.Character.Name, client.Character.Id, 0));
                }
            }
        }

        public void AddClient(GameClient client)
        {
            lock (_clients)
            {
                if (!_clients.Contains(client))
                {
                    _clients.Add(client);
                    SendGameRolePlayShowActorMessage(client);
                    Send(new GameRolePlayShowActorMessage(client.Character.GetGameRolePlayCharacterInformations));
                }
            }
        }

        public void RemoveClient(GameClient client)
        {
            lock (_clients)
            {
                if (_clients.Contains(client))
                {
                    _clients.Remove(client);
                    Send(new GameContextRemoveElementMessage(client.Character.Id));
                }
            }
        }

        public static void Initialize()
        {
            foreach (var map in Map.Maps.Values)
            {
                map.Instance = new MapEngine();
            }
        }
    }
}
