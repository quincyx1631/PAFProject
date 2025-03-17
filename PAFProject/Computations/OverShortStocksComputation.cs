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
                if (daysToGo > STOCK_THRESHOLD_DAYS)
                {
                    return "0";
                }

                // Calculate over/short stocks
                // Formula: QtyOnHand - (AvgDailySales * (14 - DaysToGo))
                decimal adjustment = averageDailySales * (STOCK_THRESHOLD_DAYS - daysToGo);
                decimal overShortStocks = quantityOnHand - adjustment;

                return Math.Round(overShortStocks, 0).ToString();
            }
            catch (Exception)
            {
                return "0"; // Return 0 for any errors, matching Excel's IFERROR behavior
            }
        }
    }
}