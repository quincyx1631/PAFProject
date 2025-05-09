﻿namespace PAFProject.Models
{
    public class ProductData
    {
        public string Description { get; set; }
        public string BarCode { get; set; }
        public string AverageDaily { get; set; }
        public string PrefVendor { get; set; }
        public string QuantityOnHand { get; set; }
        public string DaysToGo { get; set; }
        public string OverShortStocks { get; set; }
        public string SystemValue { get; set; }
        public string AllowedPurchase { get; set; }
        public string UserValue { get; set; }
        public string PurchaseLimit { get; set; }
        public string AveragePrice { get; set; }
        public string BudgetAmount { get; set; }
        public string Remarks { get; set; }
        public string PurchaseLimit7Days { get; set; }
        public string PurchaseLimit15Days { get; set; }
        public string PurchaseLimit30Days { get; set; }
        public string LimitSelection { get; set; }
        public bool IsThreeMonthPeriod { get; set; }

    }

    public static class ProductDataManager
    {
        private static List<ProductData> _productList = new List<ProductData>();
        public static event Action<ProductData, int?> OnProductAdded;
        public static event Action<decimal> OnProposedBudget;

        // Add new method for updating existing products
        public static void UpdateProduct(ProductData product, int index)
        {
            if (index >= 0 && index < _productList.Count)
            {
                _productList[index] = product;
                OnProductAdded?.Invoke(product, index);
                UpdateBudgetTotal();
            }
        }
        public static void AddProduct(ProductData product)
        {
            _productList.Add(product);
            OnProductAdded?.Invoke(product, null);
            UpdateBudgetTotal();
        }

        private static void UpdateBudgetTotal()
        {
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