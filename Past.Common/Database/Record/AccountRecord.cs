using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Past.Common.Database.Record
{
    public class AccountRecord
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public bool HasRights { get; set; }
        public string SecretQuestion { get; set; }
        public string SecretAnswer { get; set; }
        public DateTime? BannedUntil { get; set; }
        public List<CharacterRecord> Characters = new List<CharacterRecord>();
        public DateTime? LastConnection { get; set; }
        public string LastIp { get; set; }

        public AccountRecord(MySqlDataReader reader)
        {
            Id = (int)reader["Id"];
            Login = (string)reader["Login"];
            Password = (string)reader["Password"];
            Nickname = (string)reader["Nickname"];
            HasRights = (bool)reader["HasRights"];
            SecretQuestion = (string)reader["SecretQuestion"];
            SecretAnswer = (string)reader["SecretAnswer"];
            BannedUntil = reader["BannedUntil"] as DateTime?;
            LastConnection = reader["LastConnection"] as DateTime?;
            LastIp = (string)reader["LastIp"];
        }
    }
}