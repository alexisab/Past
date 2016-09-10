using MySql.Data.MySqlClient;
using Past.Utils;

namespace Past.Database
{
    public class DatabaseManager
    {
        public static MySqlConnection Connection { get; set; }

        public static void Connect()
        {
            try
            {
                Connection = new MySqlConnection();
                Connection.ConnectionString = string.Format("server={0};uid={1};pwd={2};database={3};", Config.Host, Config.Username, Config.Password, Config.Database);
                Connection.Open();
            }
            catch (MySqlException ex)
            {
                ConsoleUtils.Write(ConsoleUtils.type.ERROR, ex.ToString());
            }
        }

        public static void Close()
        {
            try
            {
                Connection = null;
                Connection.Close();
            }
            catch (MySqlException ex)
            {
                ConsoleUtils.Write(ConsoleUtils.type.ERROR, ex.ToString());
            }
        }
    }
}
