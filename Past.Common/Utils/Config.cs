using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Past.Common.Utils
{
    public class Config
    {
        public static string LoginServer_Address { get { return GetValue("LOGIN", "Address"); } }
        public static int LoginServer_Port { get { return int.Parse(GetValue("LOGIN", "Port")); } }
        public static string GameServer_Address { get { return GetValue("GAME", "Address"); } }
        public static int GameServer_Port { get { return int.Parse(GetValue("GAME", "Port")); } }
        public static string Database_Host { get { return GetValue("DATABASE", "Host"); } }
        public static string Database_Name { get { return GetValue("DATABASE", "Name"); } }
        public static string Database_Username { get { return GetValue("DATABASE", "Username"); } }
        public static string Database_Password { get { return GetValue("DATABASE", "Password"); } }
        public static bool Debug { get { return bool.Parse(GetValue("OTHERS", "Debug")); } }
        public static int StartMap { get { return int.Parse(GetValue("OTHERS", "StartMap")); } }
        public static short StartCellId { get { return short.Parse(GetValue("OTHERS", "StartCellId")); } }
        public static sbyte StartDirection { get { return sbyte.Parse(GetValue("OTHERS", "StartDirection")); } }
        private static Dictionary<string, Dictionary<string, string>> Elements = new Dictionary<string, Dictionary<string, string>>();
        private static Dictionary<string, string> ConfigEntries;
        #region default ini file
        private static string DefaultConfig = @"[LOGIN]
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
Debug = true            ; Display in the console message received and sent
StartMap = 21757955;	; Custom start map, put 0 if you don't want to use this
StartCellId = 268;		; Custom start cell, put 0 if you don't want to use this
StartDirection = 1;		; Custom start direction, put 0 if you don't want to use this";
        #endregion

        public static void ReadConfig()
        {
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}Config.ini";
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
