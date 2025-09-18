using System;
using System.Data.SqlClient; // Needed for SqlConnection and SqlException
using System.Windows.Forms;
using System.IO; // Needed for Path and AppDomain
using System.Linq; // <--- ADD THIS LINE FOR FirstOrDefault

namespace WareHouseApp
{
    static class Program
    {
        // IMPORTANT: This connection string MUST be identical to the one in BaseCrudManager.cs
        // Make sure the database file 'WarehouseDB.mdf' is in your project's root and set to
        // "Copy to Output Directory: Copy if newer" or "Copy always" in its properties.
        private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\WarehouseDB.mdf;Integrated Security=True;Connect Timeout=30;";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // --- Database Connection Test ---
            // This will attempt to connect to the database before launching the main form.
            // If it fails, it will show an error message and exit the application.
            if (!TestDatabaseConnection())
            {
                // Application will exit if the database connection fails.
                return;
            }
            // --- End Database Connection Test ---

            // IMPORTANT: If your application's main form is actually DashBoard, change Form1() to DashBoard()
            Application.Run(new Form1());
        }

        /// <summary>
        /// Tests the database connection and provides specific error messages.
        /// </summary>
        /// <returns>True if connection is successful, false otherwise.</returns>
        private static bool TestDatabaseConnection()
        {
            try
            {
                // Resolve |DataDirectory| to its actual path for better debugging and file existence check
                string dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
                if (string.IsNullOrEmpty(dataDirectory))
                {
                    // Fallback: If DataDirectory is not explicitly set, assume it's the base directory
                    // (This might happen outside of Visual Studio's debugger)
                    dataDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory); // Set it for current domain
                }

                // Extract the AttachDbFilename part from the connection string
                // Example: "AttachDbFilename=|DataDirectory|\WarehouseDB.mdf" -> "|DataDirectory|\WarehouseDB.mdf"
                string attachDbFilenamePart = connectionString.Split(';').FirstOrDefault(s => s.Trim().StartsWith("AttachDbFilename", StringComparison.OrdinalIgnoreCase));
                string dbRelativePath = attachDbFilenamePart?.Split('=')[1].Trim();

                string dbFilePath = null;
                if (!string.IsNullOrEmpty(dbRelativePath))
                {
                    dbFilePath = dbRelativePath.Replace("|DataDirectory|", dataDirectory);
                }

                if (!string.IsNullOrEmpty(dbFilePath) && !File.Exists(dbFilePath))
                {
                    MessageBox.Show($"Database file 'WarehouseDB.mdf' not found at expected path:\n{dbFilePath}\n\nPlease ensure the '.mdf' file is in your application's output directory (e.g., bin\\Debug) and its 'Copy to Output Directory' property is set correctly in Visual Studio.", "Database File Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Attempt to open the connection
                    // If successful, connection.Close() is automatically called when exiting the 'using' block.
                    MessageBox.Show("Database connection test successful!", "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return true;
            }
            catch (SqlException ex)
            {
                // Specific error for SQL connection issues
                MessageBox.Show($"SQL Database Connection Error: {ex.Message}\n\nPlease check your connection string, ensure SQL Server LocalDB is running, and the database file is not corrupted or locked.", "Database Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"SQL Connection Error details: {ex.ToString()}"); // Log full exception for debugging
                return false;
            }
            catch (Exception ex)
            {
                // General error for other issues during the test
                MessageBox.Show($"An unexpected error occurred during database connection test: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"General Connection Error details: {ex.ToString()}"); // Log full exception for debugging
                return false;
            }
        }
    }
}