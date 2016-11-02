using Past.Common.Utils;
using System;
using System.Collections.Generic;

namespace Past.Game.Network
{
    public class Server
    {
        private static Common.Network.Server GameServer { get; set; }
        public static List<Client> Clients = new List<Client>();

        public static void Start()
        {
            GameServer = new Common.Network.Server(Config.GameServer_Address, Config.GameServer_Port);
            GameServer.OnServerStarted += Game_OnServerStarted;
            GameServer.OnServerAcceptedSocket += Game_OnServerAcceptedSocket;
            GameServer.OnServerFailedToStart += Game_OnServerFailedToStart;
            GameServer.Start();
        }

        private static void Game_OnServerFailedToStart(Exception ex)
        {
            ConsoleUtils.Write(ConsoleUtils.Type.ERROR, $"{ex}");
        }

        private static void Game_OnServerStarted()
        {
            ConsoleUtils.Write(ConsoleUtils.Type.INFO, "Game server waiting for new connection ...");
        }

        private static void Game_OnServerAcceptedSocket(Common.Network.Client socket)
        {
            ConsoleUtils.Write(ConsoleUtils.Type.INFO, "New client trying to connect to game server ...");
            Clients.Add(new Client(socket));
        }
    }
}
