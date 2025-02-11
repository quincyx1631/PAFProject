// ProductModel.cs
//This file manages access to product inventory data.
using MySql.Data.MySqlClient;
using PAFProject.Database;
using System.Data;

namespace PAFProject.Models
{
    public class ProductModel
    {
        public string SalesDesc { get; set; }
        public decimal QuantityOnHand { get; set; }
        public decimal AverageCost { get; set; }
    }

    public class ProductDataAccess
    {
        private readonly DatabaseConnector _dbConnector;

        public ProductDataAccess()
        {
            _dbConnector = new DatabaseConnector();
        }

        public (DataTable DataTable, int TotalPages, List<decimal> QuantityOnHandList) GetInventoryData(string searchTerm, int currentPage, int recordsPerPage)
        {
            using (var conn = _dbConnector.GetConnection())
            {
                try
                {
                    conn.Open();
                    var dataTable = new DataTable();
                    var quantityOnHandList = new List<decimal>();
                    int totalRecords = 0;

                    // Get total records for pagination
                    string countQuery = "SELECT COUNT(*) FROM item_inventory_active";
                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        countQuery += " WHERE SalesDesc LIKE @searchTerm";
                    }

                    using (var cmd = new MySqlCommand(countQuery, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            cmd.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");
                        }
                        totalRecords = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    int totalPages = (int)Math.Ceiling((double)totalRecords / recordsPerPage);

                    // Get paginated data
                    string query = @"
                SELECT 
                    SalesDesc,
                    QuantityOnHand,
                    AverageCost
                FROM item_inventory_active";

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        query += " WHERE SalesDesc LIKE @searchTerm";
                    }
                    query += " LIMIT @offset, @limit";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            cmd.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");
                        }
                        cmd.Parameters.AddWithValue("@offset", (currentPage - 1) * recordsPerPage);
                        cmd.Parameters.AddWithValue("@limit", recordsPerPage);

                        using (var reader = cmd.ExecuteReader())
                        {
                            dataTable.Load(reader);

                            // Reset reader to fetch QuantityOnHand separately
                            reader.Close();

                            // Fetch QuantityOnHand values
                            using (var quantityCmd = new MySqlCommand(query, conn))
                            {
                                if (!string.IsNullOrWhiteSpace(searchTerm))
                                {
                                    quantityCmd.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");
                                }
                                quantityCmd.Parameters.AddWithValue("@offset", (currentPage - 1) * recordsPerPage);
                                quantityCmd.Parameters.AddWithValue("@limit", recordsPerPage);

                                using (var quantityReader = quantityCmd.ExecuteReader())
                                {
                                    while (quantityReader.Read())
                                    {
                                        quantityOnHandList.Add(quantityReader.GetDecimal("QuantityOnHand"));
                                    }
                                }
                            }
                        }
                    }

                    return (dataTable, totalPages, quantityOnHandList);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error fetching inventory data: {ex.Message}");
                }
            }
        }
    }
}