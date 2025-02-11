//OverShortStocksComputation.cs

namespace PAFProject.Computations
{
    public class OverShortStocksComputation
    {
        private const int STOCK_THRESHOLD_DAYS = 14; // Business rule: 14 days threshold

        public string ComputeOverShortStocks(decimal daysToGo, decimal quantityOnHand, decimal averageDailySales)
        {
            try
            {
                // Round input values for consistency
                decimal roundedDaysToGo = Math.Round(daysToGo, 1);
                int roundedQtyOnHand = (int)Math.Round(quantityOnHand, 0);
                int roundedAvgDailySales = (int)Math.Round(averageDailySales, 0);

                // If Days to Go is greater than threshold or Average Daily Sales is 0, return 0
                if (roundedDaysToGo > STOCK_THRESHOLD_DAYS || roundedAvgDailySales == 0)
                {
                    return "0";
                }

                // Calculate over/short stocks
                // Formula: QtyOnHand - (AvgDailySales * (14 - DaysToGo))
                decimal adjustment = roundedAvgDailySales * (STOCK_THRESHOLD_DAYS - roundedDaysToGo);
                int overShortStocks = roundedQtyOnHand - (int)Math.Round(adjustment, 0);

                return overShortStocks.ToString();
            }
            catch (Exception)
            {
                return "0"; // Return 0 for any errors, matching Excel's IFERROR behavior
            }
        }
    }
}