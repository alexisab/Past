using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Past.Common.Utils
{
    public class Config
    {
        public static string LoginServerAddress => GetValue("LOGIN", "Address");
        public static int LoginServerPort => int.Parse(GetValue("LOGIN", "Port"));
        public static string GameServerAddress => GetValue("GAME", "Address");
        public static int GameServerPort => int.Parse(GetValue("GAME", "Port"));
        public static string DatabaseHost => GetValue("DATABASE", "Host");
        public static string DatabaseName => GetValue("DATABASE", "Name");
        public static string DatabaseUsername => GetValue("DATABASE", "Username");
        public static string DatabasePassword => GetValue("DATABASE", "Password");
        public static int StartMap => int.Parse(GetValue("OTHERS", "StartMap"));
        public static short StartCellId => short.Parse(GetValue("OTHERS", "StartCellId"));
        public static sbyte StartDirection => sbyte.Parse(GetValue("OTHERS", "StartDirection"));
        private static readonly Dictionary<string, Dictionary<string, string>> Elements = new Dictionary<string, Dictionary<string, string>>();
        private static Dictionary<string, string> _configEntries;
        #region default ini file
        private const string DefaultConfig = @"[LOGIN]
Address = 127.0.0.1		; Address for the login server
Port = 443              ; Port for the login server

[GAME]
Address = 127.0.0.1     ; Address for the game server
Port = 5555				; Port for the game server

[DATABASE]
Host = localhost
Name = past
Username = root
Password =

[OTHERS]
StartMap = 21757955 	; Custom start map, put 0 if you don't want to use this
StartCellId = 268		; Custom start cell, put 0 if you don't want to use this
StartDirection = 1		; Custom start direction, put 0 if you don't want to use this";
        #endregion

        public static void ReadConfig()
        {
            string path = Debugger.IsAttached
                ? $@"{Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\", ""))}\Config.ini"
                : $@"{Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).ToString())}\Config.ini";
            if (!File.Exists(path))
            {
                File.WriteAllText(path, DefaultConfig);
            }
            foreach (string line in File.ReadAllLines(path))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    if (line.StartsWith("["))
                    {
                        string section = line.Replace("[", "").Replace("]", "");
                        _configEntries = new Dictionary<string, string>();
                        Elements.Add(section, _configEntries);
                    }
                    else if (_configEntries != null)
                    {
                        string[] data = line.Trim().Split('=', ';');
                        string key = data[0].Trim();
                        string value = data[1].Trim();
                        _configEntries.Add(key, value);
                    }
                }
            }
        }

        public static string GetValue(string section, string key)
        {
            return Elements.First(x => x.Key == section).Value.First(x => x.Key == key).Value;
        }
    }
}
