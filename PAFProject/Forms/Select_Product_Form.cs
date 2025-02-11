//Select_Product_Form.cs
using MaterialSkin.Controls;
using PAFProject.Design;
namespace PAFProject.Forms
{
    public partial class Select_Product_Form : MaterialForm
    {
        private decimal _currentAverageDailySales = 0;
        private PurchaseLimitGridViewDesign _gridViewDesign;

        public Select_Product_Form()
        {
            InitializeComponent();
            selectProductButton.Click += new System.EventHandler(this.selectProductButton_Click);
            _gridViewDesign = new PurchaseLimitGridViewDesign(kryptonDataGridView1);
            limitSelectionDropdown.TextChanged += LimitSelectionDropdown_TextChanged;
            InitializeEvents();
        }
        private void InitializeEvents()
        {
            limitSelectionDropdown.TextChanged += LimitSelectionDropdown_TextChanged;
            _gridViewDesign.OnBudgetCalculated += UpdateBudgetAmount;
        }
        private void Select_Product_Form_Load(object sender, EventArgs e)
        {
            MonthsDropdownManager.InitializeMonthlyDropdown(monthlyDropdown);
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
        private void weeklyBudgetLabel_Click(object sender, EventArgs e)
        {
        }
        private void doneButton_Click(object sender, EventArgs e)
        {
        }
        private void label4_Click(object sender, EventArgs e)
        {
        }
        private void kryptonTextBox2_TextChanged(object sender, EventArgs e)
        {
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
            _gridViewDesign.UpdateSystemValue(limitSelectionDropdown.Values.Text, _currentAverageDailySales);
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
    }
}