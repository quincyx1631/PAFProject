using MaterialSkin;
using MaterialSkin.Controls;
using System;
using MySql.Data.MySqlClient;
using PAFProject.Database;
using PAFProject.Forms;

namespace PAFProject
{
    public partial class Main : MaterialForm
    {

        public Main()
        {
            InitializeComponent();
            selectButton.Click += new System.EventHandler(this.selectButton_Click);
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void weekLabel_Click(object sender, EventArgs e)
        {

        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void kryptonTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            try
            {
                Select_Product_Form selectProductForm = new Select_Product_Form();
                selectProductForm.Show();  // Try using Show() instead of ShowDialog() for testing
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
