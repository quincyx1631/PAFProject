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

            // Subscribe to ProductDataManager events
            ProductDataManager.OnProductAdded += HandleNewProduct;
            ProductDataManager.OnWeeklyBudgetUpdated += UpdateWeeklyBudget;

            // Initialize the DataGridView
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
        private void UpdateWeeklyBudget(decimal totalBudget)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<decimal>(UpdateWeeklyBudget), totalBudget);
                return;
            }
            weeklyBudgetTextBox.Text = totalBudget.ToString("N2");
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
    }
}
