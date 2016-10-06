using Past.Common.Database;
using Past.Common.Utils;
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

            DatabaseManager.Connect(false, Config.GameDatabase_Host, Config.GameDatabase_Username, Config.GameDatabase_Password, Config.GameDatabase_Name);
            
            Network.Server.Start();

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
