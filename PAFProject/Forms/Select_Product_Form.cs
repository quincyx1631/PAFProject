//Select_Product_Form.cs
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

        private List<SelectedProduct> selectedProducts = new List<SelectedProduct>();

        private int currentProductIndex = -1;

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
            InitializeProductListSelection();
        }
        public void AddSelectedProduct(string description, decimal avgDaily, decimal qtyOnHand,
     string daysToGo, string overShort, decimal avgCost, string barCode, string prefVendor)
        {
            SaveCurrentFormValues();

            var product = new SelectedProduct
            {
                Description = description,
                AverageDaily = avgDaily,
                QuantityOnHand = qtyOnHand,
                DaysToGo = daysToGo,
                OverShortStocks = overShort,
                AverageCost = avgCost,
                BarCode = barCode,
                PrefVendor = prefVendor,
                UserValue = "0",
                Remarks = "",
                LimitSelection = limitSelectionDropdown.Values.Text,
                IsThreeMonthPeriod = monthlyDropdown.Values.Text == "3 Months"
            };

            int existingIndex = selectedProducts.FindIndex(p => p.Description == description);
            if (existingIndex >= 0)
            {
                product.UserValue = selectedProducts[existingIndex].UserValue;
                product.SystemValue = selectedProducts[existingIndex].SystemValue;
                product.TotalValue = selectedProducts[existingIndex].TotalValue;
                product.BudgetAmount = selectedProducts[existingIndex].BudgetAmount;
                product.Remarks = selectedProducts[existingIndex].Remarks;

                selectedProducts[existingIndex] = product;
                productListSelection.Rows[existingIndex].Cells[0].Value = description;
                currentProductIndex = existingIndex;
            }
            else
            {
                selectedProducts.Add(product);
                productListSelection.Rows.Add(description);
                currentProductIndex = selectedProducts.Count - 1;
            }

            if (currentProductIndex >= 0)
            {
                productListSelection.ClearSelection();
                productListSelection.Rows[currentProductIndex].Selected = true;
            }

            PopulateFormWithSelectedData(currentProductIndex);
            // Calculate initial purchase limit based on average daily sales and selected period
            var purchaseLimitComputation = new PurchaseLimitComputation();
            int days = purchaseLimitComputation.GetDaysFromSelection(limitSelectionDropdown.Values.Text);
            int systemValue = purchaseLimitComputation.ComputePurchaseLimit(avgDaily, days);

            // Update the grid with the system value
            _gridViewDesign.UpdateSystemValue(limitSelectionDropdown.Values.Text, avgDaily);
            _gridViewDesign.SetAverageCost(avgCost);

            // Get the total value from the grid after it's updated
            if (_gridViewDesign._dataGridView.Rows.Count > 0)
            {
                var totalValueCell = _gridViewDesign._dataGridView.Rows[0].Cells[2].Value;
                if (totalValueCell != null && decimal.TryParse(totalValueCell.ToString(), out decimal totalValue))
                {
                    // Calculate and update budget amount
                    decimal budgetAmount = totalValue * avgCost;
                    UpdateBudgetAmount(budgetAmount.ToString("N2"));

                    // Update the product's budget amount in the list
                    if (currentProductIndex >= 0 && currentProductIndex < selectedProducts.Count)
                    {
                        selectedProducts[currentProductIndex].BudgetAmount = budgetAmount.ToString("N2");
                        selectedProducts[currentProductIndex].SystemValue = systemValue.ToString();
                        selectedProducts[currentProductIndex].TotalValue = totalValue.ToString();
                    }
                }
            }
            }

        private void PopulateFormWithSelectedData(int index)
        {
            if (index >= 0 && index < selectedProducts.Count)
            {
                var product = selectedProducts[index];

                nameTextBox.Text = product.Description;
                string formattedAverageDailySales = _avgDailySalesComputation.ComputeAverageDailySalesFromValue(product.AverageDaily);
                avgTextBox.Text = formattedAverageDailySales;
                qtyTextBox.Text = product.QuantityOnHand.ToString("N0");
                daysTGTextBox.Text = product.DaysToGo;
                overShortSTextBox.Text = product.OverShortStocks;
                AVGPriceTextBox.Text = product.AverageCost.ToString("N2");
                remarksTextBox.Text = product.Remarks ?? "";

                limitSelectionDropdown.Values.Text = product.LimitSelection;
                monthlyDropdown.Values.Text = product.IsThreeMonthPeriod ? "3 Months" : "6 Months";

                _gridViewDesign.SetLimitSelection(product.LimitSelection);
                _gridViewDesign.UpdateSystemValue(product.LimitSelection, product.AverageDaily);
                _gridViewDesign.SetUserInput(product.UserValue ?? "0");
                _gridViewDesign.SetAverageCost(product.AverageCost);

                budgetAmountTextBox.Text = product.BudgetAmount ?? "0.00";
            }
        }
        private void ProductListSelection_SelectionChanged(object sender, EventArgs e)
        {
            if (productListSelection.SelectedRows.Count > 0)
            {
                // Save current form values before switching
                SaveCurrentFormValues();

                // Update current index and populate form
                currentProductIndex = productListSelection.SelectedRows[0].Index;
                PopulateFormWithSelectedData(currentProductIndex);
            }
        }

        private void InitializeEvents()
        {
            limitSelectionDropdown.TextChanged += LimitSelectionDropdown_TextChanged;
            _gridViewDesign.OnBudgetCalculated += UpdateBudgetAmount;

            MonthsDropdownManager.InitializeMonthlyDropdown(monthlyDropdown, (isThreeMonths) => {
                if (currentProductIndex >= 0 && currentProductIndex < selectedProducts.Count)
                {
                    selectedProducts[currentProductIndex].IsThreeMonthPeriod = isThreeMonths;
                    UpdateAverageDailySalesCalculation(isThreeMonths);
                }
            });
        }
        private void InitializeProductListSelection()
        {
            // Configure productListSelection DataGridView
            productListSelection.AutoGenerateColumns = false;
            productListSelection.AllowUserToAddRows = false;
            productListSelection.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            productListSelection.MultiSelect = false;

            // Add only Description column
            var descColumn = new DataGridViewTextBoxColumn
            {
                Name = "Description",
                HeaderText = "Selected Products",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            productListSelection.Columns.Add(descColumn);

            // Handle selection changed event
            productListSelection.SelectionChanged += ProductListSelection_SelectionChanged;
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
        private void SaveCurrentFormValues()
        {
            if (currentProductIndex >= 0 && currentProductIndex < selectedProducts.Count)
            {
                var product = selectedProducts[currentProductIndex];

                if (_gridViewDesign._dataGridView.Rows.Count > 0)
                {
                    product.SystemValue = _gridViewDesign._dataGridView.Rows[0].Cells[0].Value?.ToString() ?? "0";
                    product.UserValue = _gridViewDesign._dataGridView.Rows[0].Cells[1].Value?.ToString() ?? "0";
                    product.TotalValue = _gridViewDesign._dataGridView.Rows[0].Cells[2].Value?.ToString() ?? "0";
                }

                product.BudgetAmount = budgetAmountTextBox.Text;
                product.Remarks = remarksTextBox.Text;
                product.LimitSelection = limitSelectionDropdown.Values.Text;
                product.IsThreeMonthPeriod = monthlyDropdown.Values.Text == "3 Months";

                selectedProducts[currentProductIndex] = product;
            }
        }
        private void doneButton_Click(object sender, EventArgs e)
        {
            try
            {
                EnableEdit();
                // Save the current form values before processing
                SaveCurrentFormValues();

                // If we're editing a single product
                if (_editingProduct != null && _editingIndex.HasValue)
                {
                    var productData = new ProductData
                    {
                        Description = nameTextBox.Text,
                        BarCode = _editingProduct.BarCode, // Preserve original barcode
                        AverageDaily = _avgDailySalesComputation.ComputeAverageDailySalesFromValue(_currentAverageDailySales),
                        PrefVendor = _editingProduct.PrefVendor, // Preserve original vendor
                        QuantityOnHand = qtyTextBox.Text,
                        DaysToGo = daysTGTextBox.Text,
                        OverShortStocks = overShortSTextBox.Text,
                        PurchaseLimit = _gridViewDesign._dataGridView.Rows[0].Cells[2].Value?.ToString() ?? "0",
                        AveragePrice = AVGPriceTextBox.Text,
                        BudgetAmount = budgetAmountTextBox.Text,
                        Remarks = remarksTextBox.Text
                    };

                    // Use existing UpdateProduct method for editing
                    ProductDataManager.UpdateProduct(productData, _editingIndex.Value);
                }
                else
                {
                    // Original logic for adding multiple products
                    foreach (var product in selectedProducts)
                    {
                        var productData = new ProductData
                        {
                            Description = product.Description,
                            BarCode = product.BarCode,
                            AverageDaily = _avgDailySalesComputation.ComputeAverageDailySalesFromValue(product.AverageDaily),
                            PrefVendor = product.PrefVendor,
                            QuantityOnHand = product.QuantityOnHand.ToString("N0"),
                            DaysToGo = product.DaysToGo,
                            OverShortStocks = product.OverShortStocks,
                            PurchaseLimit = product.TotalValue ?? "0",
                            AveragePrice = product.AverageCost.ToString("N2"),
                            BudgetAmount = product.BudgetAmount ?? "0.00",
                            Remarks = product.Remarks ?? ""
                        };

                        ProductDataManager.AddProduct(productData);
                    }
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing products: " + ex.Message);
            }
        }
        public void EnableEdit()
        {
            selectProductButton.Visible = true;
            productListSelection.Visible = true;
            limitSelectionDropdown.Enabled = true;
            monthlyDropdown.Enabled = true;
        }

        public void DisableEdit()
        {
            selectProductButton.Visible = false;
            productListSelection.Visible = false;
            limitSelectionDropdown.Enabled = false;
            monthlyDropdown.Enabled = false;
        }

        private void selectProductButton_Click(object sender, EventArgs e)
        {
            try
            {
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
                if (currentProductIndex >= 0 && currentProductIndex < selectedProducts.Count)
                {
                    var product = selectedProducts[currentProductIndex];
                    product.LimitSelection = limitSelectionDropdown.Values.Text;

                    string numericPart = new string(avgTextBox.Text.TakeWhile(c => char.IsDigit(c) || c == '.').ToArray());
                    if (decimal.TryParse(numericPart, out decimal averageDailySales))
                    {
                        _currentAverageDailySales = averageDailySales;
                        _gridViewDesign.SetLimitSelection(limitSelectionDropdown.Values.Text);
                        _gridViewDesign.UpdateSystemValue(limitSelectionDropdown.Values.Text, averageDailySales);

                        if (_gridViewDesign._dataGridView.Rows.Count > 0)
                        {
                            product.SystemValue = _gridViewDesign._dataGridView.Rows[0].Cells[0].Value?.ToString() ?? "0";
                            product.TotalValue = _gridViewDesign._dataGridView.Rows[0].Cells[2].Value?.ToString() ?? "0";
                        }
                    }

                    selectedProducts[currentProductIndex] = product;
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
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
        }
        private void avgTextBox_TextChanged(object sender, EventArgs e)
        {
        }
        private void qtyTextBox_TextChanged(object sender, EventArgs e)
        {
        }
        private void budgetAmountTextBox_TextChanged(object sender, EventArgs e)
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