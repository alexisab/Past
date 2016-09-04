using MySql.Data.MySqlClient;
using Past.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using Past.Protocol.Enums;

namespace Past.Database
{
    public class Character
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public byte Level { get; set; }
        public long Experience { get; set; }
        public sbyte Breed { get; set; }
        public string EntityLookString { get; set; }
        public bool Sex { get; set; }
        public EntityLook Look { get { return ReturnCharacterLook(this); } }
        public AlignmentSideEnum AlignementSide;
        public ushort Honor { get; set; }
        public ushort Dishonor { get; set; }
        public bool PvPEnabled { get; set; }
        public int Kamas { get; set; }
        public short StatsPoints { get; set; }
        public short SpellsPoints { get; set; }
        public DateTime? LastUsage { get; set; }

        public static List<Character> ReturnCharacters(int ownerId)
        {
            List<Character> characters = new List<Character>();
            MySqlCommand command = new MySqlCommand("SELECT * FROM characters WHERE OwnerId = '" + ownerId + "' LIMIT 5;", DatabaseManager.Connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    characters.Add(new Character()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        OwnerId = int.Parse(reader["OwnerId"].ToString()),
                        Name = reader["Name"].ToString(),
                        Level = byte.Parse(reader["Level"].ToString()),
                        Experience = long.Parse(reader["Experience"].ToString()),
                        Breed = sbyte.Parse(reader["Breed"].ToString()),
                        EntityLookString = reader["EntityLookString"].ToString(),
                        Sex = Convert.ToBoolean(reader["Sex"]),
                        Kamas = int.Parse(reader["Kamas"].ToString()),
                        AlignementSide = (AlignmentSideEnum)byte.Parse(reader["AlignementSide"].ToString()),
                        Honor = ushort.Parse(reader["Honor"].ToString()),
                        Dishonor = ushort.Parse(reader["Dishonor"].ToString()),
                        PvPEnabled = Convert.ToBoolean(reader["PvPEnabled"]),
                        StatsPoints = short.Parse(reader["StatsPoints"].ToString()),
                        SpellsPoints = short.Parse(reader["SpellsPoints"].ToString()),
                        LastUsage = reader["LastUsage"] as DateTime?
                    });
                }
                reader.Close();
                return characters.OrderByDescending(x => x.LastUsage).ToList();
            }
        }

        public static void Update(Character character)
        {
            MySqlCommand command = new MySqlCommand("UPDATE characters SET Id = @Id, OwnerId = @OwnerId, Name = @Name, Level = @Level, Experience = @Experience, Breed = @Breed, EntityLookString = @EntityLookString, Sex = @Sex, Kamas = @Kamas, StatsPoints = @StatsPoints, StatsPoints = @SpellsPoints, LastUsage = @LastUsage WHERE Id = @Id", DatabaseManager.Connection);
            command.Parameters.Add(new MySqlParameter("@Id", character.Id));
            command.Parameters.Add(new MySqlParameter("@OwnerId", character.OwnerId));
            command.Parameters.Add(new MySqlParameter("@Name", character.Name));
            command.Parameters.Add(new MySqlParameter("@Level", character.Level));
            command.Parameters.Add(new MySqlParameter("@Experience", character.Experience));
            command.Parameters.Add(new MySqlParameter("@Breed", character.Breed));
            command.Parameters.Add(new MySqlParameter("@EntityLookString", character.EntityLookString));
            command.Parameters.Add(new MySqlParameter("@Sex", character.Sex));
            command.Parameters.Add(new MySqlParameter("@Kamas", character.Kamas));
            command.Parameters.Add(new MySqlParameter("@StatsPoints", character.StatsPoints));
            command.Parameters.Add(new MySqlParameter("@SpellsPoints", character.SpellsPoints));
            command.Parameters.Add(new MySqlParameter("@LastUsage", character.LastUsage));
            command.ExecuteNonQuery();
        }

        public static EntityLook ReturnCharacterLook(Character character) //TODO SubEntity & BonesId
        {
            var look_string = character.EntityLookString.Replace("{", "").Replace("}", "").Split('|');
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
                var colors_string = look_string[2].Split(',');
                colors = new int[colors_string.Length];
                for (int i = 0; i < colors_string.Length; i++)
                    colors[i] = (i & 255) << 24 | int.Parse(colors_string[i].Remove(0, 2)) & 16777215;
            }
            else
                colors = new int[0];

            short[] size = new short[] { short.Parse(look_string[3]) };

            return new EntityLook(bonesId, skins, colors, size, new SubEntity[0]);
        }
    }
}
