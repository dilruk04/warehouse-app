using System;
using System.Windows.Forms;
using WareHouseApp.Managers; // For EmployeeManager class
using System.Collections.Generic; // For List<T>
using WareHouseApp.Models; // For Employee class

namespace WareHouseApp
{
    public partial class ViewAllEmployee : Form
    {
        private EmployeeManager employeeManager = new EmployeeManager();

        public ViewAllEmployee()
        {
            InitializeComponent();
            // Optional: Customize DataGridView columns if needed
            // For example, setting column headers manually if auto-generation is off or undesirable
            // dataGridViewEmployees.AutoGenerateColumns = false; // If you want manual control
            // dataGridViewEmployees.Columns.Add("EmployeeID", "Employee ID");
            // ... and so on for other columns
        }

        // This method will be called when the form loads and when the Refresh button is clicked
        private void LoadEmployeeData()
        {
            try
            {
                List<Employee> employees = employeeManager.GetAllItems();
                // Set the DataSource of the DataGridView to the list of employees
                dataGridViewEmployees.DataSource = employees;

                if (employees.Count == 0)
                {
                    MessageBox.Show("No employee records found in the database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading employee data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error loading employees: {ex.ToString()}"); // Log the full error
                dataGridViewEmployees.DataSource = null; // Clear data grid on error
            }
        }

        // Event handler for when the form loads
        private void ViewAllEmployee_Load(object sender, EventArgs e)
        {
            LoadEmployeeData(); // Load data as soon as the form appears
        }

        // Event handler for the "Refresh" button
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadEmployeeData(); // Reload data
        }

        // Event handler for the "Close" button
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the form
        }
    }
}