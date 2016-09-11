﻿using Past.Network.Game;
using Past.Network.Handlers;
using Past.Network.Login;
using Past.Protocol;
using Past.Utils;
using System;
using System.Security.Cryptography;

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

            LoginServer.Start();
            GameServer.Start();

            while (true)
            {
               Console.Read();
            }
        }
    }
}
