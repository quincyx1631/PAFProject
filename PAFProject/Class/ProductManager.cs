// ProductManager.cs
using PAFProject.Models;
using System.Data;

namespace PAFProject.Class
{
    public class ProductManager
    {
        private readonly ProductDataAccess _dataAccess;
        public int TotalPages { get; private set; }

        public ProductManager()
        {
            _dataAccess = new ProductDataAccess();
        }

        public (DataTable DataTable, int TotalPages) GetInventoryData(string searchTerm, int currentPage, int recordsPerPage)
        {
            var result = _dataAccess.GetInventoryData(searchTerm, currentPage, recordsPerPage);
            TotalPages = result.TotalPages;
            return (result.DataTable, result.TotalPages);
        }

        public void FormatDataGridView(DataGridView grid)
        {
            // Set column headers
            grid.Columns["SalesDesc"].HeaderText = "Description";
            grid.Columns["QuantityOnHand"].HeaderText = "Quantity";
            grid.Columns["AverageCost"].HeaderText = "Avg. Cost";

            // Set number formats
            grid.Columns["QuantityOnHand"].DefaultCellStyle.Format = "N0";
            grid.Columns["AverageCost"].DefaultCellStyle.Format = "C2";

            // Set column sizing modes
            grid.Columns["SalesDesc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grid.Columns["QuantityOnHand"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Columns["AverageCost"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Set minimum width for the SalesDesc column
            grid.Columns["SalesDesc"].MinimumWidth = 200;

            // Align text in columns
            grid.Columns["SalesDesc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; // Left-aligned
            grid.Columns["QuantityOnHand"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // Right-aligned
            grid.Columns["AverageCost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // Right-aligned
        }
    }
}