using MySql.Data.MySqlClient;
using Past.Common.Database;
using Past.Common.Database.Record;
using System.Collections.Generic;

namespace Past.Game.Database
{
    public class Character
    {
        public static List<CharacterRecord> ReturnCharacters(int ownerId)
        {
            lock (DatabaseManager.Object)
            {
                List<CharacterRecord> characters = new List<CharacterRecord>();
                MySqlDataReader reader = DatabaseManager.ExecuteQuery(DatabaseManager.GameConnection, $"SELECT * FROM characters WHERE OwnerId = '{ownerId}' LIMIT 5");
                if (reader.Read())
                {
                    characters.Add(new CharacterRecord(reader));
                }
                reader.Close();
                return characters;
            }
        }
    }
}
