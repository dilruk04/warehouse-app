using System;
using System.Windows.Forms;
using WareHouseApp.Managers; // For InventoryManager class

namespace WareHouseApp
{
    public partial class DeleteInventory : Form
    {
        private InventoryManager inventoryManager = new InventoryManager();

        public DeleteInventory()
        {
            InitializeComponent();
        }

        // Event handler for the "Delete Item" button
        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemId.Text))
            {
                MessageBox.Show("Please enter the Item ID to delete.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtItemId.Focus();
                return;
            }

            int itemIdToDelete;
            if (!int.TryParse(txtItemId.Text, out itemIdToDelete))
            {
                MessageBox.Show("Please enter a valid number for Item ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtItemId.Focus();
                return;
            }

            // Confirm deletion with the user
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete item with ID {itemIdToDelete}?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (inventoryManager.DeleteItem(itemIdToDelete))
                    {
                        MessageBox.Show($"Item with ID {itemIdToDelete} deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItemId.Clear(); // Clear the field after successful deletion
                        txtItemId.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete item. An unknown error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (InvalidOperationException ex) // Catches if no item found with ID
                {
                    MessageBox.Show(ex.Message, "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtItemId.Clear(); // Clear input if item not found
                    txtItemId.Focus();
                }
                catch (Exception ex)
                {
                    // Catch any other exceptions (e.g., database connection issues, SQL errors)
                    MessageBox.Show($"An error occurred while deleting the item: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"Error deleting item: {ex.ToString()}"); // Log full error
                }
            }
        }

        // Event handler for the Cancel button
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the DeleteInventory form
        }
    }
}