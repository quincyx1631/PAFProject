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
            dateTextBox = new Krypton.Toolkit.KryptonTextBox();
            dateLabel = new Label();
            PDFButton = new Button();
            deleteButton = new Button();
            branchNameLabel = new Krypton.Toolkit.KryptonTextBox();
            branchName = new Label();
            branchSelect = new Krypton.Toolkit.KryptonDropButton();
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
            Column11 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column12 = new DataGridViewTextBoxColumn();
            Column13 = new DataGridViewTextBoxColumn();
            Column14 = new DataGridViewTextBoxColumn();
            Column15 = new DataGridViewTextBoxColumn();
            Column16 = new DataGridViewTextBoxColumn();
            Column17 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            Column10 = new DataGridViewTextBoxColumn();
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
            kryptonPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            kryptonPanel1.Controls.Add(dateTextBox);
            kryptonPanel1.Controls.Add(dateLabel);
            kryptonPanel1.Controls.Add(PDFButton);
            kryptonPanel1.Controls.Add(deleteButton);
            kryptonPanel1.Controls.Add(branchNameLabel);
            kryptonPanel1.Controls.Add(branchName);
            kryptonPanel1.Controls.Add(branchSelect);
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
            kryptonPanel1.Location = new Point(0, 66);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.Size = new Size(1366, 142);
            kryptonPanel1.TabIndex = 0;
            // 
            // dateTextBox
            // 
            dateTextBox.Location = new Point(102, 83);
            dateTextBox.Multiline = true;
            dateTextBox.Name = "dateTextBox";
            dateTextBox.ReadOnly = true;
            dateTextBox.Size = new Size(149, 30);
            dateTextBox.TabIndex = 19;
            // 
            // dateLabel
            // 
            dateLabel.BackColor = Color.Transparent;
            dateLabel.Font = new Font("Segoe UI", 12F);
            dateLabel.Location = new Point(13, 86);
            dateLabel.Name = "dateLabel";
            dateLabel.Size = new Size(83, 28);
            dateLabel.TabIndex = 18;
            dateLabel.Text = "Date:";
            dateLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PDFButton
            // 
            PDFButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PDFButton.Enabled = false;
            PDFButton.Location = new Point(1245, 74);
            PDFButton.Name = "PDFButton";
            PDFButton.Size = new Size(110, 35);
            PDFButton.TabIndex = 17;
            PDFButton.Text = "EXPORT PDF";
            PDFButton.UseVisualStyleBackColor = true;
            PDFButton.Visible = false;
            // 
            // deleteButton
            // 
            deleteButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            deleteButton.Location = new Point(1110, 74);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(129, 35);
            deleteButton.TabIndex = 16;
            deleteButton.Text = "DELETE";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Visible = false;
            // 
            // branchNameLabel
            // 
            branchNameLabel.Location = new Point(102, 10);
            branchNameLabel.Multiline = true;
            branchNameLabel.Name = "branchNameLabel";
            branchNameLabel.ReadOnly = true;
            branchNameLabel.Size = new Size(149, 30);
            branchNameLabel.TabIndex = 15;
            // 
            // branchName
            // 
            branchName.BackColor = Color.Transparent;
            branchName.Font = new Font("Segoe UI", 12F);
            branchName.Location = new Point(13, 10);
            branchName.Name = "branchName";
            branchName.Size = new Size(83, 28);
            branchName.TabIndex = 14;
            branchName.Text = "Branch: ";
            branchName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // branchSelect
            // 
            branchSelect.Location = new Point(257, 10);
            branchSelect.Name = "branchSelect";
            branchSelect.Size = new Size(116, 35);
            branchSelect.TabIndex = 13;
            branchSelect.Values.DropDownArrowColor = Color.Empty;
            branchSelect.Values.Text = "Select Branch";
            // 
            // shortOverTextBox
            // 
            shortOverTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            shortOverTextBox.Location = new Point(938, 81);
            shortOverTextBox.Multiline = true;
            shortOverTextBox.Name = "shortOverTextBox";
            shortOverTextBox.ReadOnly = true;
            shortOverTextBox.Size = new Size(143, 30);
            shortOverTextBox.TabIndex = 11;
            shortOverTextBox.TextAlign = HorizontalAlignment.Right;
            // 
            // proposedBudgetTextBox
            // 
            proposedBudgetTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            proposedBudgetTextBox.Location = new Point(938, 45);
            proposedBudgetTextBox.Multiline = true;
            proposedBudgetTextBox.Name = "proposedBudgetTextBox";
            proposedBudgetTextBox.ReadOnly = true;
            proposedBudgetTextBox.Size = new Size(143, 30);
            proposedBudgetTextBox.TabIndex = 10;
            proposedBudgetTextBox.TextAlign = HorizontalAlignment.Right;
            // 
            // shortOverLabel
            // 
            shortOverLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
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
            proposedBudgetLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
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
            weeklyBudgetTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            weeklyBudgetTextBox.Location = new Point(938, 10);
            weeklyBudgetTextBox.Multiline = true;
            weeklyBudgetTextBox.Name = "weeklyBudgetTextBox";
            weeklyBudgetTextBox.Size = new Size(143, 30);
            weeklyBudgetTextBox.TabIndex = 7;
            weeklyBudgetTextBox.TextAlign = HorizontalAlignment.Right;
            // 
            // weeklyBudgetLabel
            // 
            weeklyBudgetLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
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
            weekTextBox.Size = new Size(149, 30);
            weekTextBox.TabIndex = 5;
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
            // 
            // selectButton
            // 
            selectButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            selectButton.Location = new Point(1110, 15);
            selectButton.Name = "selectButton";
            selectButton.Size = new Size(129, 35);
            selectButton.TabIndex = 0;
            selectButton.Text = "SELECT PRODUCT";
            selectButton.UseVisualStyleBackColor = true;
            // 
            // processButton
            // 
            processButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            processButton.Location = new Point(1245, 15);
            processButton.Name = "processButton";
            processButton.Size = new Size(110, 35);
            processButton.TabIndex = 1;
            processButton.Text = "EXPORT EXCEL";
            processButton.UseVisualStyleBackColor = true;
            // 
            // kryptonSeparator1
            // 
            kryptonSeparator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            kryptonSeparator1.Location = new Point(3, 119);
            kryptonSeparator1.Name = "kryptonSeparator1";
            kryptonSeparator1.Size = new Size(1360, 20);
            kryptonSeparator1.TabIndex = 2;
            // 
            // kryptonPanel2
            // 
            kryptonPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            kryptonPanel2.Controls.Add(kryptonDataGridView1);
            kryptonPanel2.Location = new Point(0, 208);
            kryptonPanel2.Name = "kryptonPanel2";
            kryptonPanel2.Size = new Size(1366, 560);
            kryptonPanel2.TabIndex = 1;
            // 
            // kryptonDataGridView1
            // 
            kryptonDataGridView1.AllowUserToResizeColumns = false;
            kryptonDataGridView1.AllowUserToResizeRows = false;
            kryptonDataGridView1.BorderStyle = BorderStyle.None;
            kryptonDataGridView1.ColumnHeadersHeight = 30;
            kryptonDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            kryptonDataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column11, Column4, Column5, Column6, Column12, Column13, Column14, Column15, Column16, Column17, Column7, Column8, Column9, Column10 });
            kryptonDataGridView1.Dock = DockStyle.Fill;
            kryptonDataGridView1.Location = new Point(0, 0);
            kryptonDataGridView1.Name = "kryptonDataGridView1";
            kryptonDataGridView1.ReadOnly = true;
            kryptonDataGridView1.RowHeadersVisible = false;
            kryptonDataGridView1.Size = new Size(1366, 560);
            kryptonDataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            Column1.HeaderText = "DESCRIPTION";
            Column1.MinimumWidth = 200;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 200;
            // 
            // Column2
            // 
            Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column2.HeaderText = "BAR CODE";
            Column2.MinimumWidth = 150;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // Column3
            // 
            Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column3.HeaderText = "AVERAGE DAILY";
            Column3.MinimumWidth = 150;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            // 
            // Column11
            // 
            Column11.HeaderText = "PREFERRED SUPPLIER";
            Column11.MinimumWidth = 200;
            Column11.Name = "Column11";
            Column11.ReadOnly = true;
            Column11.Width = 200;
            // 
            // Column4
            // 
            Column4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column4.HeaderText = "QTY ON HAND";
            Column4.MinimumWidth = 120;
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            // 
            // Column5
            // 
            Column5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column5.HeaderText = "DAYS TO GO";
            Column5.MinimumWidth = 120;
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            // 
            // Column6
            // 
            Column6.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column6.HeaderText = "OVER SHORT STOCKS";
            Column6.MinimumWidth = 120;
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            // 
            // Column12
            // 
            Column12.HeaderText = "7 Days";
            Column12.MinimumWidth = 100;
            Column12.Name = "Column12";
            Column12.ReadOnly = true;
            // 
            // Column13
            // 
            Column13.HeaderText = "15 Days";
            Column13.MinimumWidth = 100;
            Column13.Name = "Column13";
            Column13.ReadOnly = true;
            // 
            // Column14
            // 
            Column14.HeaderText = "30 Days";
            Column14.MinimumWidth = 100;
            Column14.Name = "Column14";
            Column14.ReadOnly = true;
            // 
            // Column15
            // 
            Column15.HeaderText = "LIMIT SELECTION";
            Column15.MinimumWidth = 100;
            Column15.Name = "Column15";
            Column15.ReadOnly = true;
            // 
            // Column16
            // 
            Column16.HeaderText = "System";
            Column16.MinimumWidth = 100;
            Column16.Name = "Column16";
            Column16.ReadOnly = true;
            // 
            // Column17
            // 
            Column17.HeaderText = "User";
            Column17.MinimumWidth = 100;
            Column17.Name = "Column17";
            Column17.ReadOnly = true;
            // 
            // Column7
            // 
            Column7.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column7.HeaderText = "Total";
            Column7.MinimumWidth = 120;
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            // 
            // Column8
            // 
            Column8.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column8.HeaderText = "AVERAGE PRICE";
            Column8.MinimumWidth = 100;
            Column8.Name = "Column8";
            Column8.ReadOnly = true;
            // 
            // Column9
            // 
            Column9.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column9.HeaderText = "BUDGET AMOUNT";
            Column9.MinimumWidth = 120;
            Column9.Name = "Column9";
            Column9.ReadOnly = true;
            // 
            // Column10
            // 
            Column10.HeaderText = "REMARKS";
            Column10.MinimumWidth = 200;
            Column10.Name = "Column10";
            Column10.ReadOnly = true;
            Column10.Width = 200;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1366, 768);
            Controls.Add(kryptonPanel1);
            Controls.Add(kryptonPanel2);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = " ";
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
        private Krypton.Toolkit.KryptonDropButton branchSelect;
        private Label branchName;
        private Krypton.Toolkit.KryptonTextBox branchNameLabel;
        private Button deleteButton;
        private Button PDFButton;
        private Krypton.Toolkit.KryptonTextBox dateTextBox;
        private Label dateLabel;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column13;
        private DataGridViewTextBoxColumn Column14;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewTextBoxColumn Column16;
        private DataGridViewTextBoxColumn Column17;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column10;
    }
}
