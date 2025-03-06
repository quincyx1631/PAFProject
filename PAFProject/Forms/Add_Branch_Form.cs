using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using PAFProject.Database;

namespace PAFProject.Forms
{
    public partial class Add_Branch_Form : MaterialForm
    {
        private readonly DatabaseConnector _dbConnector;
        private readonly Main _mainForm;

        public Add_Branch_Form(Main mainForm)  // Modify constructor to accept main form
        {
            InitializeComponent();
            _dbConnector = new DatabaseConnector();
            _mainForm = mainForm;
            addButton.Click += new EventHandler(this.AddBranchButton_Click);
        }

        private void AddBranchButton_Click(object sender, EventArgs e)
        {
            string branchName = BranchNameTextBox.Text.Trim();
            string branchLocation = BranchLocationTextBox.Text.Trim();

            if (string.IsNullOrEmpty(branchName))
            {
                MessageBox.Show("Branch Name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var conn = _dbConnector.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO yulitodb.branch (name, address, isActive) VALUES (@name, @address, 1)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", branchName);
                        cmd.Parameters.AddWithValue("@address", string.IsNullOrEmpty(branchLocation) ? DBNull.Value : branchLocation);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Branch added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _mainForm.RefreshBranchList();  // Refresh the branch list
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add branch.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding branch: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
