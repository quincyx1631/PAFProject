namespace PAFProject.Forms
{
    partial class Product_List_Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            searchTextBox = new Krypton.Toolkit.KryptonTextBox();
            searchButton = new Krypton.Toolkit.KryptonButton();
            productListDataGrid = new Krypton.Toolkit.KryptonDataGridView();
            previousButton = new Krypton.Toolkit.KryptonButton();
            pageInfoLabel = new Krypton.Toolkit.KryptonLabel();
            nextButton = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).BeginInit();
            kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)productListDataGrid).BeginInit();
            SuspendLayout();
            // 
            // kryptonPanel1
            // 
            kryptonPanel1.Controls.Add(searchTextBox);
            kryptonPanel1.Controls.Add(searchButton);
            kryptonPanel1.Controls.Add(productListDataGrid);
            kryptonPanel1.Controls.Add(previousButton);
            kryptonPanel1.Controls.Add(pageInfoLabel);
            kryptonPanel1.Controls.Add(nextButton);
            kryptonPanel1.Location = new Point(4, 64);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.Size = new Size(842, 633);
            kryptonPanel1.TabIndex = 0;
            // 
            // searchTextBox
            // 
            searchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            searchTextBox.Location = new Point(587, 10);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(180, 23);
            searchTextBox.TabIndex = 1;
            // 
            // searchButton
            // 
            searchButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            searchButton.Location = new Point(772, 10);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(60, 23);
            searchButton.TabIndex = 2;
            searchButton.Values.DropDownArrowColor = Color.Empty;
            searchButton.Values.Text = "Search";
            // 
            // productListDataGrid
            // 
            productListDataGrid.BorderStyle = BorderStyle.None;
            productListDataGrid.Location = new Point(3, 39);
            productListDataGrid.Name = "productListDataGrid";
            productListDataGrid.RowHeadersVisible = false;
            productListDataGrid.Size = new Size(844, 543);
            productListDataGrid.TabIndex = 0;
            // 
            // previousButton
            // 
            previousButton.Anchor = AnchorStyles.Bottom;
            previousButton.Location = new Point(300, 599);
            previousButton.Name = "previousButton";
            previousButton.Size = new Size(75, 25);
            previousButton.TabIndex = 3;
            previousButton.Values.DropDownArrowColor = Color.Empty;
            previousButton.Values.Text = "Previous";
            // 
            // pageInfoLabel
            // 
            pageInfoLabel.Anchor = AnchorStyles.Bottom;
            pageInfoLabel.AutoSize = false;
            pageInfoLabel.LabelStyle = Krypton.Toolkit.LabelStyle.ItalicPanel;
            pageInfoLabel.Location = new Point(387, 599);
            pageInfoLabel.Name = "pageInfoLabel";
            pageInfoLabel.Size = new Size(87, 25);
            pageInfoLabel.TabIndex = 4;
            pageInfoLabel.Values.Text = "Page 1";
            // 
            // nextButton
            // 
            nextButton.Anchor = AnchorStyles.Bottom;
            nextButton.Location = new Point(485, 599);
            nextButton.Name = "nextButton";
            nextButton.Size = new Size(75, 25);
            nextButton.TabIndex = 5;
            nextButton.Values.DropDownArrowColor = Color.Empty;
            nextButton.Values.Text = "Next";
            // 
            // Product_List_Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(850, 700);
            Controls.Add(kryptonPanel1);
            Name = "Product_List_Form";
            Text = "PRODUCT LIST: ";
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).EndInit();
            kryptonPanel1.ResumeLayout(false);
            kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)productListDataGrid).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonDataGridView productListDataGrid;
        private Krypton.Toolkit.KryptonTextBox searchTextBox;
        private Krypton.Toolkit.KryptonButton searchButton;
        private Krypton.Toolkit.KryptonButton previousButton;
        private Krypton.Toolkit.KryptonButton nextButton;
        private Krypton.Toolkit.KryptonLabel pageInfoLabel;
    }
}