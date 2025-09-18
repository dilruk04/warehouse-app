using System;
using System.Windows.Forms;
using WareHouseApp.Models; 
using WareHouseApp.Managers; 

namespace WareHouseApp
{
    public partial class UpdateInventory : Form
    {
        private InventoryManager inventoryManager = new InventoryManager();
        private int currentItemId = -1; // To store the ID of the item currently loaded for update

        public UpdateInventory()
        {
            InitializeComponent();
            // Disable update fields until an item is loaded
            SetFormFieldsEnabled(false);
        }

        // Helper method to enable/disable input fields
        private void SetFormFieldsEnabled(bool enable)
        {
            txtItemName.Enabled = enable;
            txtDescription.Enabled = enable;
            txtQuantity.Enabled = enable;
            txtPrice.Enabled = enable;
            btnUpdateItem.Enabled = enable;
        }

        // Helper method to clear input fields
        private void ClearFormFields()
        {
            txtItemName.Clear();
            txtDescription.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            currentItemId = -1; // Reset current item ID
        }

        // Event handler for the "Load Item" button
        private void btnLoadItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemIdSearch.Text))
            {
                MessageBox.Show("Please enter an Item ID to load.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtItemIdSearch.Focus();
                return;
            }

            int itemIdToLoad;
            if (!int.TryParse(txtItemIdSearch.Text, out itemIdToLoad))
            {
                MessageBox.Show("Please enter a valid number for Item ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtItemIdSearch.Focus();
                return;
            }

            try
            {
                InventoryItem item = inventoryManager.GetItemByID(itemIdToLoad);

                if (item != null)
                {
                    currentItemId = item.ItemID; // Store the loaded item's ID
                    txtItemName.Text = item.Name;
                    txtDescription.Text = item.Description;
                    txtQuantity.Text = item.Quantity.ToString();
                    txtPrice.Text = item.Price.ToString();
                    SetFormFieldsEnabled(true); // Enable fields for editing
                    MessageBox.Show("Item loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ClearFormFields(); // Clear any previous data
                    SetFormFieldsEnabled(false); // Disable fields
                    MessageBox.Show($"No item found with ID: {itemIdToLoad}", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the item: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error loading item: {ex.ToString()}"); // Log the full error
                ClearFormFields();
                SetFormFieldsEnabled(false);
            }
        }

        // Event handler for the "Update Item" button
        private void btnUpdateItem_Click(object sender, EventArgs e)
        {
            if (currentItemId == -1) // Ensure an item is loaded first
            {
                MessageBox.Show("Please load an item first before attempting to update.", "Operation Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- Input Validation ---
            if (string.IsNullOrWhiteSpace(txtItemName.Text) ||
                string.IsNullOrWhiteSpace(txtQuantity.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Please fill in all required fields (Name, Quantity, Price).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- Data Parsing and Type Conversion ---
            string itemName = txtItemName.Text.Trim();
            string description = txtDescription.Text.Trim();
            int quantity;
            decimal price;

            if (!int.TryParse(txtQuantity.Text, out quantity) || quantity < 0)
            {
                MessageBox.Show("Please enter a valid positive whole number for Quantity.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out price) || price < 0)
            {
                MessageBox.Show("Please enter a valid positive number for Price.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return;
            }

            // --- Create InventoryItem object with updated details ---
            // Use currentItemId to ensure the correct item is updated
            InventoryItem updatedItem = new InventoryItem(currentItemId, itemName, description, quantity, price);

            // --- Call InventoryManager to update item in database ---
            try
            {
                if (inventoryManager.UpdateItem(updatedItem))
                {
                    MessageBox.Show("Item updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Clear fields and disable for next update operation
                    ClearFormFields();
                    SetFormFieldsEnabled(false);
                    txtItemIdSearch.Clear(); // Clear search field too
                    txtItemIdSearch.Focus(); // Focus back to search
                }
                else
                {
                    MessageBox.Show("Failed to update item. An unknown error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentNullException ex) // Catches specific validation from Manager
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex) // Catches if item not found during update
            {
                MessageBox.Show(ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ClearFormFields(); // Clear data if item disappeared
                SetFormFieldsEnabled(false);
                txtItemIdSearch.Clear();
                txtItemIdSearch.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the item: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error updating item: {ex.ToString()}"); // Log full error
            }
        }

        // Event handler for the Cancel button
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the UpdateInventory form
        }
    }
}