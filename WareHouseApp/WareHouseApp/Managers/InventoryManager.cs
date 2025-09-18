using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WareHouseApp.Models; // Assuming InventoryItem is in WareHouseApp.Models

namespace WareHouseApp.Managers
{
    public class InventoryManager : BaseManager<InventoryItem> // Inherit from BaseCrudManager
    {
        /// <summary>
        /// Adds a new inventory item to the database. (CREATE operation)
        /// </summary>
        /// <param name="item">The InventoryItem object to add.</param>
        /// <returns>True if the item was added successfully, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the item is null or its name is empty.</exception>
        /// <exception cref="Exception">Thrown for database-related errors.</exception>
        public override bool AddItem(InventoryItem item)
        {
            if (item == null || string.IsNullOrEmpty(item.Name))
            {
                throw new ArgumentNullException("Inventory item and its name cannot be null or empty.");
            }

            string query = "INSERT INTO Inventory (Name, Description, Quantity, Price) VALUES (@Name, @Description, @Quantity, @Price)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@Description", item.Description ?? (object)DBNull.Value),
                new SqlParameter("@Quantity", item.Quantity),
                new SqlParameter("@Price", item.Price)
            };

            int rowsAffected = ExecuteNonQuerySafe(query, parameters);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Retrieves all inventory items from the database. (READ operation - All)
        /// </summary>
        /// <returns>A list of InventoryItem objects.</returns>
        /// <exception cref="Exception">Thrown for database-related errors.</exception>
        public override List<InventoryItem> GetAllItems()
        {
            List<InventoryItem> items = new List<InventoryItem>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ItemID, Name, Description, Quantity, Price FROM Inventory";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new InventoryItem
                                {
                                    ItemID = GetValueOrDefault<int>(reader, "ItemID"),
                                    Name = GetValueOrDefault<string>(reader, "Name"),
                                    Description = GetValueOrDefault<string>(reader, "Description"),
                                    Quantity = GetValueOrDefault<int>(reader, "Quantity"),
                                    Price = GetValueOrDefault<decimal>(reader, "Price")
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error retrieving all items: {ex.Message}");
                    throw new Exception("A database error occurred while retrieving items. Please check the database schema and connection.", ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred while retrieving all items: {ex.Message}");
                    throw;
                }
            }
            return items;
        }

        /// <summary>
        /// Retrieves a single inventory item by its ID. (READ operation - By ID)
        /// </summary>
        /// <param name="itemID">The ID of the item to retrieve.</param>
        /// <returns>The InventoryItem object if found, otherwise null.</returns>
        /// <exception cref="Exception">Thrown for database-related errors.</exception>
        public override InventoryItem GetItemByID(int itemID)
        {
            InventoryItem item = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ItemID, Name, Description, Quantity, Price FROM Inventory WHERE ItemID = @ItemID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ItemID", itemID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                item = new InventoryItem
                                {
                                    ItemID = GetValueOrDefault<int>(reader, "ItemID"),
                                    Name = GetValueOrDefault<string>(reader, "Name"),
                                    Description = GetValueOrDefault<string>(reader, "Description"),
                                    Quantity = GetValueOrDefault<int>(reader, "Quantity"),
                                    Price = GetValueOrDefault<decimal>(reader, "Price")
                                };
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error retrieving item by ID: {ex.Message}");
                    throw new Exception("A database error occurred while retrieving the item by ID. Please check the database schema and connection.", ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred while retrieving item by ID: {ex.Message}");
                    throw;
                }
            }
            return item;
        }

        /// <summary>
        /// Updates an existing inventory item in the database. (UPDATE operation)
        /// </summary>
        /// <param name="item">The InventoryItem object with updated details (ItemID must be set).</param>
        /// <returns>True if the item was updated successfully, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the item is null or its name is empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown if no item with the given ID is found.</exception>
        /// <exception cref="Exception">Thrown for database-related errors.</exception>
        public override bool UpdateItem(InventoryItem item)
        {
            if (item == null || string.IsNullOrEmpty(item.Name))
            {
                throw new ArgumentNullException("Inventory item and its name cannot be null or empty for update.");
            }

            string query = "UPDATE Inventory SET Name = @Name, Description = @Description, Quantity = @Quantity, Price = @Price WHERE ItemID = @ItemID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@Description", item.Description ?? (object)DBNull.Value),
                new SqlParameter("@Quantity", item.Quantity),
                new SqlParameter("@Price", item.Price),
                new SqlParameter("@ItemID", item.ItemID)
            };

            int rowsAffected = ExecuteNonQuerySafe(query, parameters);
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"No inventory item found with ID {item.ItemID} to update.");
            }
            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes an inventory item from the database by its ID. (DELETE operation)
        /// </summary>
        /// <param name="itemID">The ID of the item to delete.</param>
        /// <returns>True if the item was deleted successfully, false otherwise.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no item with the given ID is found.</exception>
        /// <exception cref="Exception">Thrown for database-related errors.</exception>
        public override bool DeleteItem(int itemID)
        {
            string query = "DELETE FROM Inventory WHERE ItemID = @ItemID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ItemID", itemID)
            };

            int rowsAffected = ExecuteNonQuerySafe(query, parameters);
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"No inventory item found with ID {itemID} to delete.");
            }
            return rowsAffected > 0;
        }
    }
}