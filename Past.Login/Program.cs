using Past.Common.Database;
using Past.Common.Utils;
using Past.Protocol;
using System;

namespace Past.Login
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConsoleUtils.InitializeConsole("Login");
            Config.ReadConfig();
            
            MessageReceiver.InitializeMessages();
            MessageHandlerManager<Network.Client>.InitializeHandlers();

            DatabaseManager.Connect(Config.Database_Host, Config.Database_Username, Config.Database_Password, Config.Database_Name);

            Network.Server.Start();

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
