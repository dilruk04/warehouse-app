using System;
using System.Windows.Forms;
using WareHouseApp.Models; // For Employee class
using WareHouseApp.Managers; // For EmployeeManager class

namespace WareHouseApp
{
    public partial class UpdateEmployee : Form
    {
        private EmployeeManager employeeManager = new EmployeeManager();
        private int currentEmployeeId = -1; // To store the ID of the employee currently loaded for update

        public UpdateEmployee()
        {
            InitializeComponent();
            // Disable update fields until an employee is loaded
            SetFormFieldsEnabled(false);
            // Set default HireDate to today
            dtpHireDate.Value = DateTime.Today;
        }

        // Helper method to enable/disable input fields
        private void SetFormFieldsEnabled(bool enable)
        {
            txtFirstName.Enabled = enable;
            txtLastName.Enabled = enable;
            txtPosition.Enabled = enable;
            dtpHireDate.Enabled = enable;
            txtSalary.Enabled = enable;
            txtContactNumber.Enabled = enable;
            txtEmail.Enabled = enable;
            btnUpdateEmployee.Enabled = enable;
        }

        // Helper method to clear input fields
        private void ClearFormFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPosition.Clear();
            dtpHireDate.Value = DateTime.Today; // Reset date to today
            txtSalary.Clear();
            txtContactNumber.Clear();
            txtEmail.Clear();
            currentEmployeeId = -1; // Reset current employee ID
        }

        // Event handler for the "Load Employee" button
        private void btnLoadEmployee_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmployeeIdSearch.Text))
            {
                MessageBox.Show("Please enter an Employee ID to load.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmployeeIdSearch.Focus();
                return;
            }

            int employeeIdToLoad;
            if (!int.TryParse(txtEmployeeIdSearch.Text, out employeeIdToLoad))
            {
                MessageBox.Show("Please enter a valid number for Employee ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmployeeIdSearch.Focus();
                return;
            }

            try
            {
                Employee employee = employeeManager.GetItemByID(employeeIdToLoad);

                if (employee != null)
                {
                    currentEmployeeId = employee.EmployeeID; // Store the loaded employee's ID
                    txtFirstName.Text = employee.FirstName;
                    txtLastName.Text = employee.LastName;
                    txtPosition.Text = employee.Position;
                    dtpHireDate.Value = employee.HireDate;
                    txtSalary.Text = employee.Salary.ToString();
                    txtContactNumber.Text = employee.ContactNumber;
                    txtEmail.Text = employee.Email;
                    SetFormFieldsEnabled(true); // Enable fields for editing
                    MessageBox.Show("Employee loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ClearFormFields(); // Clear any previous data
                    SetFormFieldsEnabled(false); // Disable fields
                    MessageBox.Show($"No employee found with ID: {employeeIdToLoad}", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the employee: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error loading employee: {ex.ToString()}"); // Log the full error
                ClearFormFields();
                SetFormFieldsEnabled(false);
            }
        }

        // Event handler for the "Update Employee" button
        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            if (currentEmployeeId == -1) // Ensure an employee is loaded first
            {
                MessageBox.Show("Please load an employee first before attempting to update.", "Operation Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

            // Create Employee object with updated details
            Employee updatedEmployee = new Employee(
                currentEmployeeId, // Use currentEmployeeId to ensure the correct employee is updated
                firstName,
                lastName,
                position,
                hireDate,
                salary,
                string.IsNullOrEmpty(contactNumber) ? null : contactNumber,
                string.IsNullOrEmpty(email) ? null : email
            );

            // --- Call EmployeeManager to update employee in database ---
            try
            {
                if (employeeManager.UpdateItem(updatedEmployee))
                {
                    MessageBox.Show("Employee updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Clear fields and disable for next update operation
                    ClearFormFields();
                    SetFormFieldsEnabled(false);
                    txtEmployeeIdSearch.Clear(); // Clear search field too
                    txtEmployeeIdSearch.Focus(); // Focus back to search
                }
                else
                {
                    MessageBox.Show("Failed to update employee. An unknown error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentNullException ex) // Catches specific validation from Manager
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex) // Catches if employee not found during update
            {
                MessageBox.Show(ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ClearFormFields(); // Clear data if employee disappeared
                SetFormFieldsEnabled(false);
                txtEmployeeIdSearch.Clear();
                txtEmployeeIdSearch.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the employee: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error updating employee: {ex.ToString()}"); // Log full error
            }
        }

        // Event handler for the Cancel button
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the UpdateEmployee form
        }
    }
}