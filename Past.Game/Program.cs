using Past.Common.Data;
using Past.Common.Database;
using Past.Common.Utils;
using Past.Game.Engine;
using Past.Protocol;
using System;

namespace Past.Game
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConsoleUtils.InitializeConsole("Game");
            Config.ReadConfig();

            MessageReceiver.InitializeMessages();
            MessageHandlerManager<Network.Client>.InitializeHandlers();

            DataManager.InitializeDatas();
            MapEngine.Initialize();

            DatabaseManager.Connect(Config.DatabaseHost, Config.DatabaseUsername, Config.DatabasePassword, Config.DatabaseName);
            Network.Server.Start();

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}