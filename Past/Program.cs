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
            LoginServer.Start();
            GameServer.Start();
            MessageReceiver.InitializeMessages();
            MessageHandlerManager.InitializeHandlers();
            while (true)
            {
                Console.Read();
            }
        }
        
    }
}
