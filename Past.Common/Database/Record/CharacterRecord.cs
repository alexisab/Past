using MySql.Data.MySqlClient;
using Past.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Data;
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
        public int Chance { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        #endregion
        public short StatsPoints { get; set; }
        public short SpellsPoints { get; set; }
        public DateTime? LastUsage { get; set; }

        public CharacterRecord(IDataRecord reader)
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
            Chance = (int)reader["Chance"];
            Agility = (int)reader["Agility"];
            Intelligence = (int)reader["Intelligence"];
            AlignementSide = (AlignmentSideEnum)(sbyte)reader["AlignementSide"];
            Honor = (ushort)reader["Honor"];
            Dishonor = (ushort)reader["Dishonor"];
            PvPEnabled = (bool)reader["PvPEnabled"];
            Kamas = (int)reader["Kamas"];
            StatsPoints = (short)reader["StatsPoints"];
            SpellsPoints = (short)reader["SpellsPoints"];
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

        public int Update()
        {
            return DatabaseManager.ExecuteNonQuery($"UPDATE characters SET Name = '{Name}', Level = '{Level}', Experience = '{Experience}', Breed = '{(sbyte)Breed}', EntityLookString = '{EntityLookString}', Sex = '{Convert.ToSByte(Sex)}', MapId = '{MapId}', CellId = '{CellId}', Direction = '{(sbyte)Direction}', Health = '{Health}', Energy = '{Energy}', AP = '{AP}', MP = '{MP}', Strength = '{Strength}', Vitality = '{Vitality}', Wisdom = '{Wisdom}', Chance = '{Chance}', Agility = '{Agility}', Intelligence = '{Intelligence}', AlignementSide = '{(sbyte)AlignementSide}', Honor = '{Honor}', PvPEnabled = '{Convert.ToSByte(PvPEnabled)}', Kamas = '{Kamas}', StatsPoints = '{StatsPoints}', SpellsPoints = '{SpellsPoints}', LastUsage = '{LastUsage:yyyy-MM-dd HH:mm:ss}' WHERE Id = '{Id}'");
        }

        public int Create()
        {
            return DatabaseManager.ExecuteNonQuery($"INSERT INTO characters SET OwnerId = '{OwnerId}', Name = '{Name}', Level = '{Level}', Experience = '{Experience}', Breed = '{(sbyte)Breed}', EntityLookString = '{EntityLookString}', Sex = '{Convert.ToSByte(Sex)}', MapId = '{MapId}', CellId = '{CellId}', Direction = '{(sbyte)Direction}', LastUsage = '{LastUsage:yyyy-MM-dd HH:mm:ss}'");
        }

        public int Delete()
        {
            return DatabaseManager.ExecuteNonQuery($"DELETE characters, characters_spell FROM characters INNER JOIN characters_spell WHERE characters.Id = '{Id}' AND characters_spell.OwnerId = '{Id}'");
        }
    }
}
