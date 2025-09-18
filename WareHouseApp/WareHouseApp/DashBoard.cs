using System;
using System.Drawing; // For Image if needed for mainDash
using System.Windows.Forms;
using WareHouseApp.Managers; // Assuming your managers are in this namespace

namespace WareHouseApp
{
    public partial class DashBoard : Form
    {
        // Private variable to keep track of the currently selected category
        private string _currentCategory = string.Empty;

        public DashBoard()
        {
            InitializeComponent();
            // Initially hide the CRUD action panel
            crudActionPanel.Visible = false;
        }

        /// <summary>
        /// Handles clicks on the Inventory, Customers, and Employees category buttons.
        /// </summary>
        private void btnCategory_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                _currentCategory = clickedButton.Text; // "Inventory", "Customers", or "Employees"
                UpdateCrudButtons(_currentCategory);
                crudActionPanel.Visible = true; // Show the CRUD action panel
            }
        }

        /// <summary>
        /// Updates the text of the CRUD buttons based on the selected category.
        /// </summary>
        /// <param name="category">The selected category (e.g., "Inventory", "Customers").</param>
        private void UpdateCrudButtons(string category)
        {
            btnAdd.Text = $"Add New {category.Singularize()}"; // Using Singularize for better text
            btnViewAll.Text = $"View All {category}";
            btnUpdate.Text = $"Update {category.Singularize()}";
            btnDelete.Text = $"Delete {category.Singularize()}";
        }

        /// <summary>
        /// Handles clicks on the Add, View All, Update, and Delete buttons.
        /// This is where you would call the relevant forms or manager methods.
        /// </summary>
        private void btnCrudAction_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                string action = clickedButton.Text.Split(' ')[0]; // "Add", "View", "Update", "Delete"

                // This is a placeholder. You would replace these MessageBoxes
                // with actual calls to open new forms or execute manager methods.
                switch (_currentCategory)
                {
                    case "Inventory":
                        HandleInventoryAction(action);
                        break;
                    case "Customers":
                        HandleCustomerAction(action);
                        break;
                    case "Employees":
                        HandleEmployeeAction(action);
                        break;
                    default:
                        MessageBox.Show($"No category selected or unknown category: {_currentCategory}");
                        break;
                }
            }
        }

        // --- Helper methods to handle actions for each category ---
        private void HandleInventoryAction(string action)
        {
            switch (action)
            {
                case "Add":
                    AddInventory addInventory = new AddInventory(); 
                    addInventory.Show();
                    break;
                case "View":
                    ViewAllInventory viewAllInventory = new ViewAllInventory();
                    viewAllInventory.Show();
                    break;
                case "Update":
                    UpdateInventory updateInventory = new UpdateInventory();
                    updateInventory.Show();
                    break;
                case "Delete":
                    DeleteInventory deleteInventory = new DeleteInventory();    
                    deleteInventory.Show();
                    break;
            }
        }

        private void HandleCustomerAction(string action)
        {
            switch (action)
            {
                case "Add":
                    AddCustomer customer = new AddCustomer();
                    customer.Show();
                    break;
                case "View":
                    ViewAllCustomers viewAllCustomers = new ViewAllCustomers();
                    viewAllCustomers.Show();
                    break;
                case "Update":
                    UpdateCustomer updateCustomer = new UpdateCustomer();
                    updateCustomer.Show();
                    break;
                case "Delete":
                    DeleteCustomer deleteCustomer = new DeleteCustomer();
                    deleteCustomer.Show();
                    break;
            }
        }

        private void HandleEmployeeAction(string action)
        {
            switch (action)
            {
                case "Add":
                    AddEmployee employee = new AddEmployee();
                    employee.Show();
                    break;
                case "View":
                    ViewAllEmployee viewAllEmployee = new ViewAllEmployee();
                    viewAllEmployee.Show();
                    break;
                case "Update":
                    UpdateEmployee updateEmployee = new UpdateEmployee();
                    updateEmployee.Show();
                    break;
                case "Delete":
                    DeleteEmployee deleteEmployee = new DeleteEmployee();
                    deleteEmployee.Show();
                    break;
            }
        }
    }

    // You might want to add a simple extension method for singularization
    // Or use a more robust library like Humanizer if dealing with complex pluralization
    public static class StringExtensions
    {
        public static string Singularize(this string text)
        {
            if (text.EndsWith("s", StringComparison.OrdinalIgnoreCase) && text.Length > 1)
            {
                return text.Substring(0, text.Length - 1);
            }
            return text;
        }
    }
}