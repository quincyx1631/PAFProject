using Krypton.Toolkit;
using PAFProject.Computations;
using System.Numerics;

public class PurchaseLimitGridViewDesign
{
    private readonly KryptonDataGridView _dataGridView;
    private decimal _currentAverageDailySales = 0;
    private decimal _averageCost = 0;  // Add this field
    private readonly PurchaseLimitComputation _purchaseLimitComputation;
    public decimal totalBudget = 0;

    // Add the event declaration
    public event Action<string> OnBudgetCalculated;

    public PurchaseLimitGridViewDesign(KryptonDataGridView dataGridView)
    {
        _dataGridView = dataGridView;
        _purchaseLimitComputation = new PurchaseLimitComputation();
        InitializeGridView();
    }

    private void InitializeGridView()
    {
        // Configure grid properties
        _dataGridView.ColumnHeadersHeight = 35;
        _dataGridView.MultiSelect = false;
        _dataGridView.RowHeadersVisible = false;
        _dataGridView.RowHeadersWidth = 45;
        _dataGridView.ScrollBars = ScrollBars.None;
        _dataGridView.Size = new System.Drawing.Size(279, 61);
        _dataGridView.BorderStyle = BorderStyle.None;

        // Configure columns
        if (_dataGridView.Columns.Count == 0)
        {
            _dataGridView.Columns.Add(CreateColumn("Column1", "System"));
            _dataGridView.Columns.Add(CreateColumn("Column2", "User"));
            _dataGridView.Columns.Add(CreateColumn("Column3", "Total"));
        }

        // Configure column properties
        _dataGridView.Columns[0].ReadOnly = true;  // System column
        _dataGridView.Columns[1].ReadOnly = false; // User column
        _dataGridView.Columns[2].ReadOnly = true;  // Total column

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
        if (_dataGridView.CurrentCell.ColumnIndex == 1) // User column
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
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    private void KryptonDataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == 1) // User column
        {
            UpdateTotal();
        }
    }

    public void UpdateTotal()
    {
        try
        {
            string systemValueStr = _dataGridView.Rows[0].Cells[0].Value?.ToString() ?? "0";
            string userValueStr = _dataGridView.Rows[0].Cells[1].Value?.ToString() ?? "0";

            if (int.TryParse(systemValueStr, out int systemValue) &&
                int.TryParse(userValueStr, out int userValue))
            {
                int total = systemValue + userValue;
                _dataGridView.Rows[0].Cells[2].Value = total.ToString();

                // Calculate budget
                var budgetComputation = new BudgetComputation();
                string budget = budgetComputation.ComputeBudget(total, _averageCost);
                OnBudgetCalculated?.Invoke(budget);
            }
        }
        catch
        {
            _dataGridView.Rows[0].Cells[2].Value = "0";
            OnBudgetCalculated?.Invoke("0.00");
        }
    }

    public void SetAverageCost(decimal averageCost)
    {
        _averageCost = averageCost;
        UpdateTotal(); // Recalculate budget with new average cost
    }

    public void UpdateSystemValue(string limitSelectionText, decimal averageDailySales)
    {
        try
        {
            int days = _purchaseLimitComputation.GetDaysFromSelection(limitSelectionText);
            int purchaseLimit = _purchaseLimitComputation.ComputePurchaseLimit(averageDailySales, days);
            _dataGridView.Rows[0].Cells[0].Value = purchaseLimit.ToString();
            UpdateTotal();
        }
        catch
        {
            _dataGridView.Rows[0].Cells[0].Value = "0";
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