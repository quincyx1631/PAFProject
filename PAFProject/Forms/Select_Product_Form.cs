//Select_Product_Form.cs
using Krypton.Toolkit;
using MaterialSkin.Controls;
using PAFProject.Computations;
using PAFProject.Design;
using PAFProject.Models;
namespace PAFProject.Forms
{
    public partial class Select_Product_Form : MaterialForm
    {
        private readonly ProductDataAccess _productDataAccess;
        private decimal _currentAverageDailySales = 0;
        private PurchaseLimitGridViewDesign _gridViewDesign;
        private Main _mainForm;
        private ProductData _editingProduct;
        private AverageDailySalesComputation _avgDailySalesComputation;
        private int? _editingIndex;

        public Select_Product_Form(Main mainForm, ProductData editingProduct = null, int? editingIndex = null)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _editingProduct = editingProduct;
            _editingIndex = editingIndex;
            _avgDailySalesComputation = new AverageDailySalesComputation();

            selectProductButton.Click += new System.EventHandler(this.selectProductButton_Click);
            doneButton.Click += new System.EventHandler(this.doneButton_Click);
            _gridViewDesign = new PurchaseLimitGridViewDesign(kryptonDataGridView1);
            _productDataAccess = new ProductDataAccess();

            InitializeEvents();

            if (_editingProduct != null)
            {
                PopulateFormWithExistingData();
            }
        }

