using System;
namespace PAFProject.Computations
{
    public class BudgetComputation
    {
        public string ComputeBudget(int totalQuantity, decimal averageCost)
        {
            try
            {
                if (averageCost == 0)
                {
                    return "0.00";
                }

                decimal totalBudget = totalQuantity * averageCost;
                // Round to 2 decimal places for currency
                decimal roundedBudget = Math.Round(totalBudget, 2);
                // Format as currency with 2 decimal places
                return roundedBudget.ToString("N2");
            }
            catch (Exception)
            {
                return "0.00";
            }
        }
    }
}