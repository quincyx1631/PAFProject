//Product_list_Form.cs
using MaterialSkin.Controls;
using PAFProject.Class;
using PAFProject.Computations;
using PAFProject.Models;

namespace PAFProject.Forms
{
    public partial class Product_List_Form : MaterialForm
    {
        private readonly ProductManager _productManager;
        private Select_Product_Form _parentForm;
        private int currentPage = 1;
        private const int RecordsPerPage = 30;
        private readonly ProductDataAccess _productDataAccess;

        public Product_List_Form(Select_Product_Form parentForm = null)
        {
            InitializeComponent();
            _productManager = new ProductManager();
            _productDataAccess = new ProductDataAccess();
            _parentForm = parentForm;

            // Configure DataGridView properties
            SetupDataGridView();

            // Add double-click event handler for the entire row
            productListDataGrid.CellDoubleClick += ProductListDataGrid_CellDoubleClick;

            // Wire up other event handlers
            searchButton.Click += SearchButton_Click;
            previousButton.Click += PreviousButton_Click;
            nextButton.Click += NextButton_Click;
            searchTextBox.KeyPress += SearchTextBox_KeyPress;

            this.Resize += Product_List_Form_Resize;

            LoadInventoryData();
        }

        private void ProductListDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && _parentForm != null)
            {
                try
                {
                    string salesDesc = productListDataGrid.Rows[e.RowIndex].Cells["SalesDesc"].Value.ToString();
                    decimal quantityOnHand = decimal.Parse(productListDataGrid.Rows[e.RowIndex].Cells["QuantityOnHand"].Value.ToString());
                    decimal averageCost = decimal.Parse(productListDataGrid.Rows[e.RowIndex].Cells["AverageCost"].Value.ToString());

                    var avgDailySalesComputation = new AverageDailySalesComputation();
                    decimal numericAverageDailySales = avgDailySalesComputation.GetNumericAverageDailySales(salesDesc);

                    var daysToGoComputation = new DaysToGoComputation();
                    string daysToGoStr = daysToGoComputation.ComputeDaysToGo(quantityOnHand, numericAverageDailySales);

                    var overShortStocksComputation = new OverShortStocksComputation();
                    string overShortStocks = overShortStocksComputation.ComputeOverShortStocks(
                        decimal.Parse(daysToGoStr),
                        quantityOnHand,
                        numericAverageDailySales
                    );

                    _parentForm.AddSelectedProduct(
                        salesDesc,
                        numericAverageDailySales,
                        quantityOnHand,
                        daysToGoStr,
                        overShortStocks,
                        averageCost,
                        _productDataAccess.GetManufacturerPartNumber(salesDesc),
                        _productDataAccess.GetPrefVendorRefFullName(salesDesc)
                    );
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error selecting product: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetupDataGridView()
        {
            // Set the DataGridView to only anchor to top, left, and right
            productListDataGrid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            productListDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            productListDataGrid.AllowUserToResizeRows = false;
            productListDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            productListDataGrid.MultiSelect = false;
            productListDataGrid.ReadOnly = true;
            productListDataGrid.AutoGenerateColumns = true;
            productListDataGrid.RowHeadersVisible = false;
        }

        private void Product_List_Form_Resize(object sender, EventArgs e)
        {
            FormatDataGrid();
        }

        private void LoadInventoryData()
        {
            try
            {
                var (dataTable, totalPages) = _productManager.GetInventoryData(
                    searchTextBox.Text.Trim(),
                    currentPage,
                    RecordsPerPage
                );
                productListDataGrid.DataSource = dataTable;
                pageInfoLabel.Values.Text = $"Page {currentPage} of {totalPages}";

                // Update navigation buttons
                previousButton.Enabled = currentPage > 1;
                nextButton.Enabled = currentPage < totalPages;

                FormatDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGrid()
        {
            if (productListDataGrid.Columns.Count > 0)
            {
                _productManager.FormatDataGridView(productListDataGrid);

                productListDataGrid.Width = this.ClientSize.Width -
                    (productListDataGrid.Margin.Left + productListDataGrid.Margin.Right);
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadInventoryData();
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadInventoryData();
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (currentPage < _productManager.TotalPages)
            {
                currentPage++;
                LoadInventoryData();
            }
        }

        private void SearchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SearchButton_Click(sender, e);
            }
        }
    }
}