        private void InitializeEvents()
        {
            limitSelectionDropdown.TextChanged += LimitSelectionDropdown_TextChanged;
            _gridViewDesign.OnBudgetCalculated += UpdateBudgetAmount;
        }
        private void Select_Product_Form_Load(object sender, EventArgs e)
        {
            // Initialize dropdowns with the period change callback
            MonthsDropdownManager.InitializeMonthlyDropdown(monthlyDropdown, UpdateAverageDailySalesCalculation);
            LimitSelectionDropdown.InitializeMonthlyDropdown(limitSelectionDropdown);
        }
        public void ClearAllTextBoxes()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    nameTextBox.Clear();
                    avgTextBox.Clear();
                    qtyTextBox.Clear();
                    daysTGTextBox.Clear();
                    overShortSTextBox.Clear();
                    AVGPriceTextBox.Clear();
                    budgetAmountTextBox.Clear();
                    remarksTextBox.Clear();
                }));
            }
            else
            {
                nameTextBox.Clear();
                avgTextBox.Clear();
                qtyTextBox.Clear();
                daysTGTextBox.Clear();
                overShortSTextBox.Clear();
                AVGPriceTextBox.Clear();
                budgetAmountTextBox.Clear();
                remarksTextBox.Clear();
            }
        }
        public void UpdateProductName(string salesDesc)
        {
            if (nameTextBox.InvokeRequired)
            {
                nameTextBox.Invoke(new Action(() => nameTextBox.Text = salesDesc));
            }
            else
            {
                nameTextBox.Text = salesDesc;
            }
        }
        public void UpdateQuantityOnHand(string quantity)
        {
            if (qtyTextBox.InvokeRequired)
            {
                qtyTextBox.Invoke(new Action(() => qtyTextBox.Text = quantity));
            }
            else
            {
                qtyTextBox.Text = quantity;
            }
        }

        private void PopulateFormWithExistingData()
        {
            nameTextBox.Text = _editingProduct.Description;
            avgTextBox.Text = _editingProduct.AverageDaily;
            qtyTextBox.Text = _editingProduct.QuantityOnHand;
            daysTGTextBox.Text = _editingProduct.DaysToGo;
            overShortSTextBox.Text = _editingProduct.OverShortStocks;
            AVGPriceTextBox.Text = _editingProduct.AveragePrice;
            budgetAmountTextBox.Text = _editingProduct.BudgetAmount;
            remarksTextBox.Text = _editingProduct.Remarks;

            // Initialize grid with existing purchase limit
            _gridViewDesign.InitializeWithExistingData(_editingProduct.PurchaseLimit);

            if (decimal.TryParse(_editingProduct.AverageDaily, out decimal avgDaily))
            {
                _currentAverageDailySales = avgDaily;

                // Calculate the days from purchase limit and average daily sales
                if (decimal.TryParse(_editingProduct.PurchaseLimit, out decimal purchaseLimit))
                {
                    int calculatedDays = (int)Math.Round(purchaseLimit / avgDaily);
                    string daysText = GetClosestDaysSelection(calculatedDays);
                    limitSelectionDropdown.Values.Text = daysText;
                }
            }

            if (decimal.TryParse(_editingProduct.AveragePrice, out decimal avgPrice))
            {
                _gridViewDesign.SetAverageCost(avgPrice);
            }
        }
        private string GetClosestDaysSelection(int calculatedDays)
        {
            if (calculatedDays <= 7) return "7 Days";
            if (calculatedDays <= 15) return "15 Days";
            return "30 Days";
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            try
            {
                string purchaseLimit = "0";
                if (_gridViewDesign._dataGridView.Rows.Count > 0 &&
                    _gridViewDesign._dataGridView.Rows[0].Cells[2].Value != null)
                {
                    purchaseLimit = _gridViewDesign._dataGridView.Rows[0].Cells[2].Value.ToString();
                }

                string manufacturerPartNumber = _productDataAccess.GetManufacturerPartNumber(nameTextBox.Text);

                var productData = new ProductData
                {
                    Description = nameTextBox.Text,
                    BarCode = manufacturerPartNumber,
                    AverageDaily = avgTextBox.Text,
                    QuantityOnHand = qtyTextBox.Text,
                    DaysToGo = daysTGTextBox.Text,
                    OverShortStocks = overShortSTextBox.Text,
                    PurchaseLimit = purchaseLimit,
                    AveragePrice = AVGPriceTextBox.Text,
                    BudgetAmount = budgetAmountTextBox.Text,
                    Remarks = remarksTextBox.Text
                };

                if (_editingIndex.HasValue)
                {
                    ProductDataManager.UpdateProduct(productData, _editingIndex.Value);
                }
                else
                {
                    ProductDataManager.AddProduct(productData);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing product: " + ex.Message);
            }
        }
        private void selectProductButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Pass this form instance to Product_List_Form
                Product_List_Form selectProductListForm = new Product_List_Form(this);
                selectProductListForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void LimitSelectionDropdown_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the numeric average daily sales value from the formatted text
                string numericPart = new string(avgTextBox.Text.TakeWhile(c => char.IsDigit(c) || c == '.').ToArray());
                if (decimal.TryParse(numericPart, out decimal averageDailySales))
                {
                    _currentAverageDailySales = averageDailySales;

                    // Create instance of PurchaseLimitComputation
                    var purchaseLimitComputation = new PurchaseLimitComputation();

                    // Get days from selection
                    int days = purchaseLimitComputation.GetDaysFromSelection(limitSelectionDropdown.Values.Text);

                    // Compute new purchase limit
                    int newPurchaseLimit = purchaseLimitComputation.ComputePurchaseLimit(averageDailySales, days);

                    // Update the grid's system value with the new purchase limit
                    _gridViewDesign.UpdateSystemValue(limitSelectionDropdown.Values.Text, averageDailySales);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating purchase limit: " + ex.Message);
            }
        }
        public void UpdateAverageDailySales(string averageDailySales)
        {
            if (avgTextBox.InvokeRequired)
            {
                avgTextBox.Invoke(new Action(() =>
                {
                    avgTextBox.Text = averageDailySales;
                    // Extract numeric value from the formatted string
                    string numericPart = new string(averageDailySales.TakeWhile(c => char.IsDigit(c) || c == '.').ToArray());
                    if (decimal.TryParse(numericPart, out decimal value))
                    {
                        _currentAverageDailySales = value;
                        _gridViewDesign.UpdateSystemValue(limitSelectionDropdown.Values.Text, value);
                    }
                }));
            }
            else
            {
                avgTextBox.Text = averageDailySales;
                // Extract numeric value from the formatted string
                string numericPart = new string(averageDailySales.TakeWhile(c => char.IsDigit(c) || c == '.').ToArray());
                if (decimal.TryParse(numericPart, out decimal value))
                {
                    _currentAverageDailySales = value;
                    _gridViewDesign.UpdateSystemValue(limitSelectionDropdown.Values.Text, value);
                }
            }
        }

        public void UpdateDaysToGo(string daysToGo)
        {
            if (daysTGTextBox.InvokeRequired)
            {
                daysTGTextBox.Invoke(new Action(() => daysTGTextBox.Text = daysToGo));
            }
            else
            {
                daysTGTextBox.Text = daysToGo;
            }
        }

        public void UpdateOverShortStocks(string overShortStocks)
        {
            if (overShortSTextBox.InvokeRequired)
            {
                overShortSTextBox.Invoke(new Action(() => overShortSTextBox.Text = overShortStocks));
            }
            else
            {
                overShortSTextBox.Text = overShortStocks;
            }
        }
        public void UpdateAverageCost(string averageCost)
        {
            if (AVGPriceTextBox.InvokeRequired)
            {
                AVGPriceTextBox.Invoke(new Action(() =>
                {
                    AVGPriceTextBox.Text = averageCost;
                    if (decimal.TryParse(averageCost, out decimal cost))
                    {
                        _gridViewDesign.SetAverageCost(cost);
                    }
                }));
            }
            else
            {
                AVGPriceTextBox.Text = averageCost;
                if (decimal.TryParse(averageCost, out decimal cost))
                {
                    _gridViewDesign.SetAverageCost(cost);
                }
            }
        }
        public void UpdateBudgetAmount(string budget)
        {
            if (budgetAmountTextBox.InvokeRequired)
            {
                budgetAmountTextBox.Invoke(new Action(() =>
                    budgetAmountTextBox.Text = budget));
            }
            else
            {
                budgetAmountTextBox.Text = budget;
            }
        }
        private void UpdateAverageDailySalesCalculation(bool isThreeMonths)
        {
            if (!string.IsNullOrEmpty(nameTextBox.Text))
            {
                _avgDailySalesComputation.SetPeriod(isThreeMonths);

                string formattedAverageDailySales = _avgDailySalesComputation.ComputeAverageDailySales(nameTextBox.Text);
                UpdateAverageDailySales(formattedAverageDailySales);

                // Recalculate days to go and over/short stocks
                if (decimal.TryParse(qtyTextBox.Text, out decimal quantityOnHand))
                {
                    decimal numericAverageDailySales = _avgDailySalesComputation.GetNumericAverageDailySales(nameTextBox.Text);

                    var daysToGoComputation = new DaysToGoComputation();
                    string daysToGo = daysToGoComputation.ComputeDaysToGo(quantityOnHand, numericAverageDailySales);
                    UpdateDaysToGo(daysToGo);

                    var overShortStocksComputation = new OverShortStocksComputation();
                    string overShortStocks = overShortStocksComputation.ComputeOverShortStocks(
                        decimal.Parse(daysToGo),
                        quantityOnHand,
                        numericAverageDailySales
                    );
                    UpdateOverShortStocks(overShortStocks);
                }
            }
        }
        private void weeklyBudgetLabel_Click(object sender, EventArgs e)
        {
        }
        private void label4_Click(object sender, EventArgs e)
        {
        }
        private void kryptonTextBox2_TextChanged(object sender, EventArgs e)
        {
        }
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
        }
        private void avgTextBox_TextChanged(object sender, EventArgs e)
        {
        }
        private void qtyTextBox_TextChanged(object sender, EventArgs e)
        {
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void budgetAmountTextBox_TextChanged(object sender, EventArgs e)
        {
        }
        private void budgetAmountLabel_Click(object sender, EventArgs e)
        {
        }
        private void limitSelectionDropdown_Click(object sender, EventArgs e)
        {
        }
        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}