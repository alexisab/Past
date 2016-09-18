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

            MessageReceiver.InitializeMessages();
            MessageHandlerManager<LoginClient>.InitializeHandlers();
            MessageHandlerManager<GameClient>.InitializeHandlers();

            Database.DatabaseManager.Connect();
            Database.Experience.LoadExperienceFloor();
            Database.Breed.LoadBreeds();
            Database.Map.LoadMaps();
            Database.MapInteractive.LoadMapInteractives();

            LoginServer.Start();
            GameServer.Start();

            while (true)
            {
               Console.Read();
            }
        }
    }
}
