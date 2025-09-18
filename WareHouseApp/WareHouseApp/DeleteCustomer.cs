using System;
using System.Windows.Forms;
using WareHouseApp.Managers; // For CustomerManager class

namespace WareHouseApp
{
    public partial class DeleteCustomer : Form
    {
        private CustomerManager customerManager = new CustomerManager();

        public DeleteCustomer()
        {
            InitializeComponent();
        }

        // Event handler for the "Delete Customer" button
        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerId.Text))
            {
                MessageBox.Show("Please enter the Customer ID to delete.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerId.Focus();
                return;
            }

            int customerIdToDelete;
            if (!int.TryParse(txtCustomerId.Text, out customerIdToDelete))
            {
                MessageBox.Show("Please enter a valid number for Customer ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerId.Focus();
                return;
            }

            // Confirm deletion with the user
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete customer with ID {customerIdToDelete}?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (customerManager.DeleteItem(customerIdToDelete))
                    {
                        MessageBox.Show($"Customer with ID {customerIdToDelete} deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCustomerId.Clear(); // Clear the field after successful deletion
                        txtCustomerId.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete customer. An unknown error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (InvalidOperationException ex) // Catches if no customer found with ID
                {
                    MessageBox.Show(ex.Message, "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCustomerId.Clear(); // Clear input if customer not found
                    txtCustomerId.Focus();
                }
                catch (Exception ex)
                {
                    // Catch any other exceptions (e.g., database connection issues, SQL errors)
                    MessageBox.Show($"An error occurred while deleting the customer: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"Error deleting customer: {ex.ToString()}"); // Log full error
                }
            }
        }

        // Event handler for the Cancel button
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the DeleteCustomer form
        }
    }
}