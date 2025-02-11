using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAFProject.Design
{
    public class LimitSelectionDropdown
    {
        public static void InitializeMonthlyDropdown(KryptonDropButton limitSelectionDropdown)
        {
            // Create KryptonContextMenu
            KryptonContextMenu limitSelectionMenu = new KryptonContextMenu();
            KryptonContextMenuItems limitSelectionItems = new KryptonContextMenuItems();
            KryptonContextMenuItem sevenDays = new KryptonContextMenuItem("7 Days");
            KryptonContextMenuItem fifteenDays = new KryptonContextMenuItem("15 Days");
            KryptonContextMenuItem thirtyDays = new KryptonContextMenuItem("30 Days");

            // Add items to the menu
            limitSelectionItems.Items.Add(sevenDays);
            limitSelectionItems.Items.Add(fifteenDays);
            limitSelectionItems.Items.Add(thirtyDays);
            limitSelectionMenu.Items.Add(limitSelectionItems);

            // Assign menu to the dropdown button
            limitSelectionDropdown.KryptonContextMenu = limitSelectionMenu;

            // Add event handlers for menu item clicks
            sevenDays.Click += (s, ev) => { limitSelectionDropdown.Values.Text = "7 Days"; };
            fifteenDays.Click += (s, ev) => { limitSelectionDropdown.Values.Text = "15 Days"; };
            thirtyDays.Click += (s, ev) => { limitSelectionDropdown.Values.Text = "30 Days"; };
        }
    }
}

