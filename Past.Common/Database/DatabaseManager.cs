using MySql.Data.MySqlClient;
using Past.Common.Utils;

namespace Past.Common.Database
{
    public class DatabaseManager
    {
        public static readonly object Object = new object();
        private static MySqlConnection Connection { get; set; }

        public static void Connect(string host, string username, string password, string databaseName)
        {
            lock (Object)
            {
                try
                {
                    Connection = new MySqlConnection();
                    Connection.ConnectionString = $"server={host};uid={username};pwd={password};database={databaseName}";
                    Connection.Open();
                    ConsoleUtils.Write(ConsoleUtils.Type.INFO, $"Successfully connected to database {databaseName} ...");
                }
                catch (MySqlException ex)
                {
                    ConsoleUtils.Write(ConsoleUtils.Type.ERROR, $"{ex}");
                }
            }
        }

        public static void Close()
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
                MySqlCommand command = new MySqlCommand($"{query}", Connection);
                return command.ExecuteReader();
            }
        }
    }
}
