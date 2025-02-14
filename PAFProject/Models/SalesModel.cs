using MySql.Data.MySqlClient;
using PAFProject.Database;

namespace PAFProject.Models
{
    public class SalesModel
    {
        public string Description { get; set; }
        public decimal Quantity { get; set; }
    }

    public class SalesDataAccess
    {
        private readonly DatabaseConnector _dbConnector;
        public SalesDataAccess()
        {
            _dbConnector = new DatabaseConnector();
        }
        public List<SalesModel> GetThreeMonthsSales(string description)
        {
            List<SalesModel> salesData = new List<SalesModel>();
            using (var conn = _dbConnector.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                    SELECT Description, Quantity 
                    FROM yulitodb.three_months_sales 
                    WHERE Description = @description";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@description", description);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                decimal quantity = Convert.ToDecimal(reader["Quantity"]);
                                Console.WriteLine($"Raw Quantity from DB: {quantity}"); // Debug line

                                salesData.Add(new SalesModel
                                {
                                    Description = reader["Description"].ToString(),
                                    Quantity = quantity
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error fetching sales data: {ex.Message}");
                }
            }
            return salesData;
        }
    }
}