using System;
using System.Windows.Forms;
using WareHouseApp.Managers; // For EmployeeManager class

namespace WareHouseApp
{
    public partial class DeleteEmployee : Form
    {
        private EmployeeManager employeeManager = new EmployeeManager();

        public DeleteEmployee()
        {
            InitializeComponent();
        }

        // Event handler for the "Delete Employee" button
        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmployeeId.Text))
            {
                MessageBox.Show("Please enter the Employee ID to delete.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmployeeId.Focus();
                return;
            }

            int employeeIdToDelete;
            if (!int.TryParse(txtEmployeeId.Text, out employeeIdToDelete))
            {
                MessageBox.Show("Please enter a valid number for Employee ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmployeeId.Focus();
                return;
            }

            // Confirm deletion with the user
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete employee with ID {employeeIdToDelete}?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (employeeManager.DeleteItem(employeeIdToDelete))
                    {
                        MessageBox.Show($"Employee with ID {employeeIdToDelete} deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtEmployeeId.Clear(); // Clear the field after successful deletion
                        txtEmployeeId.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete employee. An unknown error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (InvalidOperationException ex) // Catches if no employee found with ID
                {
                    MessageBox.Show(ex.Message, "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmployeeId.Clear(); // Clear input if employee not found
                    txtEmployeeId.Focus();
                }
                catch (Exception ex)
                {
                    // Catch any other exceptions (e.g., database connection issues, SQL errors)
                    MessageBox.Show($"An error occurred while deleting the employee: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"Error deleting employee: {ex.ToString()}"); // Log full error
                }
            }
        }

        // Event handler for the Cancel button
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the DeleteEmployee form
        }
    }
}