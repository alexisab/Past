using Past.Common.Data;
using Past.Common.Database;
using Past.Common.Utils;
using Past.Game.Engine;
using Past.Protocol;
using System;

namespace Past.Game
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleUtils.InitializeConsole("Game");
            Config.ReadConfig();

            MessageReceiver.InitializeMessages();
            MessageHandlerManager<Network.Client>.InitializeHandlers();

            DataManager.InitializeDatas();
            MapEngine.Initialize();

            DatabaseManager.Connect(Config.Database_Host, Config.Database_Username, Config.Database_Password, Config.Database_Name);
            Network.Server.Start();

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
