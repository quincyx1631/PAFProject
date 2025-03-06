using Krypton.Toolkit;

namespace PAFProject.Design
{
    public class MonthsDropdownManager
    {
        public static void InitializeMonthlyDropdown(KryptonDropButton monthlyDropdown, Action<bool> onPeriodChanged)
        {
            KryptonContextMenu monthlyMenu = new KryptonContextMenu();
            KryptonContextMenuItems menuItems = new KryptonContextMenuItems();
            KryptonContextMenuItem threeMonthsItem = new KryptonContextMenuItem("3 Months");
            KryptonContextMenuItem sixMonthsItem = new KryptonContextMenuItem("6 Months");

            threeMonthsItem.Click += (s, ev) =>
            {
                monthlyDropdown.Values.Text = "3 Months";
                onPeriodChanged?.Invoke(true);
            };

            sixMonthsItem.Click += (s, ev) =>
            {
                monthlyDropdown.Values.Text = "6 Months";
                onPeriodChanged?.Invoke(false);
            };

            menuItems.Items.Add(threeMonthsItem);
            menuItems.Items.Add(sixMonthsItem);
            monthlyMenu.Items.Add(menuItems);

            monthlyDropdown.KryptonContextMenu = monthlyMenu;
        }
    }
}
