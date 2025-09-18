using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WareHouseApp.Models; // Assuming Employee is in WareHouseApp.Models

namespace WareHouseApp.Managers
{
    public class EmployeeManager : BaseManager<Employee> // Inherit from BaseCrudManager
    {
        /// <summary>
        /// Adds a new employee to the database. (CREATE operation)
        /// </summary>
        /// <param name="employee">The Employee object to add.</param>
        /// <returns>True if the employee was added successfully, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the employee is null or its required fields are empty.</exception>
        /// <exception cref="Exception">Thrown for database-related errors.</exception>
        public override bool AddItem(Employee employee)
        {
            if (employee == null || string.IsNullOrEmpty(employee.FirstName) || string.IsNullOrEmpty(employee.LastName) || string.IsNullOrEmpty(employee.Position))
            {
                throw new ArgumentNullException("Employee object and its first name, last name, and position cannot be null or empty.");
            }

            string query = "INSERT INTO Employees (FirstName, LastName, Position, HireDate, Salary, ContactNumber, Email) VALUES (@FirstName, @LastName, @Position, @HireDate, @Salary, @ContactNumber, @Email)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FirstName", employee.FirstName),
                new SqlParameter("@LastName", employee.LastName),
                new SqlParameter("@Position", employee.Position),
                new SqlParameter("@HireDate", employee.HireDate),
                new SqlParameter("@Salary", employee.Salary),
                new SqlParameter("@ContactNumber", employee.ContactNumber ?? (object)DBNull.Value),
                new SqlParameter("@Email", employee.Email ?? (object)DBNull.Value)
            };

            int rowsAffected = ExecuteNonQuerySafe(query, parameters);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Retrieves all employees from the database. (READ operation - All)
        /// </summary>
        /// <returns>A list of Employee objects.</returns>
        /// <exception cref="Exception">Thrown for database-related errors.</exception>
        public override List<Employee> GetAllItems()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT EmployeeID, FirstName, LastName, Position, HireDate, Salary, ContactNumber, Email FROM Employees";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                employees.Add(new Employee
                                {
                                    EmployeeID = GetValueOrDefault<int>(reader, "EmployeeID"),
                                    FirstName = GetValueOrDefault<string>(reader, "FirstName"),
                                    LastName = GetValueOrDefault<string>(reader, "LastName"),
                                    Position = GetValueOrDefault<string>(reader, "Position"),
                                    HireDate = GetValueOrDefault<DateTime>(reader, "HireDate"),
                                    Salary = GetValueOrDefault<decimal>(reader, "Salary"),
                                    ContactNumber = GetValueOrDefault<string>(reader, "ContactNumber"),
                                    Email = GetValueOrDefault<string>(reader, "Email")
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error retrieving all employees: {ex.Message}");
                    throw new Exception("A database error occurred while retrieving employees. Please check the database schema and connection.", ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred while retrieving all employees: {ex.Message}");
                    throw;
                }
            }
            return employees;
        }

        /// <summary>
        /// Retrieves a single employee by their ID. (READ operation - By ID)
        /// </summary>
        /// <param name="employeeID">The ID of the employee to retrieve.</param>
        /// <returns>The Employee object if found, otherwise null.</returns>
        /// <exception cref="Exception">Thrown for database-related errors.</exception>
        public override Employee GetItemByID(int employeeID)
        {
            Employee employee = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT EmployeeID, FirstName, LastName, Position, HireDate, Salary, ContactNumber, Email FROM Employees WHERE EmployeeID = @EmployeeID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeID", employeeID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                employee = new Employee
                                {
                                    EmployeeID = GetValueOrDefault<int>(reader, "EmployeeID"),
                                    FirstName = GetValueOrDefault<string>(reader, "FirstName"),
                                    LastName = GetValueOrDefault<string>(reader, "LastName"),
                                    Position = GetValueOrDefault<string>(reader, "Position"),
                                    HireDate = GetValueOrDefault<DateTime>(reader, "HireDate"),
                                    Salary = GetValueOrDefault<decimal>(reader, "Salary"),
                                    ContactNumber = GetValueOrDefault<string>(reader, "ContactNumber"),
                                    Email = GetValueOrDefault<string>(reader, "Email")
                                };
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error retrieving employee by ID: {ex.Message}");
                    throw new Exception("A database error occurred while retrieving the employee by ID. Please check the database schema and connection.", ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred while retrieving employee by ID: {ex.Message}");
                    throw;
                }
            }
            return employee;
        }

        /// <summary>
        /// Updates an existing employee in the database. (UPDATE operation)
        /// </summary>
        /// <param name="employee">The Employee object with updated details (EmployeeID must be set).</param>
        /// <returns>True if the employee was updated successfully, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the employee is null or its required fields are empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown if no employee with the given ID is found.</exception>
        /// <exception cref="Exception">Thrown for database-related errors.</exception>
        public override bool UpdateItem(Employee employee)
        {
            if (employee == null || string.IsNullOrEmpty(employee.FirstName) || string.IsNullOrEmpty(employee.LastName) || string.IsNullOrEmpty(employee.Position))
            {
                throw new ArgumentNullException("Employee object and its first name, last name, and position cannot be null or empty for update.");
            }

            string query = "UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Position = @Position, HireDate = @HireDate, Salary = @Salary, ContactNumber = @ContactNumber, Email = @Email WHERE EmployeeID = @EmployeeID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FirstName", employee.FirstName),
                new SqlParameter("@LastName", employee.LastName),
                new SqlParameter("@Position", employee.Position),
                new SqlParameter("@HireDate", employee.HireDate),
                new SqlParameter("@Salary", employee.Salary),
                new SqlParameter("@ContactNumber", employee.ContactNumber ?? (object)DBNull.Value),
                new SqlParameter("@Email", employee.Email ?? (object)DBNull.Value),
                new SqlParameter("@EmployeeID", employee.EmployeeID)
            };

            int rowsAffected = ExecuteNonQuerySafe(query, parameters);
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"No employee found with ID {employee.EmployeeID} to update.");
            }
            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes an employee from the database by their ID. (DELETE operation)
        /// </summary>
        /// <param name="employeeID">The ID of the employee to delete.</param>
        /// <returns>True if the employee was deleted successfully, false otherwise.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no employee with the given ID is found.</exception>
        /// <exception cref="Exception">Thrown for database-related errors.</exception>
        public override bool DeleteItem(int employeeID)
        {
            string query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", employeeID)
            };

            int rowsAffected = ExecuteNonQuerySafe(query, parameters);
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"No employee found with ID {employeeID} to delete.");
            }
            return rowsAffected > 0;
        }
    }
}