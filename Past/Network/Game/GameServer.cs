using Past.Utils;
using System;
using System.Collections.Generic;

namespace Past.Network.Game
{
    public class GameServer
    {
        private static Server Game { get; set; }
        public static List<GameClient> Clients = new List<GameClient>();

        public static void Start()
        {
            Game = new Server(Config.Game_Address, Config.Game_Port);
            Game.OnServerStarted += Game_OnServerStarted;
            Game.OnServerAcceptedSocket += Game_OnServerAcceptedSocket;
            Game.OnServerFailedToStart += Game_OnServerFailedToStart;
            Game.Start();
        }

        private static void Game_OnServerFailedToStart(Exception ex)
        {
            ConsoleUtils.Write(ConsoleUtils.type.ERROR, ex.ToString());
        }

        private static void Game_OnServerStarted()
        {
            ConsoleUtils.Write(ConsoleUtils.type.INFO, "GameServer waiting for new connection ...");
        }

        private static void Game_OnServerAcceptedSocket(Client socket)
        {
            ConsoleUtils.Write(ConsoleUtils.type.INFO, "New client trying to connect to GameServer ...");
            Clients.Add(new GameClient(socket));
        }
    }
}
