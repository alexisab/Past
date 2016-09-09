﻿using MySql.Data.MySqlClient;
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
        public DateTime? LastConnection { get; set; }
        public string LastIp { get; set; }

        public static Account ReturnAccount(string login)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM accounts WHERE Login = '" + login + "' LIMIT 1;", DatabaseManager.Connection);
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
                account.SecretAnswer = reader["SecretAnswer"].ToString();
                account.BannedUntil = reader["BannedUntil"] as DateTime? ?? DateTime.MinValue;
                account.LastConnection = reader["LastConnection"] as DateTime?;
                account.LastIp = reader["LastIp"].ToString();
                reader.Close();
                return account;
            }
        }
        
        public static void Update(Account account)
        {
            MySqlCommand command = new MySqlCommand("UPDATE accounts SET Id = @Id, Login = @Login, Password = @Password, Nickname = @Nickname, HasRights = @HasRights, SecretQuestion = @SecretQuestion, SecretAnswer = @SecretAnswer, BannedUntil = @BannedUntil, LastConnection = @LastConnection, LastIp = @LastIp WHERE Id = @Id", DatabaseManager.Connection);
            command.Parameters.Add(new MySqlParameter("@Id", account.Id));
            command.Parameters.Add(new MySqlParameter("@Login", account.Login));
            command.Parameters.Add(new MySqlParameter("@Password", account.Password));
            command.Parameters.Add(new MySqlParameter("@Nickname", account.Nickname));
            command.Parameters.Add(new MySqlParameter("@HasRights", account.HasRights));
            command.Parameters.Add(new MySqlParameter("@SecretQuestion", account.SecretQuestion));
            command.Parameters.Add(new MySqlParameter("@SecretAnswer", account.SecretAnswer));
            command.Parameters.Add(new MySqlParameter("@BannedUntil", account.BannedUntil));
            command.Parameters.Add(new MySqlParameter("@LastConnection", account.LastConnection));
            command.Parameters.Add(new MySqlParameter("@LastIp", account.LastIp));
            command.ExecuteNonQuery();
        }

        public static bool NicknameExist(string nickname)
        {
            MySqlCommand command = new MySqlCommand("SELECT EXISTS (SELECT 1 FROM accounts WHERE Nickname = @Nickname)", DatabaseManager.Connection);
            command.Parameters.AddWithValue("Nickname", nickname);
            return Convert.ToBoolean(command.ExecuteScalar());
        }
    }
}
