using System;
using System.Diagnostics;
using System.Timers;

namespace Past.Common.Utils
{
    public class ConsoleUtils
    {
        private static readonly object Object = new object();
        public enum Type { INFO, WARNING, ERROR, DONE, DEBUG, RECEIV, SEND, PAST };

        public static string[] logo = new string[]
        {
            "                                   ",
            "     #####     ##     ####   ##### ",
            "     #    #   #  #   #         #   ",
            "     #    #  #    #   ####     #   ",
            "     #####   ######       #    #   ",
            "     #       #    #  #    #    #   ",
            "     #       #    #   ####     #   ",
            "                                   "
        };

        public static void InitializeConsole(string service)
        {
            Console.Title = $"#Past | {service} Server | Uptime : {(DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss")}";
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < logo.Length; i++)
            {
                string text = logo[i];
                Console.WriteLine(text.PadLeft((int)(Console.BufferWidth + text.Length) / 2));
            }
            Console.ForegroundColor = ConsoleColor.White;
            Timer timer = new Timer(1000);
            timer.Elapsed += (sender, e) => { Console.Title = $"#Past | {service} Server | Uptime : {(DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss")}"; };
            timer.Start();
        }

        public static void Write(Type type, string text, params object[] args)
        {
            lock (Object)
            {
                switch (type)
                {
                    case Type.INFO:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"[{type}]");
                        break;
                    case Type.WARNING:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"[{type}]");
                        break;
                    case Type.ERROR:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"[{type}]");
                        break;
                    case Type.DONE:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"[{type}]");
                        break;
                    case Type.DEBUG:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write($"[{type}]");
                        break;
                    case Type.RECEIV:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write($"[{type}]");
                        break;
                    case Type.SEND:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"[{type}]");
                        break;
                    case Type.PAST:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"[{type}]");
                        break;
                }
                Console.SetCursorPosition(10, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(text, args);
            }
        }
    }
}
    