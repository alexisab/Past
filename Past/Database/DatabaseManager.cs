using MySql.Data.MySqlClient;
using Past.Utils;
using System;

namespace Past.Database
{
    public class DatabaseManager
    {
        private static MySqlConnection Connection { get; set; }

        public static void Connect()
        {
            Connection = new MySqlConnection();
            Connection.ConnectionString = string.Format("server={0};uid={1};pwd={2};database={3};", Config.GetValue("SQL", "Host"), Config.GetValue("SQL", "Username"), Config.GetValue("SQL", "Password"), Config.GetValue("SQL", "Database"));
            Connection.Open();
        }

        public static Account ReturnAccount(string login)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM accounts WHERE Login = '" + login + "'", Connection);
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
