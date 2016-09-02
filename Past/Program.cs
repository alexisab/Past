using Past.Network.Game;
using Past.Network.Handlers;
using Past.Network.Login;
using Past.Protocol;
using Past.Utils;
using System;

namespace Past
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleUtils.InitializeConsole();
            Config.ReadConfig();

            LoginServer.Start();
            GameServer.Start();

            MessageReceiver.InitializeMessages();
            MessageHandlerManager<LoginClient>.InitializeHandlers();
            MessageHandlerManager<GameClient>.InitializeHandlers();

            Database.DatabaseManager.Connect();
            Database.Experience.LoadExperienceFloor();

            while (true)
            {
               Console.Read();
            }
        }
    }
}
