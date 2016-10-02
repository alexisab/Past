using Past.Common.Utils;
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

            Network.Server.Start();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            while (true)
            {
                Console.ReadLine();
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ConsoleUtils.Write(ConsoleUtils.Type.ERROR, e.ToString());
        }
    }
}
