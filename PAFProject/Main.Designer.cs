namespace PAFProject
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            branchSelect = new Krypton.Toolkit.KryptonDropButton();
            addBranch = new Button();
            shortOverTextBox = new Krypton.Toolkit.KryptonTextBox();
            proposedBudgetTextBox = new Krypton.Toolkit.KryptonTextBox();
            shortOverLabel = new Label();
            proposedBudgetLabel = new Label();
            weeklyBudgetTextBox = new Krypton.Toolkit.KryptonTextBox();
            weeklyBudgetLabel = new Label();
            weekTextBox = new Krypton.Toolkit.KryptonTextBox();
            weekLabel = new Label();
            selectButton = new Button();
            processButton = new Button();
            kryptonSeparator1 = new Krypton.Toolkit.KryptonSeparator();
            kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            kryptonDataGridView1 = new Krypton.Toolkit.KryptonDataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            Column10 = new DataGridViewTextBoxColumn();
            yulito = new Label();
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
            kryptonPanel1.Controls.Add(yulito);
            kryptonPanel1.Controls.Add(branchSelect);
            kryptonPanel1.Controls.Add(addBranch);
            kryptonPanel1.Controls.Add(shortOverTextBox);
            kryptonPanel1.Controls.Add(proposedBudgetTextBox);
            kryptonPanel1.Controls.Add(shortOverLabel);
            kryptonPanel1.Controls.Add(proposedBudgetLabel);
            kryptonPanel1.Controls.Add(weeklyBudgetTextBox);
            kryptonPanel1.Controls.Add(weeklyBudgetLabel);
            kryptonPanel1.Controls.Add(weekTextBox);
            kryptonPanel1.Controls.Add(weekLabel);
            kryptonPanel1.Controls.Add(selectButton);
            kryptonPanel1.Controls.Add(processButton);
            kryptonPanel1.Controls.Add(kryptonSeparator1);
            kryptonPanel1.Location = new Point(-1, 64);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.Size = new Size(1369, 145);
            kryptonPanel1.TabIndex = 0;
            kryptonPanel1.Paint += kryptonPanel1_Paint;
            // 
            // branchSelect
            // 
            branchSelect.Location = new Point(605, 10);
            branchSelect.Name = "branchSelect";
            branchSelect.Size = new Size(167, 37);
            branchSelect.TabIndex = 13;
            branchSelect.Values.DropDownArrowColor = Color.Empty;
            branchSelect.Values.Text = "Select Branch";
            // 
            // addBranch
            // 
            addBranch.Location = new Point(1149, 74);
            addBranch.Name = "addBranch";
            addBranch.Size = new Size(100, 35);
            addBranch.TabIndex = 12;
            addBranch.Text = "ADD BRANCH";
            addBranch.UseVisualStyleBackColor = true;
            // 
            // shortOverTextBox
            // 
            shortOverTextBox.Location = new Point(938, 81);
            shortOverTextBox.Multiline = true;
            shortOverTextBox.Name = "shortOverTextBox";
            shortOverTextBox.Size = new Size(143, 30);
            shortOverTextBox.TabIndex = 11;
            shortOverTextBox.TextChanged += kryptonTextBox3_TextChanged;
            // 
            // proposedBudgetTextBox
            // 
            proposedBudgetTextBox.Location = new Point(938, 45);
            proposedBudgetTextBox.Multiline = true;
            proposedBudgetTextBox.Name = "proposedBudgetTextBox";
            proposedBudgetTextBox.Size = new Size(143, 30);
            proposedBudgetTextBox.TabIndex = 10;
            // 
            // shortOverLabel
            // 
            shortOverLabel.BackColor = Color.Transparent;
            shortOverLabel.Font = new Font("Segoe UI", 12F);
            shortOverLabel.Location = new Point(800, 81);
            shortOverLabel.Name = "shortOverLabel";
            shortOverLabel.Size = new Size(132, 28);
            shortOverLabel.TabIndex = 9;
            shortOverLabel.Text = "Short/Over";
            shortOverLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // proposedBudgetLabel
            // 
            proposedBudgetLabel.BackColor = Color.Transparent;
            proposedBudgetLabel.Font = new Font("Segoe UI", 12F);
            proposedBudgetLabel.Location = new Point(800, 45);
            proposedBudgetLabel.Name = "proposedBudgetLabel";
            proposedBudgetLabel.Size = new Size(132, 28);
            proposedBudgetLabel.TabIndex = 8;
            proposedBudgetLabel.Text = "Proposed Budget";
            proposedBudgetLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // weeklyBudgetTextBox
            // 
            weeklyBudgetTextBox.Location = new Point(938, 10);
            weeklyBudgetTextBox.Multiline = true;
            weeklyBudgetTextBox.Name = "weeklyBudgetTextBox";
            weeklyBudgetTextBox.Size = new Size(143, 30);
            weeklyBudgetTextBox.TabIndex = 7;
            // 
            // weeklyBudgetLabel
            // 
            weeklyBudgetLabel.BackColor = Color.Transparent;
            weeklyBudgetLabel.Font = new Font("Segoe UI", 12F);
            weeklyBudgetLabel.Location = new Point(800, 10);
            weeklyBudgetLabel.Name = "weeklyBudgetLabel";
            weeklyBudgetLabel.Size = new Size(132, 28);
            weeklyBudgetLabel.TabIndex = 6;
            weeklyBudgetLabel.Text = "Weekly Budget";
            weeklyBudgetLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // weekTextBox
            // 
            weekTextBox.Location = new Point(102, 46);
            weekTextBox.Multiline = true;
            weekTextBox.Name = "weekTextBox";
            weekTextBox.Size = new Size(78, 30);
            weekTextBox.TabIndex = 5;
            weekTextBox.TextChanged += kryptonTextBox1_TextChanged;
            // 
            // weekLabel
            // 
            weekLabel.BackColor = Color.Transparent;
            weekLabel.Font = new Font("Segoe UI", 12F);
            weekLabel.Location = new Point(13, 46);
            weekLabel.Name = "weekLabel";
            weekLabel.Size = new Size(83, 28);
            weekLabel.TabIndex = 4;
            weekLabel.Text = "Week No.";
            weekLabel.TextAlign = ContentAlignment.MiddleLeft;
            weekLabel.Click += weekLabel_Click;
            // 
            // selectButton
            // 
            selectButton.Location = new Point(1149, 15);
            selectButton.Name = "selectButton";
            selectButton.Size = new Size(100, 35);
            selectButton.TabIndex = 0;
            selectButton.Text = "SELECT";
            selectButton.UseVisualStyleBackColor = true;
            // 
            // processButton
            // 
            processButton.Location = new Point(1255, 15);
            processButton.Name = "processButton";
            processButton.Size = new Size(100, 35);
            processButton.TabIndex = 1;
            processButton.Text = "PROCESS";
            processButton.UseVisualStyleBackColor = true;
            // 
            // kryptonSeparator1
            // 
            kryptonSeparator1.Location = new Point(13, 119);
            kryptonSeparator1.Name = "kryptonSeparator1";
            kryptonSeparator1.Size = new Size(1342, 19);
            kryptonSeparator1.TabIndex = 2;
            // 
            // kryptonPanel2
            // 
            kryptonPanel2.Controls.Add(kryptonDataGridView1);
            kryptonPanel2.Location = new Point(-1, 208);
            kryptonPanel2.Name = "kryptonPanel2";
            kryptonPanel2.Size = new Size(1369, 560);
            kryptonPanel2.TabIndex = 1;
            // 
            // kryptonDataGridView1
            // 
            kryptonDataGridView1.BorderStyle = BorderStyle.None;
            kryptonDataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5, Column6, Column7, Column8, Column9, Column10 });
            kryptonDataGridView1.Location = new Point(3, 0);
            kryptonDataGridView1.Name = "kryptonDataGridView1";
            kryptonDataGridView1.ReadOnly = true;
            kryptonDataGridView1.RowHeadersVisible = false;
            kryptonDataGridView1.Size = new Size(1363, 600);
            kryptonDataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            Column1.HeaderText = "Description";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 200;
            // 
            // Column2
            // 
            Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column2.HeaderText = "Bar Code";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // Column3
            // 
            Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column3.HeaderText = "Average Daily";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            // 
            // Column4
            // 
            Column4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column4.HeaderText = "Qty On Hand";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            // 
            // Column5
            // 
            Column5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column5.HeaderText = "Days To Go";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            // 
            // Column6
            // 
            Column6.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column6.HeaderText = "Over/Short Stocks";
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            // 
            // Column7
            // 
            Column7.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column7.HeaderText = "Purchase Limit";
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            // 
            // Column8
            // 
            Column8.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column8.HeaderText = "Average Price";
            Column8.Name = "Column8";
            Column8.ReadOnly = true;
            // 
            // Column9
            // 
            Column9.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column9.HeaderText = "Budget Amount";
            Column9.Name = "Column9";
            Column9.ReadOnly = true;
            // 
            // Column10
            // 
            Column10.HeaderText = "Remarks";
            Column10.Name = "Column10";
            Column10.ReadOnly = true;
            // 
            // yulito
            // 
            yulito.BackColor = Color.Transparent;
            yulito.Font = new Font("Segoe UI", 12F);
            yulito.Location = new Point(13, 10);
            yulito.Name = "yulito";
            yulito.Size = new Size(83, 28);
            yulito.TabIndex = 14;
            yulito.Text = "Yulito";
            yulito.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1366, 768);
            Controls.Add(kryptonPanel1);
            Controls.Add(kryptonPanel2);
            Name = "Main";
            Text = "Purchase Approval Form";
            Load += Main_Load;
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).EndInit();
            kryptonPanel1.ResumeLayout(false);
            kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)kryptonSeparator1).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonPanel2).EndInit();
            kryptonPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)kryptonDataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private Button selectButton;
        private Button processButton;
        private Krypton.Toolkit.KryptonSeparator kryptonSeparator1;
        private Krypton.Toolkit.KryptonDataGridView kryptonDataGridView1;
        private Label weekLabel;
        private Krypton.Toolkit.KryptonTextBox weekTextBox;
        private Label shortOverLabel;
        private Label proposedBudgetLabel;
        private Krypton.Toolkit.KryptonTextBox weeklyBudgetTextBox;
        private Label weeklyBudgetLabel;
        private Krypton.Toolkit.KryptonTextBox shortOverTextBox;
        private Krypton.Toolkit.KryptonTextBox proposedBudgetTextBox;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column10;
        private Button addBranch;
        private Krypton.Toolkit.KryptonDropButton branchSelect;
        private Label yulito;
    }
}
