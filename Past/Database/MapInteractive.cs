using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Past.Database
{
    public class MapInteractive
    {
        public int ElementId { get; set; }
        public int MapId { get; set; }
        public short CellId { get; set; }
        public int Type { get; set; }
        public static List<MapInteractive> MapInteractives = new List<MapInteractive>();

        public static void LoadMapInteractives()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM maps_interactives", DatabaseManager.Connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    MapInteractives.Add(new MapInteractive()
                    {
                        ElementId = int.Parse(reader["ElementId"].ToString()),
                        MapId = int.Parse(reader["MapId"].ToString()),
                        CellId = short.Parse(reader["CellId"].ToString()),
                        Type = int.Parse(reader["Type"].ToString())
                    });
                }
                reader.Close();
            }
        }

        public static List<MapInteractive> ReturnMapInteractives(int mapId)
        {
            return MapInteractives.FindAll(x => x.MapId == mapId);
        }
    }
}
