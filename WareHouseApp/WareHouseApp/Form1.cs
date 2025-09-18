using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WareHouseApp.People;
using WareHouseApp.Resources.AppStrings; // Ensure this namespace is correct for LoginPage and other strings

namespace WareHouseApp
{
    public partial class Form1 : Form // Assuming your login form is named Form1
    {
        Admin admin = new Admin();
        LoginPage loginPage = new LoginPage(); // This likely holds your error messages

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Event handler for panel1 paint, currently empty but kept for completeness
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Event handler for label1 click, currently empty but kept for completeness
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // This button typically closes the application or current form
            this.Close();
        }

        private void butLogin_Click(object sender, EventArgs e)
        {
            string UserNameTxt = txtName.Text;
            string PassTxt = txtPassword.Text;

            try
            {
                // Attempt to log in using the Admin class's Login method
                bool loginSuccessful = admin.Login(UserNameTxt, PassTxt);

                if (loginSuccessful)
                {
                    MessageBox.Show(loginPage.LoginSuccessMessageEn, loginPage.LoginSuccessTitleEn, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // If login is successful, hide the current form and show the Dashboard
                    this.Hide();
                    DashBoard dashBoard = new DashBoard();
                    dashBoard.Show();
                }
                else
                {
                    // If login fails (username/password mismatch or role not Admin)
                    MessageBox.Show(loginPage.LoginErrorMessageEn, loginPage.LoginErrorTitleEn, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException argEx)
            {
                // Catch specific exceptions for empty fields
                MessageBox.Show(argEx.Message, loginPage.LoginErrorTitleEn, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Catch general exceptions (e.g., database connection issues)
                Console.WriteLine($"An error occurred during login: {ex}"); // Log full exception for debugging
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", loginPage.LoginErrorTitleEn, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Always clear the password field for security
                txtPassword.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // This button typically opens the registration form
            Register registerForm = new Register();
            registerForm.Show();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            // Event handler for txtName text changed, currently empty but kept for completeness
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // Event handler for txtPassword text changed, currently empty but kept for completeness
        }
    }
}