using Past.Common.Data;
using Past.Common.Database;
using Past.Common.Utils;
using Past.Game.Engine;
using Past.Protocol;
using System;
using Past.Game.Network;

namespace Past.Game
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConsoleUtils.InitializeConsole("Game");
            Config.ReadConfig();

            MessageReceiver.InitializeMessages();
            MessageHandlerManager<GameClient>.InitializeHandlers();

            DataManager.InitializeDatas();
            MapEngine.Initialize();

            DatabaseManager.Connect(Config.DatabaseHost, Config.DatabaseUsername, Config.DatabasePassword,
                Config.DatabaseName);

            GameServer gameServer = new GameServer();

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}