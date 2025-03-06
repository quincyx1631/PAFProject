//Select_Product_Form.cs
using MaterialSkin.Controls;
using PAFProject.Computations;
using PAFProject.Design;
using PAFProject.Models;
using System.Text;
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
        private bool IsQuantityExceedingLimit(decimal quantityOnHand, int systemValue)
        {
            return quantityOnHand > systemValue;
        }
        private string GenerateWarningMessage(decimal quantityOnHand, int systemValue, string allowedPurchase)
        {
            // If the user has manually set an Allowed Purchase value that's not zero
            if (!string.IsNullOrEmpty(allowedPurchase) && decimal.TryParse(allowedPurchase, out decimal allowedValue) && allowedValue > 0)
            {
                // Don't show warning when user has manually set an Allowed Purchase
                return "";
            }

            if (IsQuantityExceedingLimit(quantityOnHand, systemValue))
            {
                return "WARNING: You have enough STOCKS ON HAND, It exceeds purchase limit";
            }

            return "";
        }
        public void AddSelectedProduct(string description, decimal avgDaily, decimal qtyOnHand,
    string daysToGo, string overShort, decimal avgCost, string barCode, string prefVendor)
        {
            // First clear any existing remarks/warnings since we're adding a new or updating a product
            remarksTextBox.Text = "";
            remarksTextBox.ForeColor = SystemColors.WindowText;

            // Default for new products
            string limitSelectionValue = "7 Days";

            int existingIndex = selectedProducts.FindIndex(p => p.Description == description);
            if (existingIndex >= 0)
            {
                // For existing products, preserve their limit selection
                limitSelectionValue = selectedProducts[existingIndex].LimitSelection;
            }

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
                LimitSelection = limitSelectionValue,
                IsThreeMonthPeriod = monthlyDropdown.Values.Text == "3 Months"
            };

            if (existingIndex >= 0)
            {
                product.UserValue = selectedProducts[existingIndex].UserValue;
                product.SystemValue = selectedProducts[existingIndex].SystemValue;
                product.AllowedPurchase = selectedProducts[existingIndex].AllowedPurchase;
                product.TotalValue = selectedProducts[existingIndex].TotalValue;
                product.BudgetAmount = selectedProducts[existingIndex].BudgetAmount;
                product.Remarks = selectedProducts[existingIndex].Remarks;

                selectedProducts[existingIndex] = product;
                productListSelection.Rows[existingIndex].Cells[0].Value = description;
                currentProductIndex = existingIndex;
            }
            else
            {
                limitSelectionDropdown.Values.Text = "7 Days";
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

            // Update the grid with the system value and quantity on hand
            _gridViewDesign.SetQuantityOnHand(qtyOnHand);
            _gridViewDesign.UpdateSystemValue(limitSelectionDropdown.Values.Text, avgDaily);
            _gridViewDesign.SetAverageCost(avgCost);

            // Get the total value from the grid after it's updated
            if (_gridViewDesign._dataGridView.Rows.Count > 0)
            {
                var totalValueCell = _gridViewDesign._dataGridView.Rows[0].Cells[3].Value; // Now column 3 is Total
                if (totalValueCell != null && decimal.TryParse(totalValueCell.ToString(), out decimal totalValue))
                {
                    // Calculate and update budget amount
                    decimal budgetAmount = totalValue * avgCost;
                    UpdateBudgetAmount(budgetAmount.ToString("N2"));

                    // Update the product's values in the list
                    if (currentProductIndex >= 0 && currentProductIndex < selectedProducts.Count)
                    {
                        var allowedPurchaseCell = _gridViewDesign._dataGridView.Rows[0].Cells[1].Value;
                        string allowedPurchaseValue = allowedPurchaseCell?.ToString() ?? "0";

                        selectedProducts[currentProductIndex].BudgetAmount = budgetAmount.ToString("N2");
                        selectedProducts[currentProductIndex].SystemValue = systemValue.ToString();
                        selectedProducts[currentProductIndex].AllowedPurchase = allowedPurchaseValue;
                        selectedProducts[currentProductIndex].TotalValue = totalValue.ToString();

                        // Check if we need to add a warning message, considering the allowed purchase value
                        string warningMessage = GenerateWarningMessage(qtyOnHand, systemValue, allowedPurchaseValue);
                        if (!string.IsNullOrEmpty(warningMessage))
                        {
                            // Add the warning to remarks
                            remarksTextBox.Text = warningMessage;
                            selectedProducts[currentProductIndex].Remarks = warningMessage;
                            remarksTextBox.ForeColor = Color.Red;
                        }
                        else if (remarksTextBox.Text.Contains("WARNING:"))
                        {
                            // Clear the warning if it exists
                            remarksTextBox.Text = remarksTextBox.Text.Replace("WARNING: You have enough STOCKS ON HAND, It exceeds purchase limit", "").Trim();
                            remarksTextBox.ForeColor = SystemColors.WindowText;
                            selectedProducts[currentProductIndex].Remarks = remarksTextBox.Text;
                        }
                    }
                }
            }
            limitSelectionDropdown.Values.Text = product.LimitSelection;
        }

        private void PopulateFormWithSelectedData(int index)
        {
            if (index >= 0 && index < selectedProducts.Count)
            {
                var product = selectedProducts[index];
                // Use the precise average daily sales value
                _currentAverageDailySales = _avgDailySalesComputation.GetPreciseAverageDailySales(product.Description);
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
                _gridViewDesign.SetQuantityOnHand(product.QuantityOnHand);
                _gridViewDesign.UpdateSystemValue(product.LimitSelection, product.AverageDaily);
                _gridViewDesign.SetUserInput(product.UserValue ?? "0");
                _gridViewDesign.SetAverageCost(product.AverageCost);

                // Get the current system value and check if warning is needed
                string systemValueStr = _gridViewDesign._dataGridView.Rows[0].Cells[0].Value?.ToString() ?? "0";
                string allowedPurchaseValue = _gridViewDesign._dataGridView.Rows[0].Cells[1].Value?.ToString() ?? "0";

                if (int.TryParse(systemValueStr, out int systemValue))
                {
                    string warningMessage = GenerateWarningMessage(product.QuantityOnHand, systemValue, allowedPurchaseValue);

                    // Clear any existing warning regardless of the outcome
                    if (remarksTextBox.Text.Contains("WARNING: You have enough STOCKS ON HAND"))
                    {
                        remarksTextBox.Text = remarksTextBox.Text.Replace("WARNING: You have enough STOCKS ON HAND, It exceeds purchase limit\n\n", "")
                                                               .Replace("WARNING: You have enough STOCKS ON HAND, It exceeds purchase limit", "")
                                                               .Trim();
                    }

                    if (!string.IsNullOrEmpty(warningMessage))
                    {
                        // Add the warning to remarks if not already present
                        remarksTextBox.Text = warningMessage +
                            (string.IsNullOrEmpty(remarksTextBox.Text) ? "" : "\n\n" + remarksTextBox.Text);

                        // Highlight the warning message visually
                        remarksTextBox.ForeColor = Color.Red;
                    }
                    else
                    {
                        // Reset color if there's no warning
                        remarksTextBox.ForeColor = SystemColors.WindowText;
                    }
                }

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

            if (string.IsNullOrEmpty(limitSelectionDropdown.Values.Text))
            {
                limitSelectionDropdown.Values.Text = "7 Days";
            }

            MonthsDropdownManager.InitializeMonthlyDropdown(monthlyDropdown, (isThreeMonths) =>
            {
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

        private List<string> GetProductsWithWarnings()
        {
            List<string> productsWithWarnings = new List<string>();

            foreach (var product in selectedProducts)
            {
                // Check if there's a non-zero allowed purchase value
                if (!string.IsNullOrEmpty(product.AllowedPurchase) &&
                    decimal.TryParse(product.AllowedPurchase, out decimal allowedValue) &&
                    allowedValue > 0)
                {
                    // Skip warning check if allowed purchase is set
                    continue;
                }

                if (int.TryParse(product.SystemValue, out int systemValue) &&
                    IsQuantityExceedingLimit(product.QuantityOnHand, systemValue))
                {
                    productsWithWarnings.Add(product.Description);
                }
            }

            return productsWithWarnings;
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

            _gridViewDesign.InitializeWithExistingData(_editingProduct.PurchaseLimit);

            if (decimal.TryParse(_editingProduct.AverageDaily, out decimal avgDaily))
            {
                _currentAverageDailySales = avgDaily;
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
                    product.AllowedPurchase = _gridViewDesign._dataGridView.Rows[0].Cells[1].Value?.ToString() ?? "0";
                    product.UserValue = _gridViewDesign._dataGridView.Rows[0].Cells[2].Value?.ToString() ?? "0";
                    product.TotalValue = _gridViewDesign._dataGridView.Rows[0].Cells[3].Value?.ToString() ?? "0";
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
                this.Cursor = Cursors.WaitCursor;

                SaveCurrentFormValues();
                List<string> productsWithWarnings = GetProductsWithWarnings();

                if (productsWithWarnings.Count > 0)
                {
                    // Build the warning message
                    StringBuilder warningMessage = new StringBuilder();
                    warningMessage.AppendLine("The following products have enough stocks quantity exceeding purchase limit:");
                    warningMessage.AppendLine();

                    foreach (var product in productsWithWarnings)
                    {
                        warningMessage.AppendLine("• " + product);
                    }

                    warningMessage.AppendLine();
                    warningMessage.Append("Do you want to continue?");

                    // Show confirmation dialog
                    DialogResult result = MessageBox.Show(
                        warningMessage.ToString(),
                        "Warning: Stocks Quantity Exceeds Purchase Limit, You still have enough stocks!",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Cancel)
                    {
                        this.Cursor = Cursors.Default;
                        return; // Exit without saving
                    }
                }

                doneButton.Enabled = false;
                EnableEdit();

                // If we're editing a single product
                if (_editingProduct != null && _editingIndex.HasValue)
                {
                    var productData = new ProductData
                    {
                        Description = nameTextBox.Text,
                        BarCode = _editingProduct.BarCode,
                        AverageDaily = _editingProduct.AverageDaily,
                        PrefVendor = _editingProduct.PrefVendor,
                        QuantityOnHand = qtyTextBox.Text,
                        DaysToGo = daysTGTextBox.Text,
                        OverShortStocks = overShortSTextBox.Text,
                        PurchaseLimit = _gridViewDesign._dataGridView.Rows[0].Cells[3].Value?.ToString() ?? "0", // Now column 3 is Total
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
                            PurchaseLimit = product.TotalValue ?? "0", // This should now be the value from column 3
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
            finally
            {
                this.Cursor = Cursors.Default;
                doneButton.Enabled = true;
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
                this.Cursor = Cursors.WaitCursor;
                selectProductButton.Enabled = false;

                SaveCurrentFormValues();

                Product_List_Form selectProductListForm = new Product_List_Form(this);
                selectProductListForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                selectProductButton.Enabled = true;

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
                        _gridViewDesign.SetLimitSelection(limitSelectionDropdown.Values.Text);
                        _gridViewDesign.UpdateSystemValue(limitSelectionDropdown.Values.Text, _currentAverageDailySales);

                        if (_gridViewDesign._dataGridView.Rows.Count > 0)
                        {
                            string systemValue = _gridViewDesign._dataGridView.Rows[0].Cells[0].Value?.ToString() ?? "0";
                            string allowedPurchase = _gridViewDesign._dataGridView.Rows[0].Cells[1].Value?.ToString() ?? "0";
                            string totalValue = _gridViewDesign._dataGridView.Rows[0].Cells[3].Value?.ToString() ?? "0";

                            product.SystemValue = systemValue;
                            product.TotalValue = totalValue;

                            // Only update the warning message if needed, considering allowed purchase
                            if (int.TryParse(systemValue, out int sysValue) && decimal.TryParse(qtyTextBox.Text, out decimal qtyOnHand))
                            {
                                string warningMessage = GenerateWarningMessage(qtyOnHand, sysValue, allowedPurchase);

                                // Clear any existing warning
                                if (remarksTextBox.Text.Contains("WARNING: You have enough STOCKS ON HAND"))
                                {
                                    remarksTextBox.Text = remarksTextBox.Text.Replace("WARNING: You have enough STOCKS ON HAND, It exceeds purchase limit\n\n", "")
                                                                       .Replace("WARNING: You have enough STOCKS ON HAND, It exceeds purchase limit", "")
                                                                       .Trim();
                                    remarksTextBox.ForeColor = SystemColors.WindowText;
                                }

                                if (!string.IsNullOrEmpty(warningMessage))
                                {
                                    // Add the new warning
                                    remarksTextBox.Text = warningMessage +
                                        (string.IsNullOrEmpty(remarksTextBox.Text) ? "" : "\n\n" + remarksTextBox.Text);
                                    remarksTextBox.ForeColor = Color.Red;
                                }

                                product.Remarks = remarksTextBox.Text;
                            }
                        }
                    }
                    selectedProducts[currentProductIndex] = product;
                }
                SaveCurrentFormValues();
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

                // Force recalculation of Average Daily Sales
                decimal numericAverageDailySales = _avgDailySalesComputation.GetNumericAverageDailySales(nameTextBox.Text);
                string formattedAverageDailySales = _avgDailySalesComputation.ComputeAverageDailySalesFromValue(numericAverageDailySales);

                avgTextBox.Text = formattedAverageDailySales;
                _currentAverageDailySales = numericAverageDailySales;

                // Force recalculation of Purchase Limit
                _gridViewDesign.UpdateSystemValue(limitSelectionDropdown.Values.Text, numericAverageDailySales);

                // Refresh dependent calculations
                if (decimal.TryParse(qtyTextBox.Text, out decimal quantityOnHand))
                {
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

                    // Get updated system value from grid after recalculation
                    if (_gridViewDesign._dataGridView.Rows.Count > 0)
                    {
                        string systemValueStr = _gridViewDesign._dataGridView.Rows[0].Cells[0].Value?.ToString() ?? "0";
                        string allowedPurchaseValue = _gridViewDesign._dataGridView.Rows[0].Cells[1].Value?.ToString() ?? "0";

                        if (int.TryParse(systemValueStr, out int systemValue))
                        {
                            // Update warning message based on new values
                            string warningMessage = GenerateWarningMessage(quantityOnHand, systemValue, allowedPurchaseValue);

                            // Clear any existing warning
                            if (remarksTextBox.Text.Contains("WARNING: You have enough STOCKS ON HAND"))
                            {
                                remarksTextBox.Text = remarksTextBox.Text.Replace("WARNING: You have enough STOCKS ON HAND, It exceeds purchase limit\n\n", "")
                                                                   .Replace("WARNING: You have enough STOCKS ON HAND, It exceeds purchase limit", "")
                                                                   .Trim();
                                remarksTextBox.ForeColor = SystemColors.WindowText;
                            }

                            if (!string.IsNullOrEmpty(warningMessage))
                            {
                                // Add the new warning
                                remarksTextBox.Text = warningMessage +
                                    (string.IsNullOrEmpty(remarksTextBox.Text) ? "" : "\n\n" + remarksTextBox.Text);
                                remarksTextBox.ForeColor = Color.Red;
                            }

                            // Update the current product's remarks
                            if (currentProductIndex >= 0 && currentProductIndex < selectedProducts.Count)
                            {
                                selectedProducts[currentProductIndex].Remarks = remarksTextBox.Text;
                                selectedProducts[currentProductIndex].SystemValue = systemValueStr;

                                // Update other calculated values that may have changed
                                var totalValueCell = _gridViewDesign._dataGridView.Rows[0].Cells[3].Value;
                                if (totalValueCell != null)
                                {
                                    selectedProducts[currentProductIndex].TotalValue = totalValueCell.ToString();

                                    // Update budget if needed
                                    if (decimal.TryParse(totalValueCell.ToString(), out decimal totalValue) &&
                                        decimal.TryParse(AVGPriceTextBox.Text, out decimal avgCost))
                                    {
                                        decimal budgetAmount = totalValue * avgCost;
                                        UpdateBudgetAmount(budgetAmount.ToString("N2"));
                                        selectedProducts[currentProductIndex].BudgetAmount = budgetAmount.ToString("N2");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}