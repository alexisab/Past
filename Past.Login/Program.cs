using Past.Common.Network;
using Past.Common.Utils;
using Past.Protocol;
using System;
using System.Diagnostics;
using System.Timers;

namespace Past.Login
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "#Past | Login Server | Uptime : " + (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
            ConsoleUtils.InitializeConsole();
            Config.ReadConfig();

            MessageReceiver.InitializeMessages();
            MessageHandlerManager<Client>.InitializeHandlers();

           Network.Server.Start();

            Timer timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            while (true)
            {
                Console.ReadLine();
            }
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.Title = "#Past | Login Server | Uptime : " + (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ConsoleUtils.Write(ConsoleUtils.Type.ERROR, e.ToString());
        }
    }
}
