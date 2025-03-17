using MySql.Data.MySqlClient;

namespace PAFProject.Database
{
    internal class DatabaseConnector
    {
        private string connectionString;

        public DatabaseConnector()
        {
            string server = "localhost";
            string database = "yansdb";
            //string database = "yulitodb";
            string username = "root";
            //string password = "1234";
            string password = "adminspcg0612#";

            connectionString = $"Server={server};Database={database};User ID={username};Password={password};SslMode=none;";
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public void TestConnection()
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connected to MySQL database 'yulitodb' successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection failed: " + ex.Message);
                }
            }
        }
    }
}
