// BranchDropdownManager.cs
// This file handles loading branch names into a KryptonDropButton.
using Krypton.Toolkit;
using PAFProject.Models;

namespace PAFProject.Design
{
    public class BranchDropdownManager
    {
        private readonly BranchDataAccess _branchDataAccess;
        private readonly KryptonDropButton _branchSelectDropdown;
        private readonly KryptonTextBox _branchNameTextBox;

        public BranchDropdownManager(KryptonDropButton branchSelectDropdown, KryptonTextBox branchNameTextBox)  // Updated constructor
        {
            _branchSelectDropdown = branchSelectDropdown;
            _branchNameTextBox = branchNameTextBox;
            _branchDataAccess = new BranchDataAccess();
            InitializeBranchDropdown();
        }

        private void InitializeBranchDropdown()
        {
            try
            {
                List<string> branches = _branchDataAccess.GetBranchList();
                KryptonContextMenu branchMenu = new KryptonContextMenu();
                KryptonContextMenuItems menuItems = new KryptonContextMenuItems();

                foreach (var branch in branches)
                {
                    KryptonContextMenuItem branchItem = new KryptonContextMenuItem(branch);
                    branchItem.Click += (s, e) =>
                    {
                        _branchSelectDropdown.Values.Text = branch;
                        _branchNameTextBox.Text = branch;
                    };
                    menuItems.Items.Add(branchItem);
                }

                branchMenu.Items.Add(menuItems);
                _branchSelectDropdown.KryptonContextMenu = branchMenu;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error initializing branch dropdown: " + ex.Message);
            }
        }
    }
}
