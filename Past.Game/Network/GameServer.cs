using System.Net;
using Past.Common.Network;
using Past.Common.Utils;
using System.Collections.Generic;

namespace Past.Game.Network
{
    public class GameServer : AbstractServer<GameClient, GameServer>
    {
        public static List<GameClient> Clients;

        public GameServer() : base("Auth", IPAddress.Parse(Config.GameServerAddress), Config.GameServerPort, 100)
        {
            Clients = base._clients;
            ConsoleUtils.Write(ConsoleUtils.Type.INFO, $"Game server started on {Config.GameServerAddress}:{Config.GameServerPort} ...");
        }
    }
}
