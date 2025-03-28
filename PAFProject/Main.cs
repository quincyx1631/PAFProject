using MaterialSkin.Controls;
using PAFProject.Computations;
using PAFProject.Design;
using PAFProject.Export;
using PAFProject.Forms;
using PAFProject.Models;
using PAFProject.Properties;

namespace PAFProject
{
    public partial class Main : MaterialForm
    {
        private int selectedRowIndex = -1;
        private BranchDropdownManager _branchDropdownManager;
        private readonly Select_Product_Form _editing;
        private readonly Select_Product_Form editForm;
        public Main()
        {
            InitializeComponent();
            weeklyBudgetTextBox.Text = Settings.Default.WeeklyBudget.ToString("N2");
            selectButton.Click += new System.EventHandler(this.selectButton_Click);

            dateTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");

            ProductDataManager.OnProductAdded += HandleNewProduct;
            ProductDataManager.OnProposedBudget += UpdateProposedBudget;
            processButton.Click += new System.EventHandler(this.ProcessButton_Click);

            weeklyBudgetTextBox.TextChanged += new System.EventHandler(this.weeklyBudgetTextBox_TextChanged);

            InitializeDataGridView();

            kryptonDataGridView1.CellDoubleClick += KryptonDataGridView1_CellDoubleClick;
            kryptonDataGridView1.CellClick += KryptonDataGridView1_CellClick;
            deleteButton.Click += DeleteButton_Click;
            _branchDropdownManager = new BranchDropdownManager(branchSelect, branchNameLabel);

            PDFButton.Click += new System.EventHandler(this.pdfButton_Click);

            proposedBudgetTextBox.TextChanged += new System.EventHandler(this.proposedBudgetTextBox_TextChanged);
            this.Load += new System.EventHandler(this.Main_Load);
            CalculateShortOver();
        }
        public void RefreshBranchList()
        {
            _branchDropdownManager = new BranchDropdownManager(branchSelect, branchNameLabel);
        }
        private void InitializeDataGridView()
        {
            kryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            kryptonDataGridView1.AllowUserToAddRows = false;
            kryptonDataGridView1.AllowUserToDeleteRows = false;
            kryptonDataGridView1.ReadOnly = true;
            kryptonDataGridView1.RowHeadersVisible = false;
            kryptonDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            kryptonDataGridView1.MultiSelect = false;
            kryptonDataGridView1.AutoGenerateColumns = false;
            kryptonDataGridView1.ScrollBars = ScrollBars.Both;

            kryptonDataGridView1.Rows.Clear();

            // Center-align all column headers
            foreach (DataGridViewColumn column in kryptonDataGridView1.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            kryptonDataGridView1.CellFormatting += KryptonDataGridView1_CellFormatting;
            kryptonDataGridView1.ClearSelection();
        }
        private void KryptonDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // List of column indexes to check for negative values
            int[] targetColumns = { 4, 5, 6 }; // <-- example: QuantityOnHand, DaysToGo, OverShortStocks, BudgetAmount, etc.

            if (targetColumns.Contains(e.ColumnIndex) && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal value))
                {
                    if (value < 0)
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                }
            }
        }
        private void KryptonDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRowIndex = e.RowIndex;
                var currentRow = kryptonDataGridView1.Rows[e.RowIndex];

                // Get the value based on the purchase limit selection
                string limitSelection = currentRow.Cells[10].Value?.ToString() ?? "7 Days";
                string avgDailyStr = currentRow.Cells[2].Value?.ToString() ?? "0";

                // Extract the numeric part of average daily (in case it has text like "pcs")
                string numericPart = new string(avgDailyStr.TakeWhile(c => char.IsDigit(c) || c == '.').ToArray());

                // Calculate the SystemValue based on the limit selection and average daily sales
                decimal avgDaily = decimal.TryParse(numericPart, out decimal val) ? val : 0;
                var purchaseLimitComputation = new PurchaseLimitComputation();
                int days = purchaseLimitComputation.GetDaysFromSelection(limitSelection);
                string systemValue = purchaseLimitComputation.ComputePurchaseLimit(avgDaily, days).ToString();

                var currentProduct = new ProductData
                {
                    Description = currentRow.Cells[0].Value?.ToString() ?? string.Empty,
                    BarCode = currentRow.Cells[1].Value?.ToString() ?? string.Empty,
                    AverageDaily = avgDailyStr,
                    PrefVendor = currentRow.Cells[3].Value?.ToString() ?? string.Empty,
                    QuantityOnHand = currentRow.Cells[4].Value?.ToString() ?? string.Empty,
                    DaysToGo = currentRow.Cells[5].Value?.ToString() ?? string.Empty,
                    OverShortStocks = currentRow.Cells[6].Value?.ToString() ?? string.Empty,
                    PurchaseLimit7Days = currentRow.Cells[7].Value?.ToString() ?? string.Empty,
                    PurchaseLimit15Days = currentRow.Cells[8].Value?.ToString() ?? string.Empty,
                    PurchaseLimit30Days = currentRow.Cells[9].Value?.ToString() ?? string.Empty,
                    LimitSelection = limitSelection,
                    AllowedPurchase = currentRow.Cells[11].Value?.ToString() ?? string.Empty,
                    UserValue = currentRow.Cells[12].Value?.ToString() ?? string.Empty,
                    PurchaseLimit = currentRow.Cells[13].Value?.ToString() ?? string.Empty, //Total
                    AveragePrice = currentRow.Cells[14].Value?.ToString() ?? string.Empty,
                    BudgetAmount = currentRow.Cells[15].Value?.ToString() ?? string.Empty,
                    Remarks = currentRow.Cells[16].Value?.ToString() ?? string.Empty,
                    SystemValue = systemValue // Add the calculated or retrieved SystemValue here
                };

                Select_Product_Form selectProductForm = new Select_Product_Form(this, currentProduct, selectedRowIndex);
                selectProductForm.Show();
                selectProductForm.DisableEdit();
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
                row.Cells[3].Value = product.PrefVendor;
                row.Cells[4].Value = product.QuantityOnHand;
                row.Cells[5].Value = product.DaysToGo;
                row.Cells[6].Value = product.OverShortStocks;
                row.Cells[7].Value = product.PurchaseLimit7Days;
                row.Cells[8].Value = product.PurchaseLimit15Days;
                row.Cells[9].Value = product.PurchaseLimit30Days;
                row.Cells[10].Value = product.LimitSelection;
                row.Cells[11].Value = product.AllowedPurchase;
                row.Cells[12].Value = product.UserValue;
                row.Cells[13].Value = product.PurchaseLimit;//Total
                row.Cells[14].Value = product.AveragePrice;
                row.Cells[15].Value = product.BudgetAmount;
                row.Cells[16].Value = product.Remarks;
            }
            else
            {
                // Add new row
                kryptonDataGridView1.Rows.Add(
                    product.Description,
                    product.BarCode,
                    product.AverageDaily,
                    product.PrefVendor,
                    product.QuantityOnHand,
                    product.DaysToGo,
                    product.OverShortStocks,
                    product.PurchaseLimit7Days,
                    product.PurchaseLimit15Days,
                    product.PurchaseLimit30Days,
                    product.LimitSelection,
                    product.AllowedPurchase,
                    product.UserValue,
                    product.PurchaseLimit, //Total
                    product.AveragePrice,
                    product.BudgetAmount,
                    product.Remarks
                );
            }
            kryptonDataGridView1.ClearSelection();
            // Align specific columns to the right
            kryptonDataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // AverageDaily
            kryptonDataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // QuantityOnHand
            kryptonDataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // DaysToGo
            kryptonDataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // OverShortStocks
            kryptonDataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // PurchaseLimit7Days
            kryptonDataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // PurchaseLimit15Days
            kryptonDataGridView1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // PurchaseLimit30Days
            kryptonDataGridView1.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // System
            kryptonDataGridView1.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // UserValue
            kryptonDataGridView1.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Total
            kryptonDataGridView1.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // AveragePrice
            kryptonDataGridView1.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // BudgetAmount

            kryptonDataGridView1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // LimitSelection
            // Align remaining columns to the left
            kryptonDataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; // Description
            kryptonDataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; // BarCode
            kryptonDataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Pref Vendor
            kryptonDataGridView1.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; // Remarks
        }
        private async void ProcessButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate TextBoxes
                List<string> emptyFields = new List<string>();

                if (string.IsNullOrWhiteSpace(branchNameLabel.Text))
                    emptyFields.Add("Branch Name");
                if (string.IsNullOrWhiteSpace(weekTextBox.Text))
                    emptyFields.Add("Week");
                if (string.IsNullOrWhiteSpace(weeklyBudgetTextBox.Text))
                    emptyFields.Add("Weekly Budget");
                if (string.IsNullOrWhiteSpace(dateTextBox.Text))
                    emptyFields.Add("Date");
                if (string.IsNullOrWhiteSpace(proposedBudgetTextBox.Text))
                    emptyFields.Add("Proposed Budget");
                if (string.IsNullOrWhiteSpace(shortOverTextBox.Text))
                    emptyFields.Add("Short/Over");

                // Validate DataGridView
                bool hasValidData = false;
                if (kryptonDataGridView1.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in kryptonDataGridView1.Rows)
                    {
                        bool rowHasValue = false;
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                            {
                                rowHasValue = true;
                                break;
                            }
                        }
                        if (rowHasValue)
                        {
                            hasValidData = true;
                            break;
                        }
                    }
                }

                if (!hasValidData)
                    emptyFields.Add("DataGridView (no data available)");

                // Show error if validation fails
                if (emptyFields.Count > 0)
                {
                    string errorMessage = "The following required fields are empty or missing:\n\n";
                    errorMessage += string.Join("\n", emptyFields);
                    errorMessage += "\n\nPlease fill in all required fields before exporting to Excel.";

                    MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Proceed with Excel export if validation passes
                this.Cursor = Cursors.WaitCursor;
                processButton.Enabled = false;
                string branchName = branchNameLabel.Text;
                string week = weekTextBox.Text;
                string date = dateTextBox.Text;
                string weeklyBudget = weeklyBudgetTextBox.Text;
                string proposedBudget = proposedBudgetTextBox.Text;
                string shortOver = shortOverTextBox.Text;
                string defaultFileName = $"PurchaseApprovalForm_{DateTime.Now:yyyyMMdd}.xlsx";
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = defaultFileName
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    await Task.Run(() =>
                    {
                        this.Invoke(new Action(() =>
                        {
                            ExcelExporter.ExportToExcel(kryptonDataGridView1, saveFileDialog.FileName,
                                branchName, week, date, weeklyBudget, proposedBudget, shortOver);
                        }));
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during export: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Invoke(new Action(() =>
                {
                    this.Cursor = Cursors.Default;
                    processButton.Enabled = true;
                }));
            }
        }
        private async void pdfButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate TextBoxes
                List<string> emptyFields = new List<string>();

                if (string.IsNullOrWhiteSpace(branchNameLabel.Text))
                    emptyFields.Add("Branch Name");
                if (string.IsNullOrWhiteSpace(weekTextBox.Text))
                    emptyFields.Add("Week");
                if (string.IsNullOrWhiteSpace(dateTextBox.Text))
                    emptyFields.Add("Date");
                if (string.IsNullOrWhiteSpace(weeklyBudgetTextBox.Text))
                    emptyFields.Add("Weekly Budget");
                if (string.IsNullOrWhiteSpace(proposedBudgetTextBox.Text))
                    emptyFields.Add("Proposed Budget");
                if (string.IsNullOrWhiteSpace(shortOverTextBox.Text))
                    emptyFields.Add("Short/Over");

                // Validate DataGridView
                bool hasValidData = false;
                if (kryptonDataGridView1.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in kryptonDataGridView1.Rows)
                    {
                        bool rowHasValue = false;
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                            {
                                rowHasValue = true;
                                break;
                            }
                        }
                        if (rowHasValue)
                        {
                            hasValidData = true;
                            break;
                        }
                    }
                }

                if (!hasValidData)
                    emptyFields.Add("DataGridView (no data available)");

                // Show error if validation fails
                if (emptyFields.Count > 0)
                {
                    string errorMessage = "The following required fields are empty or missing:\n\n";
                    errorMessage += string.Join("\n", emptyFields);
                    errorMessage += "\n\nPlease fill in all required fields before exporting to PDF.";

                    MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Proceed with PDF export if validation passes
                this.Cursor = Cursors.WaitCursor;
                PDFButton.Enabled = false;
                string branchName = branchNameLabel.Text;
                string week = weekTextBox.Text;
                string date = dateTextBox.Text;
                string weeklyBudget = weeklyBudgetTextBox.Text;
                string proposedBudget = proposedBudgetTextBox.Text;
                string shortOver = shortOverTextBox.Text;
                string defaultFileName = $"PurchaseApprovalForm_{DateTime.Now:yyyyMMdd}.pdf";
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = defaultFileName
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    await Task.Run(() =>
                    {
                        this.Invoke(new Action(() =>
                        {
                            PdfExporter.ExportToPdf(kryptonDataGridView1, saveFileDialog.FileName,
                                branchName, week, date, weeklyBudget, proposedBudget, shortOver);
                        }));
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during export: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Invoke(new Action(() =>
                {
                    this.Cursor = Cursors.Default;
                    PDFButton.Enabled = true;
                }));
            }
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
                selectProductForm.EnableEdit();
                selectProductForm.ShowDialog();
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
                // Remove any existing commas from the input
                string cleanInput = weeklyBudgetTextBox.Text.Replace(",", "");

                if (decimal.TryParse(cleanInput, out decimal weeklyBudgetAmount))
                {
                    // Store the cursor position
                    int cursorPosition = weeklyBudgetTextBox.SelectionStart;

                    // Update Settings with new value
                    Properties.Settings.Default.WeeklyBudget = weeklyBudgetAmount;
                    Properties.Settings.Default.Save();

                    // Format the number with commas and two decimal places
                    weeklyBudgetTextBox.Text = weeklyBudgetAmount.ToString("N2");

                    // Restore cursor position, accounting for added characters
                    weeklyBudgetTextBox.SelectionStart = cursorPosition;

                    CalculateShortOver();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter a valid number.");
                weeklyBudgetTextBox.Text = Properties.Settings.Default.WeeklyBudget.ToString("N2");
                CalculateShortOver();
            }
        }
        private void CalculateShortOver()
        {
            try
            {
                // Get the weekly budget from settings
                decimal weeklyBudget = Properties.Settings.Default.WeeklyBudget;

                // Get the proposed budget
                decimal totalBudget = 0;
                if (decimal.TryParse(proposedBudgetTextBox.Text.Replace(",", ""), out decimal proposedBudget))
                {
                    totalBudget = proposedBudget;
                }

                // Calculate the difference
                decimal difference = weeklyBudget - totalBudget;

                // Update the shortOverTextBox
                shortOverTextBox.Text = difference.ToString("N2");
            }
            catch
            {
                shortOverTextBox.Text = "0.00";
            }
        }
        private void proposedBudgetTextBox_TextChanged(object sender, EventArgs e)
        {
            CalculateShortOver();
        }
        private void KryptonDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRowIndex = e.RowIndex;
                deleteButton.Visible = true;
            }
            else
            {
                selectedRowIndex = -1;
                deleteButton.Visible = false;
            }
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0 && selectedRowIndex < kryptonDataGridView1.Rows.Count)
            {
                // Remove the selected row
                kryptonDataGridView1.Rows.RemoveAt(selectedRowIndex);
                selectedRowIndex = -1;
                deleteButton.Visible = false;

                UpdateTotalBudget();
            }
        }
        private void UpdateTotalBudget()
        {
            decimal totalBudget = 0;

            foreach (DataGridViewRow row in kryptonDataGridView1.Rows)
            {
                if (row.Cells[9].Value != null && decimal.TryParse(row.Cells[9].Value.ToString(), out decimal budget))
                {
                    totalBudget += budget;
                }
            }

            // Update the proposed budget text box
            proposedBudgetTextBox.Text = totalBudget.ToString("N2");

            // Trigger weekly budget computation
            weeklyBudgetTextBox_TextChanged(this, EventArgs.Empty);
        }
        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}