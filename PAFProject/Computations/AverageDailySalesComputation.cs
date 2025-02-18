using PAFProject.Models;

public class AverageDailySalesComputation
{
    private readonly SalesDataAccess _salesDataAccess;
    private const int DAYS_IN_THREE_MONTHS = 90;
    private const int DAYS_IN_SIX_MONTHS = 180;
    private const int DAYS_IN_A_WEEK = 7;
    private bool _isThreeMonths = true;

    public AverageDailySalesComputation()
    {
        _salesDataAccess = new SalesDataAccess();
    }

    public void SetPeriod(bool isThreeMonths)
    {
        _isThreeMonths = isThreeMonths;
    }

    public decimal GetNumericAverageDailySales(string productDescription)
    {
        try
        {
            var salesData = _salesDataAccess.GetSalesData(productDescription, _isThreeMonths);
            var productSales = salesData.FirstOrDefault();
            if (productSales == null)
            {
                return 0;
            }

            int daysInPeriod = _isThreeMonths ? DAYS_IN_THREE_MONTHS : DAYS_IN_SIX_MONTHS;
            decimal averageDailySales = productSales.Quantity / (decimal)daysInPeriod;
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
            decimal averageDailySales = GetNumericAverageDailySales(productDescription); // Keep original logic
            string periodText = _isThreeMonths ? "3 Months" : "6 Months";

            if (averageDailySales >= 1)
            {
                int roundedSales = (int)Math.Round(averageDailySales, 0);
                return $"{roundedSales} Per Day ({periodText} Average: {averageDailySales:F2})";
            }
            else if (averageDailySales >= 1m / DAYS_IN_A_WEEK)
            {
                int weeklySales = (int)Math.Ceiling(averageDailySales * DAYS_IN_A_WEEK);
                return $"{weeklySales} Per Week ({periodText} Average: {averageDailySales:F2})";
            }
            else
            {
                int daysPerSale = (int)Math.Ceiling(1 / averageDailySales);
                return $"1 Per {daysPerSale} Days ({periodText} Average: {averageDailySales:F2})";
            }
        }
        catch (Exception ex)
        {
            return "0";
        }
    }

    // New function to extract precise value with 5 decimals (for calculations)
    public decimal GetPreciseAverageDailySales(string productDescription)
    {
        return Math.Round(GetNumericAverageDailySales(productDescription), 5);
    }
    public string ComputeAverageDailySalesFromValue(decimal averageDailySales)
    {
        try
        {
            string periodText = _isThreeMonths ? "3 Months" : "6 Months";

            if (averageDailySales >= 1)
            {
                int roundedSales = (int)Math.Round(averageDailySales, 0);
                return $"{roundedSales} Per Day ({periodText} Average: {averageDailySales:F2})";
            }
            else if (averageDailySales >= 1m / DAYS_IN_A_WEEK)
            {
                int weeklySales = (int)Math.Ceiling(averageDailySales * DAYS_IN_A_WEEK);
                return $"{weeklySales} Per Week ({periodText} Average: {averageDailySales:F2})";
            }
            else
            {
                int daysPerSale = (int)Math.Ceiling(1 / averageDailySales);
                return $"1 Per {daysPerSale} Days ({periodText} Average: {averageDailySales:F2})";
            }
        }
        catch (Exception ex)
        {
            return "0";
        }
    }
}