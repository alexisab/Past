using MySql.Data.MySqlClient;
using Past.Common.Utils;

namespace Past.Common.Database
{
    public class DatabaseManager
    {
        public static readonly object Object = new object();
        private static MySqlConnection Connection { get; set; }

        public static void Connect(bool login, string host, string username, string password, string databaseName)
        {
            lock (Object)
            {
                try
                {
                    string connectionString = $"server={host};uid={username};pwd={password};database={databaseName}";
                    Connection = new MySqlConnection();
                    Connection.ConnectionString = connectionString;
                    Connection.Open();
                    ConsoleUtils.Write(ConsoleUtils.Type.INFO, $"Successfully connected to database {databaseName} ...");
                }
                catch (MySqlException ex)
                {
                    ConsoleUtils.Write(ConsoleUtils.Type.ERROR, $"{ex}");
                }
            }
        }

        public static void Close(bool login)
        {
            lock (Object)
            {
                try
                {
                    Connection = null;
                    Connection.Close();
                }
                catch (MySqlException ex)
                {
                    ConsoleUtils.Write(ConsoleUtils.Type.ERROR, $"{ex}");
                }
            }
        }

        public static MySqlDataReader ExecuteQuery(string query)
        {
            lock (Object)
            {
                return new MySqlCommand($"{query}", Connection).ExecuteReader();
            }
        }

        public static int ExecuteNonQuery(string query)
        {
            lock (Object)
            {
                return new MySqlCommand($"{query}", Connection).ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(string query)
        {
            lock (Object)
            {
                return new MySqlCommand($"{query}", Connection).ExecuteScalar();
            }
        }
    }
}
