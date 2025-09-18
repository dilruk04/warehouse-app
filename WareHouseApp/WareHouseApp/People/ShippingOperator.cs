using System;
using System.Data.SqlClient; // Required for SQL Server interaction
using System.Security.Cryptography; // Required for password hashing (SHA256)
using System.Text; // Required for Encoding

namespace WareHouseApp.People
{
    public class ShippingOperator : Person // Assuming Person is your base class
    {
        // IMPORTANT: Verify and update this connection string.
        // It should be the same as or accessible from your Admin class.
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\WarehouseDB.mdf;Integrated Security=True;Connect Timeout=30;";

        /// <summary>
        /// Hashes a plain text password using SHA256.
        /// (This method can ideally be moved to the 'Person' class or a shared utility class
        /// if multiple user types need it.)
        /// </summary>
        /// <param name="password">The plain text password to hash.</param>
        /// <returns>The SHA256 hash of the password as a hexadecimal string.</returns>
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Authenticates a ShippingOperator user against the database.
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

            string hashedPassword = HashPassword(password);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Query to check for a user with the given username, hashed password, AND 'ShippingOperator' role
                    string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash AND Role = 'ShippingOperator'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                        int count = (int)command.ExecuteScalar();
                        return count == 1; // True if a matching ShippingOperator user is found
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error during ShippingOperator login: {ex.Message}");
                    throw new Exception("A database error occurred during login. Please try again later.", ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred during ShippingOperator login: {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Changes the password for a specific ShippingOperator user in the database.
        /// The method signature now matches 'Person.ChangePassword(string username, string newPassword)'.
        /// </summary>
        /// <param name="username">The username of the ShippingOperator whose password is to be changed.</param>
        /// <param name="newPassword">The new plain text password.</param>
        /// <exception cref="ArgumentException">Thrown if username or newPassword is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the user is not found or not a ShippingOperator.</exception>
        /// <exception cref="Exception">Thrown for database-related errors during password change.</exception>
        public override void ChangePassword(string username, string newPassword)
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
                    // Update the password hash for the specified username and 'ShippingOperator' role
                    string updateQuery = "UPDATE Users SET PasswordHash = @NewPasswordHash WHERE Username = @Username AND Role = 'ShippingOperator'";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@NewPasswordHash", newHashedPassword);
                        command.Parameters.AddWithValue("@Username", username);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new InvalidOperationException($"Shipping Operator user '{username}' not found or no change occurred. Ensure the username and role are correct.");
                        }
                        Console.WriteLine($"Password for Shipping Operator '{username}' changed successfully.");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error during ShippingOperator password change for '{username}': {ex.Message}");
                    throw new Exception("A database error occurred during password change. Please try again later.", ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred during ShippingOperator password change for '{username}': {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Provides a basic logout mechanism for a ShippingOperator.
        /// </summary>
        /// <param name="userID">The ID of the user logging out.</param>
        public override void Logout(int userID)
        {
            Console.WriteLine($"Shipping Operator with ID {userID} logged out successfully.");
            // Add logging to database here if needed, similar to the Admin.Logout example.
        }

        /// <summary>
        /// Simulates loading stocks (currently returns 0).
        /// This method would interact with your Inventory/Stock database.
        /// </summary>
        /// <returns>The number of stocks loaded.</returns>
        public int LoadStocks()
        {
            int stocks = 0;
            Console.WriteLine("Loading stocks operation initiated.");
            // TODO: Implement actual database interaction to load stocks
            return stocks;
        }

        /// <summary>
        /// Simulates shipping stocks (currently returns 0).
        /// This method would interact with your Inventory/Stock and Order database.
        /// </summary>
        /// <returns>The number of stocks shipped.</returns>
        public int ShipStocks()
        {
            int stocks = 0;
            Console.WriteLine("Shipping stocks operation initiated.");
            // TODO: Implement actual database interaction to ship stocks
            return stocks;
        }
    }
}