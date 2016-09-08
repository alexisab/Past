using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Past.Database
{
    public class Map
    {
        public int Id { get; set; }
        public int RelativeId { get; set; }
        public byte MapType { get; set; }
        public short SubareaId { get; set; }
        public int TopNeighbourId { get; set; }
        public int BottomNeighbourId { get; set; }
        public int LeftNeighbourId { get; set; }
        public int RightNeighbourId { get; set; }
        public static readonly Dictionary<int, Map> Maps = new Dictionary<int, Map>();

        public static void LoadMaps()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM maps", DatabaseManager.Connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = int.Parse(reader["Id"].ToString());
                    Maps.Add(id, new Map()
                    {
                        Id = id,
                        RelativeId = int.Parse(reader["RelativeId"].ToString()),
                        MapType = byte.Parse(reader["MapType"].ToString()),
                        SubareaId = short.Parse(reader["SubareaId"].ToString()),
                        TopNeighbourId = int.Parse(reader["TopNeighbourId"].ToString()),
                        BottomNeighbourId = int.Parse(reader["BottomNeighbourId"].ToString()),
                        LeftNeighbourId = int.Parse(reader["LeftNeighbourId"].ToString()),
                        RightNeighbourId = int.Parse(reader["RightNeighbourId"].ToString())
                    });
                }
                reader.Close();
            }
        }
    }
}
