using MySql.Data.MySqlClient;
using Past.Protocol.Types;
using System;
using System.Collections.Generic;

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

        public static List<Character> ReturnCharacters(int ownerId)
        {
            List<Character> characters = new List<Character>();
            MySqlCommand command = new MySqlCommand("SELECT * FROM characters WHERE OwnerId = '" + ownerId + "'", DatabaseManager.Connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    characters.Add(new Character()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Level = byte.Parse(reader["Level"].ToString()),
                        Experience = long.Parse(reader["Experience"].ToString()),
                        Breed = sbyte.Parse(reader["Breed"].ToString()),
                        EntityLookString = reader["EntityLookString"].ToString(),
                        Sex = Convert.ToBoolean(reader["Sex"])
                    });
                }
                reader.Close();
                return characters;
            }
        }

        public static EntityLook ReturnCharacterLook(Character character) //TODO SubEntity
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
