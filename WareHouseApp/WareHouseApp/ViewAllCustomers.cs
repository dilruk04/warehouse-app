using System;
using System.Windows.Forms;
using WareHouseApp.Managers; // For CustomerManager class
using System.Collections.Generic; // For List<T>
using WareHouseApp.Models; // For Customer class

namespace WareHouseApp
{
    public partial class ViewAllCustomers : Form
    {
        private CustomerManager customerManager = new CustomerManager();

        public ViewAllCustomers()
        {
            InitializeComponent();
            // Optional: Customize DataGridView columns if needed
            // For example, setting column headers manually if auto-generation is off or undesirable
            // dataGridViewCustomers.AutoGenerateColumns = false; // If you want manual control
            // dataGridViewCustomers.Columns.Add("CustomerID", "Customer ID");
            // ... and so on for other columns
        }

        // This method will be called when the form loads and when the Refresh button is clicked
        private void LoadCustomerData()
        {
            try
            {
                List<Customer> customers = customerManager.GetAllItems();
                // Set the DataSource of the DataGridView to the list of customers
                dataGridViewCustomers.DataSource = customers;

                if (customers.Count == 0)
                {
                    MessageBox.Show("No customer records found in the database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading customer data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error loading customers: {ex.ToString()}"); // Log the full error
                dataGridViewCustomers.DataSource = null; // Clear data grid on error
            }
        }

        // Event handler for when the form loads
        private void ViewAllCustomers_Load(object sender, EventArgs e)
        {
            LoadCustomerData(); // Load data as soon as the form appears
        }

        // Event handler for the "Refresh" button
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomerData(); // Reload data
        }

        // Event handler for the "Close" button
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the form
        }
    }
}