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
        public string Ticket { get; set; }
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
            Ticket = (string)reader["Ticket"];
            Nickname = (string)reader["Nickname"];
            HasRights = (bool)reader["HasRights"];
            SecretQuestion = (string)reader["SecretQuestion"];
            SecretAnswer = (string)reader["SecretAnswer"];
            BannedUntil = reader["BannedUntil"] as DateTime?;
            LastConnection = reader["LastConnection"] as DateTime?;
            LastIp = (string)reader["LastIp"];
        }

        public static AccountRecord ReturnAccount(string login)
        {
            lock (DatabaseManager.Object)
            {
                AccountRecord account = null;
                MySqlDataReader reader = DatabaseManager.ExecuteQuery($"SELECT * FROM accounts WHERE Login = '{login}' LIMIT 1");
                if (reader.Read())
                {
                    account = new AccountRecord(reader);
                }
                reader.Close();
                return account;
            }
        }

        public static AccountRecord ReturnAccountWithTicket(string ticket)
        {
            lock (DatabaseManager.Object)
            {
                AccountRecord account = null;
                MySqlDataReader reader = DatabaseManager.ExecuteQuery($"SELECT * FROM accounts WHERE Ticket = '{ticket}' LIMIT 1");
                if (reader.Read())
                {
                    account = new AccountRecord(reader);
                }
                reader.Close();
                return account;
            }
        }

        public int Update()
        {
            return DatabaseManager.ExecuteNonQuery($"UPDATE accounts SET Id = '{Id}', Login = '{Login}', Password = '{Password}', Ticket = '{Ticket}', Nickname = '{Nickname}', HasRights = '{Convert.ToInt32(HasRights)}', SecretQuestion = '{SecretQuestion}', SecretAnswer = '{SecretAnswer}', BannedUntil = '{BannedUntil.Value.ToString("yyyy-MM-dd HH:mm:ss")}', LastConnection = '{LastConnection.Value.ToString("yyyy-MM-dd HH:mm:ss")}', LastIp = '{LastIp}' WHERE Id = '{Id}'");
        }
    }
}