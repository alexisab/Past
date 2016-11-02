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
        public short StatsPoints { get; set; }
        public short SpellsPoints { get; set; }
        public DateTime? LastUsage { get; set; }
        public EntityLook EntityLook { get { return ReturnEntityLook(this); } }

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

        public int Create()
        {
            return DatabaseManager.ExecuteNonQuery($"INSERT INTO characters SET OwnerId = '{OwnerId}', Name = '{Name}', Level = '{Level}', Experience = '{Experience}', Breed = '{(sbyte)Breed}', EntityLookString = '{EntityLookString}', Sex = '{Convert.ToSByte(Sex)}', MapId = '{MapId}', CellId = '{CellId}', Direction = '{(sbyte)Direction}', LastUsage = '{LastUsage.Value.ToString("yyyy-MM-dd HH:mm:ss")}'");
        }

        public int Delete()
        {
            return DatabaseManager.ExecuteNonQuery($"DELETE FROM characters WHERE Id = '{Id}'");
        }

        public static EntityLook ReturnEntityLook(CharacterRecord character) //TODO SubEntity & BonesId
        {
            string[] look_string = character.EntityLookString.Replace("{", "").Replace("}", "").Split('|');
            short bonesId = short.Parse(look_string[0]);

            short[] skins;
            if (look_string[1].Contains(","))
            {
                var skins_string = look_string[1].Split(',');
                skins = new short[skins_string.Length];
                for (int i = 0; i < skins_string.Length; i++)
                    skins[i] = short.Parse(skins_string[i]);
            }
            else
                skins = new short[] { short.Parse(look_string[1]) };

            int[] colors;
            if (look_string[2].Contains(","))
            {
                string[] colors_string = look_string[2].Split(',');
                colors = new int[colors_string.Length];
                for (int i = 0; i < colors_string.Length; i++)
                {
                    int color = int.Parse(colors_string[i].Remove(0, 2));
                    if (color == -1) { }
                    else
                        colors[i] = (i + 1 & 255) << 24 | color & 16777215;
                }
            }
            else
                colors = new int[0];

            short[] size = new short[] { short.Parse(look_string[3]) };

            return new EntityLook(bonesId, skins, colors, size, new SubEntity[0]);
        }
    }
}
