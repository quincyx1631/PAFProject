//PurchaseLimitGridViewDesign.cs
using Krypton.Toolkit;
using PAFProject.Computations;
using System.Diagnostics;

public class PurchaseLimitGridViewDesign
{
    public readonly KryptonDataGridView _dataGridView;
    private decimal _currentAverageDailySales = 0;
    private decimal _averageCost = 0;
    private decimal _quantityOnHand = 0;
    private readonly PurchaseLimitComputation _purchaseLimitComputation;
    private string _userInput = "0";
    private string _currentLimitSelection = "7 Days";

    public event Action<string> OnBudgetCalculated;

    public PurchaseLimitGridViewDesign(KryptonDataGridView dataGridView)
    {
        _dataGridView = dataGridView;
        _purchaseLimitComputation = new PurchaseLimitComputation();
        InitializeGridView();
    }

    public void SetLimitSelection(string limitSelection)
    {
        _currentLimitSelection = limitSelection;
    }

    public void SetUserInput(string userValue)
    {
        _userInput = userValue;
        if (_dataGridView.Rows.Count > 0)
        {
            _dataGridView.Rows[0].Cells[2].Value = userValue;
            UpdateTotal();
        }
    }

    public void SetAverageCost(decimal averageCost)
    {
        _averageCost = averageCost;
        UpdateTotal();
    }
    public void SetQuantityOnHand(decimal quantityOnHand)
    {
        _quantityOnHand = quantityOnHand;
        UpdateAllowedPurchase();
    }
    private void InitializeGridView()
    {
        // Configure grid properties
        _dataGridView.ColumnHeadersHeight = 35;
        _dataGridView.MultiSelect = false;
        _dataGridView.RowHeadersVisible = false;
        _dataGridView.RowHeadersWidth = 45;
        _dataGridView.ScrollBars = ScrollBars.None;
        _dataGridView.BorderStyle = BorderStyle.None;

        // Configure columns
        if (_dataGridView.Columns.Count == 0)
        {
            _dataGridView.Columns.Add(CreateColumn("Column1", "System"));
            _dataGridView.Columns.Add(CreateColumn("Column2", "Allowed Purchase"));
            _dataGridView.Columns.Add(CreateColumn("Column3", "User"));
            _dataGridView.Columns.Add(CreateColumn("Column4", "Total"));
        }

        // Align text in all columns to the right
        _dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        _dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        _dataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        _dataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        // Configure column properties
        _dataGridView.Columns[0].ReadOnly = true;  // System column
        _dataGridView.Columns[1].ReadOnly = true;  // Allowed Purchase column
        _dataGridView.Columns[2].ReadOnly = false; // User column
        _dataGridView.Columns[3].ReadOnly = true;  // Total column

        // Add a new row if none exists
        if (_dataGridView.Rows.Count == 0)
        {
            _dataGridView.Rows.Add();
        }

        // Wire up events
        _dataGridView.CellEndEdit += KryptonDataGridView1_CellEndEdit;
        _dataGridView.EditingControlShowing += KryptonDataGridView1_EditingControlShowing;
        _dataGridView.LostFocus += KryptonDataGridView1_LostFocus;
        _dataGridView.KeyDown += KryptonDataGridView1_KeyDown;
    }
    public void InitializeWithExistingData(string purchaseLimit)
    {
        if (_dataGridView.Rows.Count > 0)
        {
            // Set the existing purchase limit as the system value
            _dataGridView.Rows[0].Cells[0].Value = purchaseLimit;
            // Calculate Allowed Purchase (System - QuantityOnHand)
            UpdateAllowedPurchase();
            // Set user input to 0 initially
            _dataGridView.Rows[0].Cells[2].Value = "0";
            // Set total to match the purchase limit initially
            UpdateTotal();
            _userInput = "0"; // Reset user input
        }
    }
    private void UpdateAllowedPurchase()
    {
        try
        {
            string systemValueStr = _dataGridView.Rows[0].Cells[0].Value?.ToString() ?? "0";

            if (int.TryParse(systemValueStr, out int systemValue))
            {
                // Calculate allowed purchase (System - QuantityOnHand)
                int allowedPurchase = Math.Max(0, systemValue - (int)_quantityOnHand);
                _dataGridView.Rows[0].Cells[1].Value = allowedPurchase.ToString();
            }
            else
            {
                _dataGridView.Rows[0].Cells[1].Value = "0";
            }
        }
        catch
        {
            _dataGridView.Rows[0].Cells[1].Value = "0";
        }
    }
    private void KryptonDataGridView1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            UpdateTotal();
            e.Handled = true;
            e.SuppressKeyPress = true;
            // Move focus to next control
            _dataGridView.Parent.SelectNextControl(_dataGridView, true, true, true, true);
        }
    }

    private void KryptonDataGridView1_LostFocus(object sender, EventArgs e)
    {
        UpdateTotal();
    }

    private void KryptonDataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        if (_dataGridView.CurrentCell.ColumnIndex == 2) // User column (now index 2)
        {
            if (e.Control is TextBox tb)
            {
                tb.KeyPress -= TextBox_KeyPress;
                tb.KeyPress += TextBox_KeyPress;
            }
        }
    }

    private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        // Allow negative sign, digits, and control characters (like backspace)
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
        {
            e.Handled = true;
        }

        // Allow only one negative sign at the beginning
        if (e.KeyChar == '-' && (sender as TextBox)?.Text.Contains('-') == true)
        {
            e.Handled = true;
        }

        // Only allow negative sign at the beginning of the number
        if (e.KeyChar == '-' && (sender as TextBox)?.SelectionStart != 0)
        {
            e.Handled = true;
        }
    }

    private void KryptonDataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == 2) // User column (now index 2)
        {
            UpdateTotal();
        }
    }

    private void UpdateTotal()
    {
        try
        {
            string allowedPurchaseStr = _dataGridView.Rows[0].Cells[1].Value?.ToString() ?? "0";
            string userValueStr = _dataGridView.Rows[0].Cells[2].Value?.ToString() ?? "0";

            if (int.TryParse(allowedPurchaseStr, out int allowedPurchase) &&
                int.TryParse(userValueStr, out int userValue))
            {
                _userInput = userValue.ToString();
                int total = Math.Max(0, allowedPurchase + userValue);
                _dataGridView.Rows[0].Cells[3].Value = total.ToString();

                var budgetComputation = new BudgetComputation();
                string budget = budgetComputation.ComputeBudget(total, _averageCost);
                OnBudgetCalculated?.Invoke(budget);
            }
        }
        catch
        {
            _dataGridView.Rows[0].Cells[3].Value = "0";
            OnBudgetCalculated?.Invoke("0.00");
        }
    }

    public void UpdateSystemValue(string limitSelectionText, decimal averageDailySales)
    {
        try
        {
            _currentAverageDailySales = averageDailySales;
            _currentLimitSelection = limitSelectionText;

            int days = _purchaseLimitComputation.GetDaysFromSelection(limitSelectionText);
            int newPurchaseLimit = _purchaseLimitComputation.ComputePurchaseLimit(averageDailySales, days);

            // Debugging line: Verify values being computed
            Debug.WriteLine($"Recomputing with {days} days, Avg Sales: {averageDailySales}, New Limit: {newPurchaseLimit}");

            _dataGridView.Rows[0].Cells[0].Value = newPurchaseLimit.ToString();
            _dataGridView.Rows[0].Cells[2].Value = _userInput;

            UpdateAllowedPurchase();
            UpdateTotal();
        }
        catch
        {
            _dataGridView.Rows[0].Cells[0].Value = "0";
            _dataGridView.Rows[0].Cells[1].Value = "0";
            _dataGridView.Rows[0].Cells[2].Value = _userInput;
            UpdateTotal();
        }
    }


    private DataGridViewTextBoxColumn CreateColumn(string name, string headerText)
    {
        return new DataGridViewTextBoxColumn
        {
            HeaderText = headerText,
            Name = name
        };
    }
}