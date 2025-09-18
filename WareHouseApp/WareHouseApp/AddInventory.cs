using System;
using System.Windows.Forms;
using WareHouseApp.Models; 
using WareHouseApp.Managers; 

namespace WareHouseApp
{
    public partial class AddInventory : Form
    {
        private InventoryManager inventoryManager = new InventoryManager();

        public AddInventory()
        {
            InitializeComponent();
            // Optional: Initialize fields or focus here if needed on form load
        }

        // This is the method that was reported as missing
        private void btnAddItem_Click(object sender, EventArgs e)
        {
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
            string description = txtDescription.Text.Trim(); // Description can be empty
            int quantity;
            decimal price;

            // Validate Quantity is a positive integer
            if (!int.TryParse(txtQuantity.Text, out quantity) || quantity < 0)
            {
                MessageBox.Show("Please enter a valid positive whole number for Quantity.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return;
            }

            // Validate Price is a positive decimal
            if (!decimal.TryParse(txtPrice.Text, out price) || price < 0)
            {
                MessageBox.Show("Please enter a valid positive number for Price.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return;
            }

            // --- Create InventoryItem object ---
            InventoryItem newItem = new InventoryItem(itemName, description, quantity, price);

            // --- Call InventoryManager to add item to database ---
            try
            {
                if (inventoryManager.AddItem(newItem))
                {
                    MessageBox.Show("Item added to inventory successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Clear fields for next entry
                    ClearFormFields();
                }
                else
                {
                    MessageBox.Show("Failed to add item. An unknown error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentNullException ex) // Catches specific validation from Manager (e.g., null item name)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Catch any other exceptions (e.g., database connection issues, SQL errors)
                MessageBox.Show($"An error occurred while adding the item: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error adding item: {ex.ToString()}"); // Log the full error for debugging
            }
        }

        // Event handler for the Cancel button
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the AddInventory form
        }

        // Helper method to clear input fields
        private void ClearFormFields()
        {
            txtItemName.Clear();
            txtDescription.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            txtItemName.Focus(); // Set focus back to the first field
        }

        // You might want to add other event handlers here if needed,
        // for example, for form load (AddInventory_Load) or text box changes.
    }
}