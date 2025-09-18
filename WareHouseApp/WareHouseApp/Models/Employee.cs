using System;

namespace WareHouseApp.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        // Constructor for creating new employees
        public Employee(string firstName, string lastName, string position, DateTime hireDate, decimal salary, string contactNumber, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            HireDate = hireDate;
            Salary = salary;
            ContactNumber = contactNumber;
            Email = email;
        }

        // Constructor for loading existing employees from DB (with ID)
        public Employee(int employeeId, string firstName, string lastName, string position, DateTime hireDate, decimal salary, string contactNumber, string email)
        {
            EmployeeID = employeeId;
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            HireDate = hireDate;
            Salary = salary;
            ContactNumber = contactNumber;
            Email = email;
        }

        // Default constructor for cases where it's needed (e.g., deserialization)
        public Employee() { }
    }
}