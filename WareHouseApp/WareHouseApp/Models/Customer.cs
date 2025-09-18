using System;

namespace WareHouseApp.Models
{
    public class Customer
    {
        public int CustomerID { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        // Constructor for creating new customers
        public Customer(string firstName, string lastName, string email, string phone, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
        }

        // Constructor for loading existing customers from DB (with ID)
        public Customer(int customerId, string firstName, string lastName, string email, string phone, string address)
        {
            CustomerID = customerId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
        }

        // Default constructor for cases where it's needed (e.g., deserialization)
        public Customer() { }
    }
}