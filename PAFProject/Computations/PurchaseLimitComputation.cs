namespace PAFProject.Computations
{
    public class PurchaseLimitComputation
    {
        public int ComputePurchaseLimit(decimal averageDailySales, int days)
        {
            try
            {
                // Compute purchase limit as average daily sales * selected days
                decimal result = averageDailySales * days;
                // Round to whole number
                return (int)Math.Round(result, 0);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int GetDaysFromSelection(string selection)
        {
            return selection.ToLower() switch
            {
                "7 days" => 7,
                "15 days" => 15,
                "30 days" => 30,
                _ => 7  // Default to 7 days
            };
        }
    }
}