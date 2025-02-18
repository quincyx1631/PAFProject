using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAFProject.Models
{
    public class SelectedProduct
    {
        public string Description { get; set; }
        public decimal AverageDaily { get; set; }
        public decimal QuantityOnHand { get; set; }
        public string DaysToGo { get; set; }
        public string OverShortStocks { get; set; }
        public decimal AverageCost { get; set; }
        public string BarCode { get; set; }
        public string PrefVendor { get; set; }
        public string UserValue { get; set; }
        public string SystemValue { get; set; }
        public string TotalValue { get; set; }
        public string BudgetAmount { get; set; }
        public string Remarks { get; set; }
        public string LimitSelection { get; set; } = "7 Days";
        public bool IsThreeMonthPeriod { get; set; } = true;
    }
}
