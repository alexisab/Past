using System.Net;
using Past.Common.Network;
using Past.Common.Utils;
using System.Collections.Generic;
using Past.Game.Engine;

namespace Past.Game.Network
{
    public class GameServer : AbstractServer<GameClient, GameServer>
    {
        public static List<GameClient> Clients;

        public GameServer() : base("Auth", IPAddress.Parse(Config.GameServerAddress), Config.GameServerPort, 100)
        {
            Clients = _clients;
            ConsoleUtils.Write(ConsoleUtils.Type.INFO, $"Game server started on {Config.GameServerAddress}:{Config.GameServerPort} ...");
        }

        public static List<CharacterEngine> GetCharacters()
        {
            List<CharacterEngine> characters = new List<CharacterEngine>();
            foreach (GameClient client in Clients)
            {
                if (client.Character != null)
                {
                    characters.Add(client.Character);
                }
            }
            return characters;
        }
    }
}
