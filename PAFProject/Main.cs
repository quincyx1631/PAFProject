using MaterialSkin;
using MaterialSkin.Controls;
using System;
using MySql.Data.MySqlClient;
using PAFProject.Database;
using PAFProject.Forms;
using PAFProject.Models;

namespace PAFProject
{
    public partial class Main : MaterialForm
    {

        public Main()
        {
            InitializeComponent();
            selectButton.Click += new System.EventHandler(this.selectButton_Click);

            ProductDataManager.OnProductAdded += HandleNewProduct;
            ProductDataManager.OnProposedBudget += UpdateProposedBudget;

            weeklyBudgetTextBox.TextChanged += new System.EventHandler(this.weeklyBudgetTextBox_TextChanged);

            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            kryptonDataGridView1.AllowUserToAddRows = false;
            kryptonDataGridView1.AllowUserToDeleteRows = false;
            kryptonDataGridView1.ReadOnly = true;
            kryptonDataGridView1.RowHeadersVisible = false;
            kryptonDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            kryptonDataGridView1.MultiSelect = false;
            kryptonDataGridView1.AutoGenerateColumns = false;

            kryptonDataGridView1.Rows.Clear();
        }
        private void HandleNewProduct(ProductData product)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<ProductData>(HandleNewProduct), product);
                return;
            }

            kryptonDataGridView1.Rows.Add(
                product.Description,
                product.BarCode,
                product.AverageDaily,
                product.QuantityOnHand,
                product.DaysToGo,
                product.OverShortStocks,
                product.PurchaseLimit,
                product.AveragePrice,
                product.BudgetAmount,
                product.Remarks
            );
        }
        private void UpdateProposedBudget(decimal totalBudget)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<decimal>(UpdateProposedBudget), totalBudget);
                return;
            }
            proposedBudgetTextBox.Text = totalBudget.ToString("N2");
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void weekLabel_Click(object sender, EventArgs e)
        {

        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void kryptonTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            try
            {
                Select_Product_Form selectProductForm = new Select_Product_Form(this);
                selectProductForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void weeklyBudgetTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the total budget from proposedBudget
                decimal totalBudget = decimal.Parse(proposedBudgetTextBox.Text);

                // Try to parse the Weekly budget
                if (decimal.TryParse(weeklyBudgetTextBox.Text, out decimal weeklyBudgetAmount))
                {
                    // Calculate the difference
                    decimal difference = weeklyBudgetAmount - totalBudget;

                    // Display the result in shortOverTextBox
                    shortOverTextBox.Text = difference.ToString("N2");

                    // Optionally, you can change the text color based on whether it's short or over
                    if (difference < 0)
                    {
                        shortOverTextBox.ForeColor = System.Drawing.Color.Red;
                        // You might want to show a warning message or indicator here
                    }
                    else
                    {
                        shortOverTextBox.ForeColor = System.Drawing.Color.Green;
                    }
                }
                else
                {
                    // If the input is not a valid number, clear the shortOverTextBox
                    shortOverTextBox.Text = "0.00";
                    shortOverTextBox.ForeColor = System.Drawing.Color.Black;
                }
            }
            catch (Exception ex)
            {
                // Handle any potential errors (like invalid number format)
                MessageBox.Show("Please enter a valid number.");
                weeklyBudgetTextBox.Text = "0";
                shortOverTextBox.Text = weeklyBudgetTextBox.Text;
            }
        }

        private void kryptonPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
