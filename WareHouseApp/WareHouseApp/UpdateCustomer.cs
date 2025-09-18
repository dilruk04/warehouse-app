using System;
using System.Windows.Forms;
using WareHouseApp.Models; // For Customer class
using WareHouseApp.Managers; // For CustomerManager class

namespace WareHouseApp
{
    public partial class UpdateCustomer : Form
    {
        private CustomerManager customerManager = new CustomerManager();
        private int currentCustomerId = -1; // To store the ID of the customer currently loaded for update

        public UpdateCustomer()
        {
            InitializeComponent();
            // Disable update fields until a customer is loaded
            SetFormFieldsEnabled(false);
        }

        // Helper method to enable/disable input fields
        private void SetFormFieldsEnabled(bool enable)
        {
            txtFirstName.Enabled = enable;
            txtLastName.Enabled = enable;
            txtEmail.Enabled = enable;
            txtPhone.Enabled = enable;
            txtAddress.Enabled = enable;
            btnUpdateCustomer.Enabled = enable;
        }

        // Helper method to clear input fields
        private void ClearFormFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            currentCustomerId = -1; // Reset current customer ID
        }

        // Event handler for the "Load Customer" button
        private void btnLoadCustomer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerIdSearch.Text))
            {
                MessageBox.Show("Please enter a Customer ID to load.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerIdSearch.Focus();
                return;
            }

            int customerIdToLoad;
            if (!int.TryParse(txtCustomerIdSearch.Text, out customerIdToLoad))
            {
                MessageBox.Show("Please enter a valid number for Customer ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerIdSearch.Focus();
                return;
            }

            try
            {
                Customer customer = customerManager.GetItemByID(customerIdToLoad);

                if (customer != null)
                {
                    currentCustomerId = customer.CustomerID; // Store the loaded customer's ID
                    txtFirstName.Text = customer.FirstName;
                    txtLastName.Text = customer.LastName;
                    txtEmail.Text = customer.Email;
                    txtPhone.Text = customer.Phone;
                    txtAddress.Text = customer.Address;
                    SetFormFieldsEnabled(true); // Enable fields for editing
                    MessageBox.Show("Customer loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ClearFormFields(); // Clear any previous data
                    SetFormFieldsEnabled(false); // Disable fields
                    MessageBox.Show($"No customer found with ID: {customerIdToLoad}", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the customer: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error loading customer: {ex.ToString()}"); // Log the full error
                ClearFormFields();
                SetFormFieldsEnabled(false);
            }
        }

        // Event handler for the "Update Customer" button
        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            if (currentCustomerId == -1) // Ensure a customer is loaded first
            {
                MessageBox.Show("Please load a customer first before attempting to update.", "Operation Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- Input Validation ---
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please fill in First Name and Last Name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- Data Collection ---
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();

            // Create Customer object with updated details
            Customer updatedCustomer = new Customer(
                currentCustomerId, // Use currentCustomerId to ensure the correct customer is updated
                firstName,
                lastName,
                string.IsNullOrEmpty(email) ? null : email,
                string.IsNullOrEmpty(phone) ? null : phone,
                string.IsNullOrEmpty(address) ? null : address
            );

            // --- Call CustomerManager to update customer in database ---
            try
            {
                if (customerManager.UpdateItem(updatedCustomer))
                {
                    MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Clear fields and disable for next update operation
                    ClearFormFields();
                    SetFormFieldsEnabled(false);
                    txtCustomerIdSearch.Clear(); // Clear search field too
                    txtCustomerIdSearch.Focus(); // Focus back to search
                }
                else
                {
                    MessageBox.Show("Failed to update customer. An unknown error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentNullException ex) // Catches specific validation from Manager
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex) // Catches if customer not found during update
            {
                MessageBox.Show(ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ClearFormFields(); // Clear data if customer disappeared
                SetFormFieldsEnabled(false);
                txtCustomerIdSearch.Clear();
                txtCustomerIdSearch.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the customer: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error updating customer: {ex.ToString()}"); // Log full error
            }
        }

        // Event handler for the Cancel button
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the UpdateCustomer form
        }
    }
}