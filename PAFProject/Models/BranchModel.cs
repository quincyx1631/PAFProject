// BranchModel.cs
// This file manages access to branch data.
using MySql.Data.MySqlClient;
using PAFProject.Database;
using System.Data;
using System.Collections.Generic;

namespace PAFProject.Models
{
    public class BranchModel
    {
        public string BranchName { get; set; }
    }

    public class BranchDataAccess
    {
        private readonly DatabaseConnector _dbConnector;

        public BranchDataAccess()
        {
            _dbConnector = new DatabaseConnector();
        }

        public List<string> GetBranchList()
        {
            List<string> branches = new List<string>();

            try
            {
                using (var conn = _dbConnector.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT branchName FROM yulitodb.branch WHERE isActive = 1";

                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            branches.Add(reader["branchName"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting branch list: {ex.Message}");
                throw;
            }

            return branches;
        }
    }
}
