using System;
using System.Windows.Forms;
using WareHouseApp.Models; // For Customer class
using WareHouseApp.Managers; // For CustomerManager class

namespace WareHouseApp
{
    public partial class AddCustomer : Form
    {
        private CustomerManager customerManager = new CustomerManager();

        public AddCustomer()
        {
            InitializeComponent();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
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

            // Create Customer object
            // Use null for empty optional fields (Email, Phone, Address) if your DB allows NULLs
            // Otherwise, pass empty strings or handle as per your DB schema
            Customer newCustomer = new Customer(
                firstName,
                lastName,
                string.IsNullOrEmpty(email) ? null : email,
                string.IsNullOrEmpty(phone) ? null : phone,
                string.IsNullOrEmpty(address) ? null : address
            );

            // --- Call CustomerManager to add customer to database ---
            try
            {
                if (customerManager.AddItem(newCustomer))
                {
                    MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Clear fields for next entry
                    ClearFormFields();
                }
                else
                {
                    MessageBox.Show("Failed to add customer. An unknown error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentNullException ex) // Catches specific validation from Manager
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Catch any other exceptions (e.g., database connection issues, SQL errors)
                MessageBox.Show($"An error occurred while adding the customer: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error adding customer: {ex.ToString()}"); // Log the full error for debugging
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the AddCustomer form
        }

        private void ClearFormFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtFirstName.Focus(); // Set focus back to the first field
        }
    }
}