namespace PAFProject.Forms
{
    partial class Add_Branch_Form
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
            addButton = new Krypton.Toolkit.KryptonButton();
            BranchLocationTextBox = new Krypton.Toolkit.KryptonTextBox();
            BranchLocation = new Label();
            BranchName = new Label();
            BranchNameTextBox = new Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).BeginInit();
            kryptonPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // kryptonPanel1
            // 
            kryptonPanel1.Controls.Add(addButton);
            kryptonPanel1.Controls.Add(BranchLocationTextBox);
            kryptonPanel1.Controls.Add(BranchLocation);
            kryptonPanel1.Controls.Add(BranchName);
            kryptonPanel1.Controls.Add(BranchNameTextBox);
            kryptonPanel1.Location = new Point(3, 64);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.Size = new Size(645, 433);
            kryptonPanel1.TabIndex = 0;
            // 
            // addButton
            // 
            addButton.Location = new Point(515, 18);
            addButton.Name = "addButton";
            addButton.Size = new Size(120, 35);
            addButton.TabIndex = 4;
            addButton.Values.DropDownArrowColor = Color.Empty;
            addButton.Values.Text = "Add Branch";
            // 
            // BranchLocationTextBox
            // 
            BranchLocationTextBox.Location = new Point(144, 68);
            BranchLocationTextBox.Multiline = true;
            BranchLocationTextBox.Name = "BranchLocationTextBox";
            BranchLocationTextBox.RightToLeft = RightToLeft.No;
            BranchLocationTextBox.Size = new Size(160, 35);
            BranchLocationTextBox.TabIndex = 3;
            // 
            // BranchLocation
            // 
            BranchLocation.BackColor = Color.Transparent;
            BranchLocation.Font = new Font("Segoe UI", 12F);
            BranchLocation.ImageAlign = ContentAlignment.BottomLeft;
            BranchLocation.Location = new Point(9, 68);
            BranchLocation.Name = "BranchLocation";
            BranchLocation.Size = new Size(129, 35);
            BranchLocation.TabIndex = 2;
            BranchLocation.Text = "Branch Location: ";
            BranchLocation.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // BranchName
            // 
            BranchName.BackColor = Color.Transparent;
            BranchName.Font = new Font("Segoe UI", 12F);
            BranchName.ImageAlign = ContentAlignment.BottomLeft;
            BranchName.Location = new Point(9, 18);
            BranchName.Name = "BranchName";
            BranchName.Size = new Size(120, 35);
            BranchName.TabIndex = 1;
            BranchName.Text = "Branch Name: ";
            BranchName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // BranchNameTextBox
            // 
            BranchNameTextBox.Location = new Point(144, 18);
            BranchNameTextBox.Multiline = true;
            BranchNameTextBox.Name = "BranchNameTextBox";
            BranchNameTextBox.RightToLeft = RightToLeft.No;
            BranchNameTextBox.Size = new Size(160, 35);
            BranchNameTextBox.TabIndex = 0;
            // 
            // Add_Branch_Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 500);
            Controls.Add(kryptonPanel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Add_Branch_Form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add Branch Form";
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).EndInit();
            kryptonPanel1.ResumeLayout(false);
            kryptonPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Label BranchName;
        private Krypton.Toolkit.KryptonTextBox BranchNameTextBox;
        private Label BranchLocation;
        private Krypton.Toolkit.KryptonTextBox BranchLocationTextBox;
        private Krypton.Toolkit.KryptonButton addButton;
    }
}