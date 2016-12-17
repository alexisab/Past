using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace Past.Common.Database.Record
{
    public class CharacterSpellRecord
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public byte Position { get; set; }
        public int SpellId { get; set; }
        public sbyte Level { get; set; }

        public CharacterSpellRecord(IDataRecord reader)
        {
            Id = (int)reader["Id"];
            OwnerId = (int)reader["OwnerId"];
            Position = (byte)reader["Position"];
            SpellId = (int)reader["SpellId"];
            Level = (sbyte)reader["Level"];
        }

        public CharacterSpellRecord(int ownerId, byte position, int spellId, sbyte level)
        {
            OwnerId = ownerId;
            Position = position;
            SpellId = spellId;
            Level = level;
        }

        public static List<CharacterSpellRecord> ReturnCharacterSpells(int ownerId)
        {
            lock (DatabaseManager.Object)
            {
                List<CharacterSpellRecord> characterSpells = new List<CharacterSpellRecord>();
                MySqlDataReader reader = DatabaseManager.ExecuteQuery($"SELECT * FROM characters_spell WHERE OwnerId = '{ownerId}'");
                while (reader.Read())
                {
                    characterSpells.Add(new CharacterSpellRecord(reader));
                }
                reader.Close();
                return characterSpells;
            }
        }

        public int Update()
        {
            return DatabaseManager.ExecuteNonQuery($"UPDATE characters_spell SET OwnerId = '{OwnerId}', Position = '{Position}', SpellId = '{SpellId}', Level = '{Level}' WHERE Id = '{Id}'");
        }

        public int Create()
        {
            return DatabaseManager.ExecuteNonQuery($"INSERT INTO characters_spell SET OwnerId = '{OwnerId}', Position = '{Position}', SpellId = '{SpellId}', Level = '{Level}'");
        }
    }
}
