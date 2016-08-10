using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Past.Utils
{
    public class Config
    {
        public static Dictionary<string, Dictionary<string, string>> Elements = new Dictionary<string, Dictionary<string, string>>();
        public static Dictionary<string, string> ConfigEntries;
        #region default ini file
        private static string DefaultConfig = @"[LOGIN]
Address = 127.0.0.1 		; Address for the login server
Port = 443					; Port for the login server

[LOGIN_SQL]
Host = localhost
Database = login_past
Username = root
Password =

[GAME]
Address = 127.0.0.1 		; Address for the game server
Port = 5555					; Port for the game server

[GAME_SQL]
Host = localhost
Database = game_past
Username = root
Password =

[OTHERS]
LoginMessage = Welcome to the server Past 0.0.9";
        #endregion

        public static void ReadConfig()
        {
            if (!File.Exists("Config.ini"))
            {
                File.WriteAllText("Config.ini", DefaultConfig);
            }
            foreach (var line in File.ReadAllLines("Config.ini"))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    if (line.StartsWith("["))
                    {
                        string section = line.Replace("[", "").Replace("]", "");
                        ConfigEntries = new Dictionary<string, string>();
                        Elements.Add(section, ConfigEntries);
                    }
                    else if (ConfigEntries != null)
                    {
                        string[] data = line.Trim().Split('=', ';');
                        string key = data[0].Trim();
                        string value = data[1].Trim();
                        ConfigEntries.Add(key, value);
                    }
                }
            }
        }

        public static string GetValue(string section, string key)
        {
            return Elements.Where(x => x.Key == section).First().Value.Where(x => x.Key == key).First().Value;
        }
    }
}