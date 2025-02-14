using MaterialSkin.Controls;
using PAFProject.Export;
using PAFProject.Forms;
using PAFProject.Models;

namespace PAFProject
{
    public partial class Main : MaterialForm
    {
        private int selectedRowIndex = -1;


        public Main()
        {
            InitializeComponent();
            selectButton.Click += new System.EventHandler(this.selectButton_Click);

            ProductDataManager.OnProductAdded += HandleNewProduct;
            ProductDataManager.OnProposedBudget += UpdateProposedBudget;
            processButton.Click += new System.EventHandler(this.ProcessButton_Click);

            weeklyBudgetTextBox.TextChanged += new System.EventHandler(this.weeklyBudgetTextBox_TextChanged);

            InitializeDataGridView();

            // Add double-click event handler
            kryptonDataGridView1.CellDoubleClick += KryptonDataGridView1_CellDoubleClick;
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

            // Center-align all column headers
            foreach (DataGridViewColumn column in kryptonDataGridView1.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void KryptonDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRowIndex = e.RowIndex;

                var currentProduct = new ProductData
                {
                    Description = kryptonDataGridView1.Rows[e.RowIndex].Cells[0].Value?.ToString(),
                    BarCode = kryptonDataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString(),
                    AverageDaily = kryptonDataGridView1.Rows[e.RowIndex].Cells[2].Value?.ToString(),
                    QuantityOnHand = kryptonDataGridView1.Rows[e.RowIndex].Cells[3].Value?.ToString(),
                    DaysToGo = kryptonDataGridView1.Rows[e.RowIndex].Cells[4].Value?.ToString(),
                    OverShortStocks = kryptonDataGridView1.Rows[e.RowIndex].Cells[5].Value?.ToString(),
                    PurchaseLimit = kryptonDataGridView1.Rows[e.RowIndex].Cells[6].Value?.ToString(),
                    AveragePrice = kryptonDataGridView1.Rows[e.RowIndex].Cells[7].Value?.ToString(),
                    BudgetAmount = kryptonDataGridView1.Rows[e.RowIndex].Cells[8].Value?.ToString(),
                    Remarks = kryptonDataGridView1.Rows[e.RowIndex].Cells[9].Value?.ToString()
                };

                Select_Product_Form selectProductForm = new Select_Product_Form(this, currentProduct, selectedRowIndex);
                selectProductForm.Show();
            }
        }

        private void HandleNewProduct(ProductData product, int? editIndex)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<ProductData, int?>((p, i) => HandleNewProduct(p, i)), product, editIndex);
                return;
            }

            if (editIndex.HasValue && editIndex.Value >= 0 && editIndex.Value < kryptonDataGridView1.Rows.Count)
            {
                // Update existing row
                var row = kryptonDataGridView1.Rows[editIndex.Value];
                row.Cells[0].Value = product.Description;
                row.Cells[1].Value = product.BarCode;
                row.Cells[2].Value = product.AverageDaily;
                row.Cells[3].Value = product.QuantityOnHand;
                row.Cells[4].Value = product.DaysToGo;
                row.Cells[5].Value = product.OverShortStocks;
                row.Cells[6].Value = product.PurchaseLimit;
                row.Cells[7].Value = product.AveragePrice;
                row.Cells[8].Value = product.BudgetAmount;
                row.Cells[9].Value = product.Remarks;
            }
            else
            {
                // Add new row
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
            // Align specific columns to the right
            kryptonDataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // AverageDaily
            kryptonDataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // QuantityOnHand
            kryptonDataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // DaysToGo
            kryptonDataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // OverShortStocks
            kryptonDataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // PurchaseLimit
            kryptonDataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // AveragePrice
            kryptonDataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // BudgetAmount

            // Align remaining columns to the left
            kryptonDataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; // Description
            kryptonDataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; // BarCode
            kryptonDataGridView1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; // Remarks
        }

        private void ProcessButton_Click(object sender, EventArgs e)
        {
            string branchName = branchNameLabel.Text;
            string week = weekTextBox.Text;
            string weeklyBudget = weeklyBudgetTextBox.Text;
            string proposedBudget = proposedBudgetTextBox.Text;
            string shortOver = shortOverTextBox.Text;

            ExcelExporter.ExportToExcel(kryptonDataGridView1, branchName, week, weeklyBudget, proposedBudget, shortOver);
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
    }
}
