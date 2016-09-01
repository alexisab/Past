using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace Past.Database
{
    public class Experience
    {
        public byte Level { get; set; }
        public long CharacterXp { get; set; }
        public long GuildXp { get; set; }
        public long AlignementXp { get; set; }
        private static List<Experience> ExperienceFloor = new List<Experience>();

        public static void LoadExperienceFloor()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM experiences", DatabaseManager.Connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ExperienceFloor.Add(new Experience()
                    {
                        Level = byte.Parse(reader["Level"].ToString()),
                        CharacterXp = long.Parse(reader["Character"].ToString()),
                        GuildXp = long.Parse(reader["Guild"].ToString()),
                        AlignementXp = long.Parse(reader["Alignement"].ToString())
                    });
                }
                reader.Close();
            }
        }

        public static Experience GetLevelFloor(byte level)
        {
            return ExperienceFloor.FirstOrDefault(x => x.Level == level);
        }

        public static Experience GetNextLevelFloor(byte level)
        {
            return ExperienceFloor.FirstOrDefault(x => x.Level == level + 1);
        }
    }
}
