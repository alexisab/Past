using Past.Common.Data;
using Past.Game.Network;
using Past.Protocol;
using System;
using System.Collections.Generic;

namespace Past.Game.Engine
{
    public class MapEngine
    {
        public Map Map { get; set; }
        private static List<Client> Clients;

        public MapEngine(Map map)
        {
            Map = map;
            Clients = new List<Client>();
        }

        public void Send(NetworkMessage message)
        {
            Clients.ForEach((Action<Client>)(client => client.Send(message)));
        }

        public static void Initialize()
        {
            foreach (Map map in Map.Maps.Values)
            {
                map.Instance = new MapEngine(map);
            }
        }
    }
}
