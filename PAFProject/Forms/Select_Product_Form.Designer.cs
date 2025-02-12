namespace PAFProject.Forms
{
    partial class Select_Product_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            monthsSelectionLabel = new Label();
            monthlyDropdown = new Krypton.Toolkit.KryptonDropButton();
            kryptonSeparator1 = new Krypton.Toolkit.KryptonSeparator();
            kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            kryptonDataGridView1 = new Krypton.Toolkit.KryptonDataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            limitSelectionDropdown = new Krypton.Toolkit.KryptonDropButton();
            limitSelectionLabel = new Label();
            remarksTextBox = new Krypton.Toolkit.KryptonTextBox();
            remarksLabel = new Label();
            budgetAmountTextBox = new Krypton.Toolkit.KryptonTextBox();
            budgetAmountLabel = new Label();
            AVGPriceTextBox = new Krypton.Toolkit.KryptonTextBox();
            AVGPriceLabel = new Label();
            overShortSTextBox = new Krypton.Toolkit.KryptonTextBox();
            stocksLabel = new Label();
            daysTGTextBox = new Krypton.Toolkit.KryptonTextBox();
            DTGLabel = new Label();
            qtyOHLabel = new Label();
            doneButton = new Button();
            selectProductButton = new Button();
            qtyTextBox = new Krypton.Toolkit.KryptonTextBox();
            avgTextBox = new Krypton.Toolkit.KryptonTextBox();
            nameTextBox = new Krypton.Toolkit.KryptonTextBox();
            avgLabel = new Label();
            nameLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).BeginInit();
            kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)kryptonSeparator1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonPanel2).BeginInit();
            kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)kryptonDataGridView1).BeginInit();
            SuspendLayout();
            // 
            // kryptonPanel1
            // 
            kryptonPanel1.Controls.Add(monthsSelectionLabel);
            kryptonPanel1.Controls.Add(monthlyDropdown);
            kryptonPanel1.Controls.Add(kryptonSeparator1);
            kryptonPanel1.Location = new Point(3, 63);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.Size = new Size(844, 71);
            kryptonPanel1.TabIndex = 0;
            // 
            // monthsSelectionLabel
            // 
            monthsSelectionLabel.BackColor = Color.Transparent;
            monthsSelectionLabel.Font = new Font("Segoe UI", 12F);
            monthsSelectionLabel.Location = new Point(617, 12);
            monthsSelectionLabel.Name = "monthsSelectionLabel";
            monthsSelectionLabel.Size = new Size(85, 28);
            monthsSelectionLabel.TabIndex = 8;
            monthsSelectionLabel.Text = "MONTHS: ";
            monthsSelectionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // monthlyDropdown
            // 
            monthlyDropdown.Location = new Point(708, 12);
            monthlyDropdown.Name = "monthlyDropdown";
            monthlyDropdown.Size = new Size(130, 30);
            monthlyDropdown.TabIndex = 1;
            monthlyDropdown.Values.DropDownArrowColor = Color.Empty;
            monthlyDropdown.Values.Text = "3 Months";
            // 
            // kryptonSeparator1
            // 
            kryptonSeparator1.Location = new Point(12, 48);
            kryptonSeparator1.Name = "kryptonSeparator1";
            kryptonSeparator1.Size = new Size(826, 16);
            kryptonSeparator1.TabIndex = 0;
            // 
            // kryptonPanel2
            // 
            kryptonPanel2.Controls.Add(kryptonDataGridView1);
            kryptonPanel2.Controls.Add(limitSelectionDropdown);
            kryptonPanel2.Controls.Add(limitSelectionLabel);
            kryptonPanel2.Controls.Add(remarksTextBox);
            kryptonPanel2.Controls.Add(remarksLabel);
            kryptonPanel2.Controls.Add(budgetAmountTextBox);
            kryptonPanel2.Controls.Add(budgetAmountLabel);
            kryptonPanel2.Controls.Add(AVGPriceTextBox);
            kryptonPanel2.Controls.Add(AVGPriceLabel);
            kryptonPanel2.Controls.Add(overShortSTextBox);
            kryptonPanel2.Controls.Add(stocksLabel);
            kryptonPanel2.Controls.Add(daysTGTextBox);
            kryptonPanel2.Controls.Add(DTGLabel);
            kryptonPanel2.Controls.Add(qtyOHLabel);
            kryptonPanel2.Controls.Add(doneButton);
            kryptonPanel2.Controls.Add(selectProductButton);
            kryptonPanel2.Controls.Add(qtyTextBox);
            kryptonPanel2.Controls.Add(avgTextBox);
            kryptonPanel2.Controls.Add(nameTextBox);
            kryptonPanel2.Controls.Add(avgLabel);
            kryptonPanel2.Controls.Add(nameLabel);
            kryptonPanel2.Location = new Point(3, 133);
            kryptonPanel2.Name = "kryptonPanel2";
            kryptonPanel2.Size = new Size(844, 565);
            kryptonPanel2.TabIndex = 1;
            // 
            // kryptonDataGridView1
            // 
            kryptonDataGridView1.AllowUserToOrderColumns = true;
            kryptonDataGridView1.AllowUserToResizeColumns = false;
            kryptonDataGridView1.AllowUserToResizeRows = false;
            kryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            kryptonDataGridView1.BorderStyle = BorderStyle.None;
            kryptonDataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            kryptonDataGridView1.ColumnHeadersHeight = 30;
            kryptonDataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3 });
            kryptonDataGridView1.Location = new Point(193, 223);
            kryptonDataGridView1.MultiSelect = false;
            kryptonDataGridView1.Name = "kryptonDataGridView1";
            kryptonDataGridView1.RowHeadersVisible = false;
            kryptonDataGridView1.RowHeadersWidth = 45;
            kryptonDataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            kryptonDataGridView1.ScrollBars = ScrollBars.None;
            kryptonDataGridView1.ShowCellErrors = false;
            kryptonDataGridView1.ShowCellToolTips = false;
            kryptonDataGridView1.ShowEditingIcon = false;
            kryptonDataGridView1.ShowRowErrors = false;
            kryptonDataGridView1.Size = new Size(279, 61);
            kryptonDataGridView1.StandardTab = true;
            kryptonDataGridView1.TabIndex = 30;
            kryptonDataGridView1.TabStop = false;
            kryptonDataGridView1.CellContentClick += kryptonDataGridView1_CellContentClick;
            // 
            // Column1
            // 
            Column1.HeaderText = "System";
            Column1.Name = "Column1";
            // 
            // Column2
            // 
            Column2.HeaderText = "User";
            Column2.Name = "Column2";
            // 
            // Column3
            // 
            Column3.HeaderText = "Total";
            Column3.Name = "Column3";
            // 
            // limitSelectionDropdown
            // 
            limitSelectionDropdown.Location = new Point(15, 254);
            limitSelectionDropdown.Name = "limitSelectionDropdown";
            limitSelectionDropdown.Size = new Size(130, 30);
            limitSelectionDropdown.TabIndex = 27;
            limitSelectionDropdown.Values.DropDownArrowColor = Color.Empty;
            limitSelectionDropdown.Values.Text = "7 Days";
            limitSelectionDropdown.Click += limitSelectionDropdown_Click;
            // 
            // limitSelectionLabel
            // 
            limitSelectionLabel.BackColor = Color.Transparent;
            limitSelectionLabel.Font = new Font("Segoe UI", 12F);
            limitSelectionLabel.Location = new Point(15, 223);
            limitSelectionLabel.Name = "limitSelectionLabel";
            limitSelectionLabel.Size = new Size(172, 28);
            limitSelectionLabel.TabIndex = 26;
            limitSelectionLabel.Text = "LIMIT SELECTION:";
            limitSelectionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // remarksTextBox
            // 
            remarksTextBox.Location = new Point(193, 493);
            remarksTextBox.Multiline = true;
            remarksTextBox.Name = "remarksTextBox";
            remarksTextBox.Size = new Size(279, 30);
            remarksTextBox.TabIndex = 25;
            // 
            // remarksLabel
            // 
            remarksLabel.BackColor = Color.Transparent;
            remarksLabel.Font = new Font("Segoe UI", 12F);
            remarksLabel.Location = new Point(15, 493);
            remarksLabel.Name = "remarksLabel";
            remarksLabel.Size = new Size(172, 28);
            remarksLabel.TabIndex = 24;
            remarksLabel.Text = "REMARKS:";
            remarksLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // budgetAmountTextBox
            // 
            budgetAmountTextBox.Location = new Point(193, 336);
            budgetAmountTextBox.Multiline = true;
            budgetAmountTextBox.Name = "budgetAmountTextBox";
            budgetAmountTextBox.Size = new Size(279, 30);
            budgetAmountTextBox.TabIndex = 23;
            budgetAmountTextBox.TextAlign = HorizontalAlignment.Right;
            budgetAmountTextBox.TextChanged += budgetAmountTextBox_TextChanged;
            // 
            // budgetAmountLabel
            // 
            budgetAmountLabel.BackColor = Color.Transparent;
            budgetAmountLabel.Font = new Font("Segoe UI", 12F);
            budgetAmountLabel.Location = new Point(15, 336);
            budgetAmountLabel.Name = "budgetAmountLabel";
            budgetAmountLabel.Size = new Size(172, 28);
            budgetAmountLabel.TabIndex = 22;
            budgetAmountLabel.Text = "BUDGET AMOUNT:";
            budgetAmountLabel.TextAlign = ContentAlignment.MiddleLeft;
            budgetAmountLabel.Click += budgetAmountLabel_Click;
            // 
            // AVGPriceTextBox
            // 
            AVGPriceTextBox.Location = new Point(193, 296);
            AVGPriceTextBox.Multiline = true;
            AVGPriceTextBox.Name = "AVGPriceTextBox";
            AVGPriceTextBox.Size = new Size(279, 30);
            AVGPriceTextBox.TabIndex = 21;
            AVGPriceTextBox.TextAlign = HorizontalAlignment.Right;
            // 
            // AVGPriceLabel
            // 
            AVGPriceLabel.BackColor = Color.Transparent;
            AVGPriceLabel.Font = new Font("Segoe UI", 12F);
            AVGPriceLabel.Location = new Point(15, 296);
            AVGPriceLabel.Name = "AVGPriceLabel";
            AVGPriceLabel.Size = new Size(172, 28);
            AVGPriceLabel.TabIndex = 20;
            AVGPriceLabel.Text = "AVERAGE PRICE: ";
            AVGPriceLabel.TextAlign = ContentAlignment.MiddleLeft;
            AVGPriceLabel.Click += label1_Click;
            // 
            // overShortSTextBox
            // 
            overShortSTextBox.Location = new Point(193, 176);
            overShortSTextBox.Multiline = true;
            overShortSTextBox.Name = "overShortSTextBox";
            overShortSTextBox.Size = new Size(279, 30);
            overShortSTextBox.TabIndex = 19;
            overShortSTextBox.TextAlign = HorizontalAlignment.Right;
            // 
            // stocksLabel
            // 
            stocksLabel.BackColor = Color.Transparent;
            stocksLabel.Font = new Font("Segoe UI", 12F);
            stocksLabel.Location = new Point(15, 176);
            stocksLabel.Name = "stocksLabel";
            stocksLabel.Size = new Size(172, 28);
            stocksLabel.TabIndex = 18;
            stocksLabel.Text = "OVER/SHORT STOCKS: ";
            stocksLabel.TextAlign = ContentAlignment.MiddleLeft;
            stocksLabel.Click += label4_Click;
            // 
            // daysTGTextBox
            // 
            daysTGTextBox.Location = new Point(193, 136);
            daysTGTextBox.Multiline = true;
            daysTGTextBox.Name = "daysTGTextBox";
            daysTGTextBox.Size = new Size(279, 30);
            daysTGTextBox.TabIndex = 17;
            daysTGTextBox.TextAlign = HorizontalAlignment.Right;
            // 
            // DTGLabel
            // 
            DTGLabel.BackColor = Color.Transparent;
            DTGLabel.Font = new Font("Segoe UI", 12F);
            DTGLabel.Location = new Point(15, 136);
            DTGLabel.Name = "DTGLabel";
            DTGLabel.Size = new Size(127, 28);
            DTGLabel.TabIndex = 16;
            DTGLabel.Text = "DAYS TO GO:";
            DTGLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // qtyOHLabel
            // 
            qtyOHLabel.BackColor = Color.Transparent;
            qtyOHLabel.Font = new Font("Segoe UI", 12F);
            qtyOHLabel.Location = new Point(15, 96);
            qtyOHLabel.Name = "qtyOHLabel";
            qtyOHLabel.Size = new Size(127, 28);
            qtyOHLabel.TabIndex = 15;
            qtyOHLabel.Text = "QTY. ON HAND: ";
            qtyOHLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // doneButton
            // 
            doneButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            doneButton.Location = new Point(735, 515);
            doneButton.Name = "doneButton";
            doneButton.Size = new Size(100, 40);
            doneButton.TabIndex = 14;
            doneButton.Text = "DONE";
            doneButton.UseVisualStyleBackColor = true;
            
            // 
            // selectProductButton
            // 
            selectProductButton.Location = new Point(478, 16);
            selectProductButton.Name = "selectProductButton";
            selectProductButton.Size = new Size(131, 30);
            selectProductButton.TabIndex = 13;
            selectProductButton.Text = "SELECT A PRODUCT";
            selectProductButton.UseVisualStyleBackColor = true;
            // 
            // qtyTextBox
            // 
            qtyTextBox.Location = new Point(193, 96);
            qtyTextBox.Multiline = true;
            qtyTextBox.Name = "qtyTextBox";
            qtyTextBox.Size = new Size(279, 30);
            qtyTextBox.TabIndex = 12;
            qtyTextBox.TextAlign = HorizontalAlignment.Right;
            qtyTextBox.TextChanged += qtyTextBox_TextChanged;
            // 
            // avgTextBox
            // 
            avgTextBox.Location = new Point(193, 56);
            avgTextBox.Multiline = true;
            avgTextBox.Name = "avgTextBox";
            avgTextBox.Size = new Size(279, 30);
            avgTextBox.TabIndex = 11;
            avgTextBox.TextChanged += avgTextBox_TextChanged;
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(193, 16);
            nameTextBox.Multiline = true;
            nameTextBox.Name = "nameTextBox";
            nameTextBox.ReadOnly = true;
            nameTextBox.Size = new Size(279, 30);
            nameTextBox.TabIndex = 10;
            nameTextBox.TextChanged += nameTextBox_TextChanged;
            // 
            // avgLabel
            // 
            avgLabel.BackColor = Color.Transparent;
            avgLabel.Font = new Font("Segoe UI", 12F);
            avgLabel.Location = new Point(15, 56);
            avgLabel.Name = "avgLabel";
            avgLabel.Size = new Size(127, 28);
            avgLabel.TabIndex = 8;
            avgLabel.Text = "AVERAGE DAILY:";
            avgLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nameLabel
            // 
            nameLabel.BackColor = Color.Transparent;
            nameLabel.Font = new Font("Segoe UI", 12F);
            nameLabel.Location = new Point(15, 16);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(127, 28);
            nameLabel.TabIndex = 7;
            nameLabel.Text = "NAME: ";
            nameLabel.TextAlign = ContentAlignment.MiddleLeft;
            nameLabel.Click += weeklyBudgetLabel_Click;
            // 
            // Select_Product_Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(850, 700);
            Controls.Add(kryptonPanel1);
            Controls.Add(kryptonPanel2);
            MaximizeBox = false;
            Name = "Select_Product_Form";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SELECT A PRODUCT:";
            Load += Select_Product_Form_Load;
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).EndInit();
            kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)kryptonSeparator1).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonPanel2).EndInit();
            kryptonPanel2.ResumeLayout(false);
            kryptonPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)kryptonDataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonSeparator kryptonSeparator1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private Label nameLabel;
        private Label avgLabel;
        private Krypton.Toolkit.KryptonTextBox qtyTextBox;
        private Krypton.Toolkit.KryptonTextBox avgTextBox;
        private Krypton.Toolkit.KryptonTextBox nameTextBox;
        private Krypton.Toolkit.KryptonDropButton monthlyDropdown;
        private Button selectProductButton;
        private Label monthsSelectionLabel;
        private Button doneButton;
        private Krypton.Toolkit.KryptonTextBox daysTGTextBox;
        private Label DTGLabel;
        private Label qtyOHLabel;
        private Krypton.Toolkit.KryptonTextBox overShortSTextBox;
        private Label stocksLabel;
        private Krypton.Toolkit.KryptonTextBox AVGPriceTextBox;
        private Label AVGPriceLabel;
        private Krypton.Toolkit.KryptonTextBox budgetAmountTextBox;
        private Label budgetAmountLabel;
        private Krypton.Toolkit.KryptonTextBox remarksTextBox;
        private Label remarksLabel;
        private Krypton.Toolkit.KryptonDataGridView kryptonDataGridView1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private Krypton.Toolkit.KryptonDropButton limitSelectionDropdown;
        private Label limitSelectionLabel;
    }
}