using Krypton.Toolkit;

namespace PAFProject.Design
{
    public class MonthsDropdownManager
    {
        public static void InitializeMonthlyDropdown(KryptonDropButton monthlyDropdown)
        {
            // Create KryptonContextMenu
            KryptonContextMenu monthlyMenu = new KryptonContextMenu();
            KryptonContextMenuItems menuItems = new KryptonContextMenuItems();
            KryptonContextMenuItem threeMonthsItem = new KryptonContextMenuItem("3 Months");
            KryptonContextMenuItem sixMonthsItem = new KryptonContextMenuItem("6 Months");

            // Add items to the menu
            menuItems.Items.Add(threeMonthsItem);
            menuItems.Items.Add(sixMonthsItem);
            monthlyMenu.Items.Add(menuItems);

            // Assign menu to the dropdown button
            monthlyDropdown.KryptonContextMenu = monthlyMenu;

            // Add event handlers for menu item clicks
            threeMonthsItem.Click += (s, ev) => { monthlyDropdown.Values.Text = "3 Months"; };
            sixMonthsItem.Click += (s, ev) => { monthlyDropdown.Values.Text = "6 Months"; };
        }
    }
}
