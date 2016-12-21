using Past.Common.Database;
using Past.Common.Utils;
using Past.Protocol;
using System;
using Past.Login.Network;

namespace Past.Login
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConsoleUtils.InitializeConsole("Login");
            Config.ReadConfig();
            
            MessageReceiver.InitializeMessages();
            MessageHandlerManager<LoginClient>.InitializeHandlers();

            DatabaseManager.Connect(Config.DatabaseHost, Config.DatabaseUsername, Config.DatabasePassword, Config.DatabaseName);

            var s = new LoginServer();

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
