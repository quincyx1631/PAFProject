using MySql.Data.MySqlClient;

namespace PAFProject.Database
{
    internal class DatabaseConnector
    {
        private string connectionString;

        public DatabaseConnector()
        {
            //string database = "yansdb";
            ////string database = "yulitodb";
            //string server = "localhost";
            //string username = "root";
            //string password = "adminspcg0612#";

            //string server = "192.168.3.13";
            //string database = "pharmacydb";
            //string username = "voucher";
            //string password = "adminspcg0612#";

            string server = "localhost";
            string database = "yulitodb";
            string username = "root";
            string password = "1234";

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
                    Console.WriteLine("Connected to MySQL database 'pharmacydb' successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection failed: " + ex.Message);
                }
            }
        }
    }
}
