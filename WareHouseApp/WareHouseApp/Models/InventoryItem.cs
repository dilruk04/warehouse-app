using System;

namespace WareHouseApp.Models
{
    public class InventoryItem
    {
        public int ItemID { get; set; } // Primary Key
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Constructor to easily create new items
        public InventoryItem(string name, string description, int quantity, decimal price)
        {
            Name = name;
            Description = description;
            Quantity = quantity;
            Price = price;
        }

        // Constructor for loading existing items from DB (with ID)
        public InventoryItem(int itemId, string name, string description, int quantity, decimal price)
        {
            ItemID = itemId;
            Name = name;
            Description = description;
            Quantity = quantity;
            Price = price;
        }

        // Default constructor for cases where it's needed (e.g., deserialization)
        public InventoryItem() { }
    }
}