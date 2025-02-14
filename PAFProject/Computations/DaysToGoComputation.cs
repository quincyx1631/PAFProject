//DaysToGoComputation.cs
namespace PAFProject.Computations
{
    public class DaysToGoComputation
    {
        public string ComputeDaysToGo(decimal quantityOnHand, decimal averageDailySales)
        {
            try
            {
                // Check if average daily sales is zero to avoid division by zero
                if (averageDailySales == 0)
                {
                    return "0";
                }

                // Calculate days to go using raw decimal values
                decimal daysToGo = quantityOnHand / averageDailySales;

                // Round to 1 decimal place
                decimal roundedDaysToGo = Math.Round(daysToGo, 1);

                // Convert to string with one decimal place
                return roundedDaysToGo.ToString("F1");
            }
            catch (Exception)
            {
                return "Error";
            }
        }
    }
}