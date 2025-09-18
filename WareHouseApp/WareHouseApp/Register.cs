using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WareHouseApp.People; // To access the Admin class
using WareHouseApp.Resources.AppStrings; // To access LoginError messages (optional, can use generic)

namespace WareHouseApp
{
    public partial class Register : Form
    {
        // Instantiate Admin and LoginPage helper classes
        private Admin admin = new Admin();
        private LoginPage loginPage = new LoginPage(); // For consistent error messages

        public Register()
        {
            InitializeComponent();
            // Optional: Set placeholder or clear fields on load
            txtName.Text = "";
            txtPassword.Text = "";
            textBox1.Text = ""; // Assuming textBox1 is Confirm Password
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Event handler for your "Inventory Management System" label, currently empty.
        }

        private void butLogin_Click(object sender, EventArgs e)
        {
            // This button is labeled "REGISTER" in your designer code.
            string username = txtName.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = textBox1.Text; // Assuming textBox1 is for Confirm Password

            // --- Client-Side Validation ---
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter a password.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please confirm your password.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please re-enter.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Clear();
                textBox1.Clear();
                txtPassword.Focus();
                return;
            }

            // Optional: Add password complexity requirements (e.g., min length)
            if (password.Length < 6) // Example: minimum 6 characters
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Clear();
                textBox1.Clear();
                txtPassword.Focus();
                return;
            }


            // --- Call RegisterNewAdmin function ---
            try
            {
                bool isRegistered = admin.RegisterNewAdmin(username, password);

                if (isRegistered)
                {
                    MessageBox.Show("Registration successful! You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Optionally clear fields and close form after successful registration
                    txtName.Clear();
                    txtPassword.Clear();
                    textBox1.Clear();
                    this.Close(); // Close the registration form and return to the previous one (e.g., login)
                }
                else
                {
                    // This else block might be redundant if RegisterNewAdmin always throws on failure,
                    // but it's good for clarity if it returns false for some specific non-exception failures.
                    MessageBox.Show("Registration failed. Please try again.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (InvalidOperationException ex)
            {
                // Catch specific exception for username already existing
                MessageBox.Show(ex.Message, "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus(); // Suggest user to change username
            }
            catch (Exception ex)
            {
                // Catch any other exceptions (e.g., database connection issues, other errors from Admin class)
                Console.WriteLine($"Registration Error: {ex.ToString()}"); // Log full exception for debugging
                MessageBox.Show($"An error occurred during registration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Always clear password fields for security
                txtPassword.Text = "";
                textBox1.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // This button is likely the 'X' button to close the form.
            this.Close();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // You might want to add other event handlers for text boxes if needed,
        // e.g., for 'Enter' key press to submit.
    }
}