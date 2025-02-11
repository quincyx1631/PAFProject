//AverageDailySalesComputation.cs
using PAFProject.Models;

namespace PAFProject.Computations
{
    public class AverageDailySalesComputation
    {
        private readonly SalesDataAccess _salesDataAccess;
        private const int DAYS_IN_THREE_MONTHS = 90; // Approximate days in 3 months
        private const int DAYS_IN_SIX_MONTHS = 180;
        private const int DAYS_IN_A_WEEK = 7;

        public AverageDailySalesComputation()
        {
            _salesDataAccess = new SalesDataAccess();
        }

        public decimal GetNumericAverageDailySales(string productDescription)
        {
            try
            {
                var salesData = _salesDataAccess.GetThreeMonthsSales(productDescription);
                var productSales = salesData.FirstOrDefault();
                if (productSales == null)
                {
                    return 0;
                }

                decimal averageDailySales = productSales.Quantity / (decimal)DAYS_IN_THREE_MONTHS;
                return averageDailySales;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error computing numeric average daily sales: {ex.Message}");
            }
        }

        public string ComputeAverageDailySales(string productDescription)
        {
            try
            {
                decimal averageDailySales = GetNumericAverageDailySales(productDescription);

                if (averageDailySales >= 1)
                {
                    int roundedSales = (int)Math.Round(averageDailySales, 0);
                    return $"{roundedSales} Per Day (Average: {averageDailySales:F2})";
                }
                else if (averageDailySales >= 1m / DAYS_IN_A_WEEK)
                {
                    // For weekly sales, still use ceiling to ensure minimum 1 per week if close
                    int weeklySales = (int)Math.Ceiling(averageDailySales * DAYS_IN_A_WEEK);
                    return $"{weeklySales} Per Week (Average: {averageDailySales:F2})";
                }
                else
                {
                    int daysPerSale = (int)Math.Ceiling(1 / averageDailySales);
                    return $"1 Per {daysPerSale} Days (Average: {averageDailySales:F2})";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
    }
}