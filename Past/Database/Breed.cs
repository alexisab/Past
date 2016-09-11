using MySql.Data.MySqlClient;
using Past.Protocol.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Past.Database
{
    public class Breed
    {
        public BreedEnum Id { get; set; }
        public string MaleLook { get; set; }
        public string FemaleLook { get; set; }
        public string StatsPointsForStrength { get; set; }
        public string StatsPointsForIntelligence { get; set; }
        public string StatsPointsForChance { get; set; }
        public string StatsPointsForAgility { get; set; }
        public string StatsPointsForVitality { get; set; }
        public string StatsPointsForWisdom { get; set; }
        public string BreedSpellsId { get; set; }
        private static List<Breed> Breeds = new List<Breed>();

        public static void LoadBreeds()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM breeds", DatabaseManager.Connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Breeds.Add(new Breed()
                    {
                        Id = (BreedEnum)byte.Parse(reader["Id"].ToString()),
                        MaleLook = reader["MaleLook"].ToString(),
                        FemaleLook = reader["FemaleLook"].ToString(),
                        StatsPointsForStrength = reader["StatsPointsForStrength"].ToString(),
                        StatsPointsForIntelligence = reader["StatsPointsForIntelligence"].ToString(),
                        StatsPointsForChance = reader["StatsPointsForChance"].ToString(),
                        StatsPointsForAgility = reader["StatsPointsForAgility"].ToString(),
                        StatsPointsForVitality = reader["StatsPointsForVitality"].ToString(),
                        StatsPointsForWisdom = reader["StatsPointsForWisdom"].ToString(),
                        BreedSpellsId = reader["BreedSpellsId"].ToString()
                    });
                }
                reader.Close();
            }
           
        }

        public static string ReturnBaseLook(BreedEnum breed, bool sex)
        {
            return sex == false ? Breeds.FirstOrDefault(x => x.Id == breed).MaleLook : Breeds.FirstOrDefault(x => x.Id == breed).FemaleLook;
        }

        public static int ReturnStartMap(BreedEnum breed) //CellId 242 Direction 1
        {
            int mapId = 0;
            switch (breed)
            {
                case BreedEnum.Feca: mapId = 21893123; break;
                case BreedEnum.Osamodas: mapId = 21891589; break;
                case BreedEnum.Enutrof: mapId = 21893121; break;
                case BreedEnum.Sram: mapId = 21894663; break;
                case BreedEnum.Xelor: mapId = 21893127; break;
                case BreedEnum.Ecaflip: mapId = 21891841; break;
                case BreedEnum.Eniripsa: mapId = 21891585; break;
                case BreedEnum.Iop: mapId = 21891587; break;
                case BreedEnum.Cra: mapId = 21893377; break;
                case BreedEnum.Sadida: mapId = 21893125; break;
                case BreedEnum.Sacrieur: mapId = 21894913; break;
                case BreedEnum.Pandawa: mapId = 21891591; break;
            }
            return mapId;
        }
    }
}
