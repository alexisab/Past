using Past.Common.Database;
using Past.Common.Utils;
using Past.Login.Database;
using Past.Protocol;
using System;

namespace Past.Login
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleUtils.InitializeConsole("Login");
            Config.ReadConfig();
            
            MessageReceiver.InitializeMessages();
            MessageHandlerManager<Network.Client>.InitializeHandlers();

            DatabaseManager.Connect(Config.LoginDatabase_Host, Config.LoginDatabase_Username, Config.LoginDatabase_Password, Config.LoginDatabase_Name);

            Network.Server.Start();

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
