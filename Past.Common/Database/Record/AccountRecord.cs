using MySql.Data.MySqlClient;
using Past.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Data;

namespace Past.Common.Database.Record
{
    public class AccountRecord
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Ticket { get; set; }
        public string Nickname { get; set; }
        public GameHierarchyEnum Role { get; set; }
        public bool HasRights => Role >= GameHierarchyEnum.PLAYER;
        public string SecretQuestion { get; set; }
        public string SecretAnswer { get; set; }
        public DateTime? BannedUntil { get; set; }
        public List<CharacterRecord> Characters = new List<CharacterRecord>();
        public DateTime? LastConnection { get; set; }
        public string LastIp { get; set; }

        public AccountRecord(IDataRecord reader)
        {
            Id = (int)reader["Id"];
            Login = (string)reader["Login"];
            Password = (string)reader["Password"];
            Ticket = (string)reader["Ticket"];
            Nickname = (string)reader["Nickname"];
            Role = (GameHierarchyEnum)(sbyte)reader["Role"];
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

        public static AccountRecord ReturnAccount(int id)
        {
            lock (DatabaseManager.Object)
            {
                AccountRecord account = null;
                MySqlDataReader reader = DatabaseManager.ExecuteQuery($"SELECT * FROM accounts WHERE Id = '{id}' LIMIT 1");
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
            return DatabaseManager.ExecuteNonQuery($"UPDATE accounts SET Id = '{Id}', Login = '{Login}', Password = '{Password}', Ticket = '{Ticket}', Nickname = '{Nickname}', Role = '{(sbyte)Role}', SecretQuestion = '{SecretQuestion}', SecretAnswer = '{SecretAnswer}', BannedUntil = '{BannedUntil:yyyy-MM-dd HH:mm:ss}', LastConnection = '{LastConnection:yyyy-MM-dd HH:mm:ss}', LastIp = '{LastIp}' WHERE Id = '{Id}'");
        }
    }
}