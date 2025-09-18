using System;
using System.Data.SqlClient; // Required for SQL Server interaction
using System.Security.Cryptography; // Required for password hashing (SHA256)
using System.Text; // Required for Encoding

namespace WareHouseApp.People
{
    class Admin : Person // Assuming Person is your base class
    {
        // IMPORTANT: Verify and update this connection string.
        // You can find this in Visual Studio:
        // 1. In Solution Explorer, double-click 'WarehouseDB.mdf' or open 'SQL Server Object Explorer'.
        // 2. Right-click on 'WarehouseDB.mdf' and select 'Properties'.
        // 3. Copy the 'Connection String' value. Ensure '|DataDirectory|' is used if the .mdf is in your App_Data folder.
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""E:\Assignments\13. June 2025\CS107.3 Object Oriented Programming with C# (1)\WareHouseApp - repeat coursework\WareHouseApp - repeat coursework\WareHouseApp\WarehouseDB.mdf"";Integrated Security=True;";

        /// <summary>
        /// Hashes a plain text password using SHA256.
        /// </summary>
        /// <param name="password">The plain text password to hash.</param>
        /// <returns>The SHA256 hash of the password as a hexadecimal string.</returns>
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // "x2" formats byte as two hexadecimal digits
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Authenticates an admin user against the database.
        /// </summary>
        /// <param name="username">The username to authenticate.</param>
        /// <param name="password">The plain text password.</param>
        /// <returns>True if login is successful, false otherwise.</returns>
        /// <exception cref="ArgumentException">Thrown if username or password is null or empty.</exception>
        /// <exception cref="Exception">Thrown for database-related errors during login.</exception>
        public override bool Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Username and password cannot be empty.");
            }

            // Hash the provided password to compare with the stored hash in the database
            string hashedPassword = HashPassword(password);

            // Use 'using' statements for database objects to ensure proper disposal even if errors occur
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // SQL query to check if a user with the given username and hashed password, AND 'Admin' role exists
                    string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash AND Role = 'Admin'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                        // ExecuteScalar retrieves the first column of the first row (the count)
                        int count = (int)command.ExecuteScalar();

                        // If count is 1, a matching admin user was found
                        return count == 1;
                    }
                }
                catch (SqlException ex)
                {
                    // Log the detailed SQL exception for debugging purposes
                    Console.WriteLine($"SQL Error during admin login: {ex.Message}");
                    // Re-throw a more general, user-friendly exception for the UI layer
                    throw new Exception("A database error occurred during login. Please try again later.", ex);
                }
                catch (Exception ex)
                {
                    // Catch any other unexpected exceptions
                    Console.WriteLine($"An unexpected error occurred during admin login: {ex.Message}");
                    throw; // Re-throw to be caught by the calling method (e.g., Form1)
                }
            }
        }

        /// <summary>
        /// Registers a new admin user in the database.
        /// </summary>
        /// <param name="username">The username for the new admin.</param>
        /// <param name="password">The plain text password for the new admin.</param>
        /// <returns>True if registration is successful, false otherwise.</returns>
        /// <exception cref="ArgumentException">Thrown if username or password is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the username already exists.</exception>
        /// <exception cref="Exception">Thrown for database-related errors during registration.</exception>
        public bool RegisterNewAdmin(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Username and password cannot be empty for registration.");
            }

            // Hash the password before storing it
            string hashedPassword = HashPassword(password);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // First, check if the username already exists
                    string checkQuery = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Username", username);
                        if ((int)checkCommand.ExecuteScalar() > 0)
                        {
                            throw new InvalidOperationException("Username already exists. Please choose a different one.");
                        }
                    }

                    // If username is unique, proceed with insertion
                    string insertQuery = "INSERT INTO Users (Username, PasswordHash, Role) VALUES (@Username, @PasswordHash, @Role)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                        command.Parameters.AddWithValue("@Role", "Admin"); // Assign the 'Admin' role

                        int rowsAffected = command.ExecuteNonQuery(); // Execute the insert command
                        return rowsAffected > 0; // Returns true if at least one row was inserted
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error during admin registration: {ex.Message}");
                    throw new Exception("A database error occurred during registration. Please try again later.", ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred during admin registration: {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Changes the password for a specific admin user in the database.
        /// </summary>
        /// <param name="username">The username of the admin whose password is to be changed.</param>
        /// <param name="newPassword">The new plain text password.</param>
        /// <returns>True if the password was successfully changed, false otherwise (e.g., user not found).</returns>
        /// <exception cref="ArgumentException">Thrown if username or newPassword is null or empty.</exception>
        /// <exception cref="Exception">Thrown for database-related errors during password change.</exception>
        public override void ChangePassword(string username, string newPassword) // Modified to accept username
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentException("Username and new password cannot be empty.");
            }

            string newHashedPassword = HashPassword(newPassword);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string updateQuery = "UPDATE Users SET PasswordHash = @NewPasswordHash WHERE Username = @Username AND Role = 'Admin'";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@NewPasswordHash", newHashedPassword);
                        command.Parameters.AddWithValue("@Username", username);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new InvalidOperationException($"Admin user '{username}' not found or no change occurred.");
                        }
                        Console.WriteLine($"Password for '{username}' changed successfully.");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error during password change for '{username}': {ex.Message}");
                    throw new Exception("A database error occurred during password change. Please try again later.", ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred during password change for '{username}': {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Provides a basic logout mechanism. In a WinForms app, this typically involves UI navigation
        /// (e.g., back to login screen) or clearing client-side session data.
        /// This method can be extended to log logout activity in the database if needed.
        /// </summary>
        /// <param name="userID">The ID of the user logging out (optional, depends on tracking needs).</param>
        public override void Logout(int userID) // Changed parameter type to int for consistency with typical DB IDs
        {
            // For a desktop application, logging out might involve:
            // 1. Clearing any stored user session data in memory.
            // 2. Navigating back to the login screen or closing the application.
            // 3. (Optional) Logging the logout event in the database for auditing purposes.

            Console.WriteLine($"User with ID {userID} logged out successfully.");
            // Example of logging logout to DB (assuming you have a 'Logs' table)
            /*
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string logQuery = "INSERT INTO Logs (UserID, EventType, Timestamp) VALUES (@UserID, 'Logout', GETDATE())";
                    using (SqlCommand command = new SqlCommand(logQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error logging logout for User ID {userID}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error logging logout for User ID {userID}: {ex.Message}");
                }
            }
            */
            // The actual UI navigation (e.g., hiding current form and showing login form)
            // will typically be handled in the Form's event handler calling this method.
        }
    }
}