using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WareHouseApp.Models; // Assuming Customer is in WareHouseApp.Models

namespace WareHouseApp.Managers
{
    public class CustomerManager : BaseManager<Customer> // Inherit from BaseCrudManager
    {
        /// <summary>
        /// Adds a new customer to the database. (CREATE operation)
        /// </summary>
        /// <param name="customer">The Customer object to add.</param>
        /// <returns>True if the customer was added successfully, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the customer is null or its required fields are empty.</exception>
        /// <exception cref="Exception">Thrown for database-related errors.</exception>
        public override bool AddItem(Customer customer)
        {
            if (customer == null || string.IsNullOrEmpty(customer.FirstName) || string.IsNullOrEmpty(customer.LastName))
            {
                throw new ArgumentNullException("Customer object and its first/last name cannot be null or empty.");
            }

            string query = "INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) VALUES (@FirstName, @LastName, @Email, @Phone, @Address)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FirstName", customer.FirstName),
                new SqlParameter("@LastName", customer.LastName),
                new SqlParameter("@Email", customer.Email ?? (object)DBNull.Value),
                new SqlParameter("@Phone", customer.Phone ?? (object)DBNull.Value),
                new SqlParameter("@Address", customer.Address ?? (object)DBNull.Value)
            };

            int rowsAffected = ExecuteNonQuerySafe(query, parameters);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Retrieves all customers from the database. (READ operation - All)
        /// </summary>
        /// <returns>A list of Customer objects.</returns>
        /// <exception cref="Exception">Thrown for database-related errors.</exception>
        public override List<Customer> GetAllItems()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT CustomerID, FirstName, LastName, Email, Phone, Address FROM Customers";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customers.Add(new Customer
                                {
                                    CustomerID = GetValueOrDefault<int>(reader, "CustomerID"),
                                    FirstName = GetValueOrDefault<string>(reader, "FirstName"),
                                    LastName = GetValueOrDefault<string>(reader, "LastName"),
                                    Email = GetValueOrDefault<string>(reader, "Email"),
                                    Phone = GetValueOrDefault<string>(reader, "Phone"),
                                    Address = GetValueOrDefault<string>(reader, "Address")
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error retrieving all customers: {ex.Message}");
                    throw new Exception("A database error occurred while retrieving customers. Please check the database schema and connection.", ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred while retrieving all customers: {ex.Message}");
                    throw;
                }
            }
            return customers;
        }

        /// <summary>
        /// Retrieves a single customer by their ID. (READ operation - By ID)
        /// </summary>
        /// <param name="customerID">The ID of the customer to retrieve.</param>
        /// <returns>The Customer object if found, otherwise null.</returns>
        /// <exception cref="Exception">Thrown for database-related errors.</exception>
        public override Customer GetItemByID(int customerID)
        {
            Customer customer = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT CustomerID, FirstName, LastName, Email, Phone, Address FROM Customers WHERE CustomerID = @CustomerID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customer = new Customer
                                {
                                    CustomerID = GetValueOrDefault<int>(reader, "CustomerID"),
                                    FirstName = GetValueOrDefault<string>(reader, "FirstName"),
                                    LastName = GetValueOrDefault<string>(reader, "LastName"),
                                    Email = GetValueOrDefault<string>(reader, "Email"),
                                    Phone = GetValueOrDefault<string>(reader, "Phone"),
                                    Address = GetValueOrDefault<string>(reader, "Address")
                                };
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error retrieving customer by ID: {ex.Message}");
                    throw new Exception("A database error occurred while retrieving the customer by ID. Please check the database schema and connection.", ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred while retrieving customer by ID: {ex.Message}");
                    throw;
                }
            }
            return customer;
        }

        /// <summary>
        /// Updates an existing customer in the database. (UPDATE operation)
        /// </summary>
        /// <param name="customer">The Customer object with updated details (CustomerID must be set).</param>
        /// <returns>True if the customer was updated successfully, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the customer is null or its required fields are empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown if no customer with the given ID is found.</exception>
        /// <exception cref="Exception">Thrown for database-related errors.</exception>
        public override bool UpdateItem(Customer customer)
        {
            if (customer == null || string.IsNullOrEmpty(customer.FirstName) || string.IsNullOrEmpty(customer.LastName))
            {
                throw new ArgumentNullException("Customer object and its first/last name cannot be null or empty for update.");
            }

            string query = "UPDATE Customers SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, Address = @Address WHERE CustomerID = @CustomerID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FirstName", customer.FirstName),
                new SqlParameter("@LastName", customer.LastName),
                new SqlParameter("@Email", customer.Email ?? (object)DBNull.Value),
                new SqlParameter("@Phone", customer.Phone ?? (object)DBNull.Value),
                new SqlParameter("@Address", customer.Address ?? (object)DBNull.Value),
                new SqlParameter("@CustomerID", customer.CustomerID)
            };

            int rowsAffected = ExecuteNonQuerySafe(query, parameters);
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"No customer found with ID {customer.CustomerID} to update.");
            }
            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes a customer from the database by their ID. (DELETE operation)
        /// </summary>
        /// <param name="customerID">The ID of the customer to delete.</param>
        /// <returns>True if the customer was deleted successfully, false otherwise.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no customer with the given ID is found.</exception>
        /// <exception cref="Exception">Thrown for database-related errors.</exception>
        public override bool DeleteItem(int customerID)
        {
            string query = "DELETE FROM Customers WHERE CustomerID = @CustomerID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CustomerID", customerID)
            };

            int rowsAffected = ExecuteNonQuerySafe(query, parameters);
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"No customer found with ID {customerID} to delete.");
            }
            return rowsAffected > 0;
        }
    }
}