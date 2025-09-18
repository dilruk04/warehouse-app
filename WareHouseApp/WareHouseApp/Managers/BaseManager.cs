using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions; // For generic method to get value from reader safely

namespace WareHouseApp.Managers
{
    // T represents the entity type (e.g., InventoryItem, Customer, Employee)
    public abstract class BaseManager<T> where T : class, new() // 'new()' constraint ensures T has a parameterless constructor
    {
        // Protected connection string accessible by derived classes
        // IMPORTANT: Verify and update this connection string for your WarehouseDB.mdf
        protected string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\WarehouseDB.mdf;Integrated Security=True;Connect Timeout=30;";

        // Abstract methods for CRUD operations (must be implemented by derived classes)
        public abstract bool AddItem(T item);
        public abstract List<T> GetAllItems();
        public abstract T GetItemByID(int id);
        public abstract bool UpdateItem(T item);
        public abstract bool DeleteItem(int id);

        // Protected helper method for executing non-query commands (INSERT, UPDATE, DELETE)
        // This centralizes common try-catch blocks for database operations
        protected int ExecuteNonQuerySafe(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        return command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error: {ex.Message}");
                    throw new Exception("A database error occurred. Please check the database schema and connection.", ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    throw;
                }
            }
        }

        // Protected helper method to safely read a value from SqlDataReader, handling DBNull
        protected TValue GetValueOrDefault<TValue>(SqlDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);
            if (reader.IsDBNull(ordinal))
            {
                return default(TValue); // Returns null for reference types, 0 for int, etc.
            }

            // Handle specific type conversions if necessary beyond direct cast
            if (typeof(TValue) == typeof(string))
            {
                return (TValue)(object)reader.GetString(ordinal);
            }
            else if (typeof(TValue) == typeof(int))
            {
                return (TValue)(object)reader.GetInt32(ordinal);
            }
            else if (typeof(TValue) == typeof(decimal))
            {
                return (TValue)(object)reader.GetDecimal(ordinal);
            }
            else if (typeof(TValue) == typeof(DateTime))
            {
                return (TValue)(object)reader.GetDateTime(ordinal);
            }
            // Add more specific types as needed, or use a generic approach like:
            return (TValue)reader.GetValue(ordinal);
        }
    }
}