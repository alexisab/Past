using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Past.Database
{
    public class Account
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public bool HasRights { get; set; }
        public string SecretQuestion { get; set; }
        public string SecretAnswer { get; set; }
        public DateTime BannedUntil { get; set; }
        public List<Character> Characters = new List<Character>();

        public static Account ReturnAccount(string login)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM accounts WHERE Login = '" + login + "' COLLATE latin1_bin", DatabaseManager.Connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (!reader.Read())
                    return null;
                Account account = new Account();
                account.Id = int.Parse(reader["Id"].ToString());
                account.Login = reader["Login"].ToString();
                account.Password = reader["Password"].ToString();
                account.Nickname = reader["Nickname"].ToString();
                account.HasRights = Convert.ToBoolean(reader["HasRights"]);
                account.SecretQuestion = reader["SecretQuestion"].ToString();
                account.BannedUntil = reader["BannedUntil"] as DateTime? ?? DateTime.MinValue;
                reader.Close();
                return account;
            }
        }
    }
}

