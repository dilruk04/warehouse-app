using System;
using System.Windows.Forms;
using WareHouseApp.Managers; // For InventoryManager class
using System.Collections.Generic; // For List<T>
using WareHouseApp.Models; // For InventoryItem class

namespace WareHouseApp
{
    public partial class ViewAllInventory : Form
    {
        private InventoryManager inventoryManager = new InventoryManager();

        public ViewAllInventory()
        {
            InitializeComponent();
            // Optional: Customize DataGridView columns if needed
            // For example, setting column headers manually if auto-generation is off or undesirable
            // dataGridViewInventory.AutoGenerateColumns = false; // If you want manual control
            // dataGridViewInventory.Columns.Add("ItemID", "Item ID");
            // ... and so on for other columns
        }

        // This method will be called when the form loads and when the Refresh button is clicked
        private void LoadInventoryData()
        {
            try
            {
                List<InventoryItem> items = inventoryManager.GetAllItems();
                // Set the DataSource of the DataGridView to the list of items
                dataGridViewInventory.DataSource = items;

                if (items.Count == 0)
                {
                    MessageBox.Show("No inventory items found in the database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading inventory data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error loading inventory: {ex.ToString()}"); // Log the full error
                dataGridViewInventory.DataSource = null; // Clear data grid on error
            }
        }

        // Event handler for when the form loads
        private void ViewAllInventory_Load(object sender, EventArgs e)
        {
            LoadInventoryData(); // Load data as soon as the form appears
        }

        // Event handler for the "Refresh" button
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadInventoryData(); // Reload data
        }

        // Event handler for the "Close" button
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the form
        }
    }
}