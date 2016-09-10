using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Past.Utils
{
    public class Config
    {
        public static string Login_Address { get { return GetValue("LOGIN", "Address"); } }
        public static int Login_Port { get { return int.Parse(GetValue("LOGIN", "Port")); } }
        public static string Game_Address { get { return GetValue("GAME", "Address"); } }
        public static int Game_Port { get { return int.Parse(GetValue("GAME", "Port")); } }
        public static string Host { get { return GetValue("SQL", "Host"); } }
        public static string Database { get { return GetValue("SQL", "Database"); } }
        public static string Username { get { return GetValue("SQL", "Username"); } }
        public static string Password { get { return GetValue("SQL", "Password"); } }
        public static bool Debug { get { return bool.Parse(GetValue("OTHERS", "Debug")); } }
        public static string LoginMessage { get { return GetValue("OTHERS", "LoginMessage"); } }

        private static Dictionary<string, Dictionary<string, string>> Elements = new Dictionary<string, Dictionary<string, string>>();
        private static Dictionary<string, string> ConfigEntries;
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