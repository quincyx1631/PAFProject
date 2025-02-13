using System;
using System.Collections.Generic;

namespace PAFProject.Models
{
    public class ProductData
    {
        public string Description { get; set; }
        public string BarCode { get; set; }
        public string AverageDaily { get; set; }
        public string QuantityOnHand { get; set; }
        public string DaysToGo { get; set; }
        public string OverShortStocks { get; set; }
        public string PurchaseLimit { get; set; }
        public string AveragePrice { get; set; }
        public string BudgetAmount { get; set; }
        public string Remarks { get; set; }
    }

    public static class ProductDataManager
    {
        private static List<ProductData> _productList = new List<ProductData>();
        public static event Action<ProductData> OnProductAdded;
        public static event Action<decimal> OnProposedBudget;

        public static void AddProduct(ProductData product)
        {
            _productList.Add(product);
            OnProductAdded?.Invoke(product);

            // Calculate and update weekly budget total
            decimal totalBudget = 0;
            foreach (var p in _productList)
            {
                if (decimal.TryParse(p.BudgetAmount, out decimal budget))
                {
                    totalBudget += budget;
                }
            }
            OnProposedBudget?.Invoke(totalBudget);
        }

        public static List<ProductData> GetProducts()
        {
            return _productList;
        }

        public static void ClearProducts()
        {
            _productList.Clear();
            OnProposedBudget?.Invoke(0);
        }
    }
}