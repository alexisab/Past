using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Past.Database
{
    public class Character
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public sbyte Breed { get; set; }
        public bool Sex { get; set; }

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
                        Breed = sbyte.Parse(reader["Breed"].ToString()),
                        Sex = Convert.ToBoolean(reader["Sex"])
                    });
                }
                reader.Close();
                return characters;
            }
        }
    }
}
