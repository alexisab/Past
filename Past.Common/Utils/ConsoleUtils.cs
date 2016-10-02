using System;

namespace Past.Common.Utils
{
    public class ConsoleUtils
    {
        private static readonly object Object = new object();
        public enum Type { INFO, WARNING, ERROR, DONE, DEBUG, RECEIV, SEND };

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

        public static void InitializeConsole()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < logo.Length; i++)
            {
                string text = logo[i];
                Console.WriteLine(text.PadLeft((int)(Console.BufferWidth + text.Length) / 2));
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Write(Type type, string text, params object[] args)
        {
            lock (Object)
            {
                switch (type)
                {
                    case Type.INFO:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[{0}]", type);
                        break;
                    case Type.WARNING:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("[{0}]", type);
                        break;
                    case Type.ERROR:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[{0}]", type);
                        break;
                    case Type.DONE:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("[{0}]", type);
                        break;
                    case Type.DEBUG:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("[{0}]", type);
                        break;
                    case Type.RECEIV:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("[{0}]", type);
                        break;
                    case Type.SEND:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("[{0}]", type);
                        break;
                }
                Console.SetCursorPosition(10, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(text, args);
            }
        }
    }
}
    