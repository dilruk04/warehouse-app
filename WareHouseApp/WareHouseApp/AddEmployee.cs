using System;
using System.Windows.Forms;
using WareHouseApp.Models; // For Employee class
using WareHouseApp.Managers; // For EmployeeManager class

namespace WareHouseApp
{
    public partial class AddEmployee : Form
    {
        private EmployeeManager employeeManager = new EmployeeManager();

        public AddEmployee()
        {
            InitializeComponent();
            // Set default HireDate to today
            dtpHireDate.Value = DateTime.Today;
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            // --- Input Validation ---
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtPosition.Text) ||
                string.IsNullOrWhiteSpace(txtSalary.Text))
            {
                MessageBox.Show("Please fill in First Name, Last Name, Position, and Salary.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtSalary.Text, out decimal salary))
            {
                MessageBox.Show("Please enter a valid number for Salary.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSalary.Focus();
                return;
            }
            if (salary < 0)
            {
                MessageBox.Show("Salary cannot be negative.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSalary.Focus();
                return;
            }

            // --- Data Collection ---
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string position = txtPosition.Text.Trim();
            DateTime hireDate = dtpHireDate.Value;
            string contactNumber = txtContactNumber.Text.Trim();
            string email = txtEmail.Text.Trim();

            // Create Employee object
            // Use null for empty optional fields (ContactNumber, Email) if your DB allows NULLs
            Employee newEmployee = new Employee(
                firstName,
                lastName,
                position,
                hireDate,
                salary,
                string.IsNullOrEmpty(contactNumber) ? null : contactNumber,
                string.IsNullOrEmpty(email) ? null : email
            );

            // --- Call EmployeeManager to add employee to database ---
            try
            {
                if (employeeManager.AddItem(newEmployee))
                {
                    MessageBox.Show("Employee added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Clear fields for next entry
                    ClearFormFields();
                }
                else
                {
                    MessageBox.Show("Failed to add employee. An unknown error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentNullException ex) // Catches specific validation from Manager
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Catch any other exceptions (e.g., database connection issues, SQL errors)
                MessageBox.Show($"An error occurred while adding the employee: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error adding employee: {ex.ToString()}"); // Log the full error for debugging
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the AddEmployee form
        }

        private void ClearFormFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPosition.Clear();
            dtpHireDate.Value = DateTime.Today; // Reset date to today
            txtSalary.Clear();
            txtContactNumber.Clear();
            txtEmail.Clear();
            txtFirstName.Focus(); // Set focus back to the first field
        }
    }
}