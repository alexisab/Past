using MySql.Data.MySqlClient;
using Past.Protocol.Enums;
using Past.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Past.Common.Database.Record
{
    public class CharacterRecord
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public byte Level { get; set; }
        public long Experience { get; set; }
        public BreedEnum Breed { get; set; }
        public string EntityLookString { get; set; }
        public bool Sex { get; set; }
        public int MapId { get; set; }
        public short CellId { get; set; }
        public DirectionsEnum Direction { get; set; }
        public AlignmentSideEnum AlignementSide { get; set; }
        public ushort Honor { get; set; }
        public ushort Dishonor { get; set; }
        public bool PvPEnabled { get; set; }
        public int Kamas { get; set; }

        #region base stats
        public int Health { get; set; }
        public short Energy { get; set; }
        public byte AP { get; set; }
        public byte MP { get; set; }
        public int Strength { get; set; }
        public int Vitality { get; set; }
        public int Wisdom { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        #endregion

        #region stats added with scroll
        public int ScrollStrength { get; set; }
        public int ScrollVitality { get; set; }
        public int ScrollWisdom { get; set; }
        public int ScrollAgility { get; set; }
        public int ScrollIntelligence { get; set; }
        #endregion

        public short StatsPoints { get; set; }
        public short SpellsPoints { get; set; }
        public DateTime? LastUsage { get; set; }

        public CharacterRecord(MySqlDataReader reader)
        {
            Id = (int)reader["Id"];
            OwnerId = (int)reader["OwnerId"];
            Name = (string)reader["Name"];
            Level = (byte)reader["Level"];
            Experience = (long)reader["Experience"];
            Breed = (BreedEnum)(sbyte)reader["Breed"];
            EntityLookString = (string)reader["EntityLookString"];
            Sex = (bool)reader["Sex"];
            MapId = (int)reader["MapId"];
            CellId = (short)reader["CellId"];
            Direction = (DirectionsEnum)(sbyte)reader["Direction"];
            Health = (int)reader["Health"];
            Energy = (short)reader["Energy"];
            AP = (byte)reader["AP"];
            MP = (byte)reader["MP"];
            Strength = (int)reader["Strength"];
            Vitality = (int)reader["Vitality"];
            Wisdom = (int)reader["Wisdom"];
            Agility = (int)reader["Agility"];
            Intelligence = (int)reader["Intelligence"];
            AlignementSide = (AlignmentSideEnum)(sbyte)reader["AlignementSide"];
            Honor = (ushort)reader["Honor"];
            Dishonor = (ushort)reader["Dishonor"];
            PvPEnabled = (bool)reader["PvPEnabled"];
            Kamas = (int)reader["Kamas"];
            StatsPoints = (short)reader["StatsPoints"];
            SpellsPoints = (short)reader["SpellsPoints"];
            ScrollStrength = (int)reader["ScrollStrength"];
            ScrollVitality = (int)reader["ScrollVitality"];
            ScrollWisdom = (int)reader["ScrollWisdom"];
            ScrollAgility = (int)reader["ScrollAgility"];
            ScrollIntelligence = (int)reader["ScrollIntelligence"];
            LastUsage = reader["LastUsage"] as DateTime?;
        }

        public CharacterRecord(int ownerId, string name, byte level, long experience, BreedEnum breed, string entityLookString, bool sex, int mapId, short cellId, DirectionsEnum direction, AlignmentSideEnum alignementSide, ushort honor, ushort dishonor, bool pvpEnabled, int kamas, short statsPoints, short spellsPoints, DateTime? lastUsage)
        {
            OwnerId = ownerId;
            Name = name;
            Level = level;
            Experience = experience;
            Breed = breed;
            EntityLookString = entityLookString;
            Sex = sex;
            MapId = mapId;
            CellId = cellId;
            Direction = direction;
            AlignementSide = alignementSide;
            Honor = honor;
            Dishonor = dishonor;
            PvPEnabled = pvpEnabled;
            Kamas = kamas;
            StatsPoints = statsPoints;
            SpellsPoints = spellsPoints;
            LastUsage = lastUsage;
        }

        public static List<CharacterRecord> ReturnCharacters(int ownerId)
        {
            lock (DatabaseManager.Object)
            {
                List<CharacterRecord> characters = new List<CharacterRecord>();
                MySqlDataReader reader = DatabaseManager.ExecuteQuery($"SELECT * FROM characters WHERE OwnerId = '{ownerId}' LIMIT 5");
                while (reader.Read())
                {
                    characters.Add(new CharacterRecord(reader));
                }
                reader.Close();
                return characters.OrderByDescending(x => x.LastUsage).ToList();
            }
        }

        public static bool NameExist(string name)
        {
            return Convert.ToBoolean(DatabaseManager.ExecuteScalar($"SELECT EXISTS (SELECT 1 FROM characters WHERE Name = '{name}')"));
        }

        public int Create()
        {
            return DatabaseManager.ExecuteNonQuery($"INSERT INTO characters SET OwnerId = '{OwnerId}', Name = '{Name}', Level = '{Level}', Experience = '{Experience}', Breed = '{(sbyte)Breed}', EntityLookString = '{EntityLookString}', Sex = '{Convert.ToSByte(Sex)}', MapId = '{MapId}', CellId = '{CellId}', Direction = '{(sbyte)Direction}', LastUsage = '{LastUsage.Value.ToString("yyyy-MM-dd HH:mm:ss")}'");
        }

        public int Delete()
        {
            return DatabaseManager.ExecuteNonQuery($"DELETE FROM characters WHERE Id = '{Id}'");
        }
    }
}
