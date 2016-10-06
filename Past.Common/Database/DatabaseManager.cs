using MySql.Data.MySqlClient;
using Past.Common.Utils;

namespace Past.Common.Database
{
    public class DatabaseManager
    {
        public static readonly object Object = new object();
        public static MySqlConnection LoginConnection { get; set; }
        public static MySqlConnection GameConnection { get; set; }

        public static void Connect(bool login, string host, string username, string password, string databaseName)
        {
            lock (Object)
            {
                try
                {
                    string connectionString = $"server={host};uid={username};pwd={password};database={databaseName}";
                    if (login)
                    {
                        LoginConnection = new MySqlConnection();
                        LoginConnection.ConnectionString = connectionString;
                        LoginConnection.Open();
                    }
                    else
                    {
                        GameConnection = new MySqlConnection();
                        GameConnection.ConnectionString = connectionString;
                        GameConnection.Open();
                    }
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
                    if (login)
                    {
                        LoginConnection = null;
                        LoginConnection.Close();
                    }
                    else
                    {
                        GameConnection = null;
                        GameConnection.Close();
                    }
                }
                catch (MySqlException ex)
                {
                    ConsoleUtils.Write(ConsoleUtils.Type.ERROR, $"{ex}");
                }
            }
        }

        public static MySqlDataReader ExecuteQuery(MySqlConnection connection, string query)
        {
            lock (Object)
            {
                MySqlCommand command = new MySqlCommand($"{query}", connection);
                return command.ExecuteReader();
            }
        }
    }
}
