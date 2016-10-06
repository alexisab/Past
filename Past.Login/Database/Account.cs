using MySql.Data.MySqlClient;
using Past.Common.Database;
using Past.Common.Database.Record;

namespace Past.Login.Database
{
    public class Account
    {
        public static AccountRecord ReturnAccount(string login)
        {
            lock (DatabaseManager.Object)
            {
                AccountRecord account = null;
                MySqlDataReader reader = DatabaseManager.ExecuteQuery(DatabaseManager.LoginConnection, $"SELECT * FROM accounts WHERE Login = '{login}' LIMIT 1");
                if (reader.Read())
                {
                    account = new AccountRecord(reader);
                }
                reader.Close();
                return account;
            }
        }
    }
}
