using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Past.Common.Network;
using Past.Common.Utils;

namespace Past.Game.Network
{
    public class GameServer : AbstractServer<GameClient, GameServer>
    {
        public GameServer() : base("Auth", IPAddress.Parse(Config.GameServerAddress), Config.GameServerPort, 100)
        {
            ConsoleUtils.Write(ConsoleUtils.Type.INFO, $"Game server started on {Config.GameServerAddress}:{Config.GameServerPort} ...");
        }
    }
}
