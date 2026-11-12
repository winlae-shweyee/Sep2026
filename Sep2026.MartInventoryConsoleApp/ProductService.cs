// using System.Collections.Generic;
// using System;

// namespace Sep2026.MartInventoryConsoleApp;

// public class ProductService
// {
//     public static List<ProductDto> products = new List<ProductDto>();

//     //adding product
//     public void AddProduct(string name, string category, decimal price, int quantity)
//     {
//        string id = GenerateProductId (category);
       
//          ProductDto product = new ProductDto
//         {
//             Id = id ,
//             Name = name,
//             Category = category,
//             Price = price,
//             Quantity = quantity
//         };

//         products.Add(product);

//         Console.WriteLine ($"Product added Successfully! ID:{id} ");
//     }

//     //generating id
//     //use private : to calculate the ID from the back by calling the add product. 
//     private string GenerateProductId(string category)
//     {
//         string prefix = category.Substring(0,1).ToUpper();
//         int maxNumber = 0;

//         foreach (ProductDto product in products)
//         {
//             //finding the same category
//             if (product.Id.StartsWith(prefix))
//             {
//                 //to use the number part by emitting the prefix
//                 string numberPart = product.Id.Substring (1);

//                 //try to change the string to int (if can't return false) and if it can change , out the integer,put to the number.
//                 if (int.TryParse(numberPart, out int number))//
//                 {
//                     if (number > maxNumber)
//                     {
//                         maxNumber = number;
//                     }
//                 }
//             }
//         }
//         return $"{prefix}{(maxNumber + 1):D3}";
//         //D3 : change to the 3 numbers like 001
//     }
    

//     //view product
//     public void ViewAllProducts()
//     {
//         foreach (ProductDto product in products)
//         {
//             Console.WriteLine($"ID: {product.Id}");
//             Console.WriteLine($"Name: {product.Name}");
//             Console.WriteLine($"Category: {product.Category}");
//             Console.WriteLine($"Price: {product.Price}");
//             Console.WriteLine($"Quantity: {product.Quantity}");
//             Console.WriteLine("___________________________________");
//         }
// }

//     //View restock
//     public void ViewRestockProducts()
//     {
//         bool found = false;

//         foreach (ProductDto product in products)
//         {
//             if (product.Quantity < 10)
//             {
//                 Console.WriteLine ($"ID: {product.Id}");
//                 Console.WriteLine($"Name: {product.Name}");
//                 Console.WriteLine($"Quantity : {product.Quantity}");
//                 Console.WriteLine("Restock recommended!");
//                 Console.WriteLine("__________________________________");

//                 found = true;
//             }
//         }

//         if (!found)
//         {
//             Console.WriteLine("All products have sufficient stock. No restocking needed!");
//         }
//     }

    
//     //searching the product
//     public void SearchProduct(string keyword)
//     {
//         bool found = false;

//         foreach (ProductDto product in products)
//         {
//             if(product.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) || product.Category.Contains(keyword, StringComparison.OrdinalIgnoreCase))
//             {
//                 Console.WriteLine($"ID: {product.Id} ");
//                 Console.WriteLine($"Name: {product.Name}");
//                 Console.WriteLine($"Category: {product.Category}");
//                 Console.WriteLine($"Price: {product.Price}");
//                 Console.WriteLine($"Quantity: {product.Quantity}");
//                 Console.WriteLine ("__________________________");

//                 found = true;
//             }
//         }
//         if (!found)
//         {
//             Console.WriteLine("No matching product found.");
//         }
//     }

//     //updating the product
//     public void UpdateProduct(string id)
//     {
//         foreach (ProductDto product in products)
//         {
//             if (product.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
//             {
//                 bool updating = true;

//                 while (updating)
//                 {
//                     Console.WriteLine("\n--- Choose Information to Update ---");
//                     Console.WriteLine("1. Update Name");
//                     Console.WriteLine("2. Update Price");
//                     Console.WriteLine("3. Update Quantity");
//                     Console.WriteLine("4. Exit Update");
//                     Console.Write("Enter your choice: ");

//                     string choice = Console.ReadLine();

//                     switch (choice)
//                     {
//                         case "1":
//                             Console.Write("Enter new name: ");
//                             product.Name = Console.ReadLine();
//                             Console.WriteLine("Name updated successfully!");
//                             break;
                        
//                         case "2":
//                             Console.Write("Enter new price: ");
//                             product.Price = Convert.ToDecimal (Console.ReadLine());
//                             Console.WriteLine("Price updated successfully!");
//                             break;

//                         case "3":
//                             Console.Write ("Enter new quantity: ");
//                             product.Quantity = Convert.ToInt32(Console.ReadLine());
//                             Console.WriteLine("Quantity updated successfully!");
//                             break;

//                         case "4":
//                             updating = false;
//                             break;

//                         default: 
//                             Console.WriteLine("Invalid option . Try again.");
//                             break;
//                     }
//                 }
//                 return;
                
//             }
//         }
//         Console.WriteLine("Product not found");
//     }

//     //Delete Product
//     public void DeleteProduct(string id)
//     {
//         foreach (ProductDto product in products)
//         {
//             if (product.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
//             {
//                 products.Remove(product);
//                 Console.WriteLine("Product deleted successfully!");
//                 return;
//             }
//         }
//         Console.WriteLine("Product not found");
//     }
// }
using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Sep2026.MartInventoryConsoleApp;

public class ProductService
{
    private readonly string _connectionString = "Server=.;Database=Sep2026;User Id=sa;Password=sasa@123;TrustServerCertificate=True;";

    
    public void AddProduct(string name, string category, decimal price, int quantity)
    {
        string id = GenerateProductId(category);

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        string query = "INSERT INTO Products (Id, Name, Category, Price, Quantity) VALUES (@Id, @Name, @Category, @Price, @Quantity)";
        using var command = new SqlCommand(query, connection);
        
        command.Parameters.AddWithValue("@Id", id);
        command.Parameters.AddWithValue("@Name", name);
        command.Parameters.AddWithValue("@Category", category);
        command.Parameters.AddWithValue("@Price", price);
        command.Parameters.AddWithValue("@Quantity", quantity);

        command.ExecuteNonQuery();
        Console.WriteLine($"Product added Successfully to Database! ID: {id}");
    }

    
    private string GenerateProductId(string category)
    {
        string prefix = category.Substring(0, 1).ToUpper();
        int maxNumber = 0;

        using var connection = new SqlConnection(_connectionString);
        string query = "SELECT Id FROM Products WHERE Category = @Category";
        
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Category", category);

        using var adapter = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        foreach (DataRow row in dt.Rows)
        {
            string prodId = row["Id"].ToString() ?? "";
            if (prodId.StartsWith(prefix))
            {
                string numberPart = prodId.Substring(1);
                if (int.TryParse(numberPart, out int number))
                {
                    if (number > maxNumber)
                    {
                        maxNumber = number;
                    }
                }
            }
        }

        return $"{prefix}{(maxNumber + 1):D3}";
    }

    
    public void ViewAllProducts()
{
    using var connection = new SqlConnection(_connectionString);
    string query = "SELECT Id, Name, Category, Price, Quantity FROM Products";
    
    using var command = new SqlCommand(query, connection);
    using var adapter = new SqlDataAdapter(command);
    DataTable dt = new DataTable();
    adapter.Fill(dt);

    if (dt.Rows.Count == 0)
    {
        Console.WriteLine("No products found in the database.");
        return;
    }

    // Displaying the products in a tabular format
    Console.WriteLine(new string('-', 85));
    Console.WriteLine($"| {"ID",-8} | {"Product Name",-25} | {"Category",-15} | {"Price",-10} | {"Quantity",-8} |");
    Console.WriteLine(new string('-', 85));

// Loop through each row in the DataTable and display the product details
    foreach (DataRow row in dt.Rows)
    {
        string id = row["Id"].ToString() ?? "";
        string name = row["Name"].ToString() ?? "";
        string category = row["Category"].ToString() ?? "";
        decimal price = Convert.ToDecimal(row["Price"]);
        int quantity = Convert.ToInt32(row["Quantity"]);

        Console.WriteLine($"| {id,-8} | {name,-25} | {category,-15} | {price,-10:N2} | {quantity,-8} |");
    }

    Console.WriteLine(new string('-', 85));
}

    
    public void ViewRestockProducts()
    {
        using var connection = new SqlConnection(_connectionString);
        string query = "SELECT Id, Name, Quantity FROM Products WHERE Quantity < 10";
        
        using var command = new SqlCommand(query, connection);
        using var adapter = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        if (dt.Rows.Count == 0)
        {
            Console.WriteLine("All products have sufficient stock. No restocking needed!");
            return;
        }

        foreach (DataRow row in dt.Rows)
        {
            Console.WriteLine($"ID: {row["Id"]}");
            Console.WriteLine($"Name: {row["Name"]}");
            Console.WriteLine($"Quantity: {row["Quantity"]}");
            Console.WriteLine("Restock recommended!");
            Console.WriteLine("__________________________________");
        }
    }

    
    public void SearchProduct(string keyword)
    {
        using var connection = new SqlConnection(_connectionString);
        string query = "SELECT Id, Name, Category, Price, Quantity FROM Products WHERE Name LIKE @Keyword OR Category LIKE @Keyword";
        
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Keyword", $"%{keyword}%");

        using var adapter = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        if (dt.Rows.Count == 0)
        {
            Console.WriteLine("No matching product found.");
            return;
        }

        foreach (DataRow row in dt.Rows)
        {
            Console.WriteLine($"ID: {row["Id"]}");
            Console.WriteLine($"Name: {row["Name"]}");
            Console.WriteLine($"Category: {row["Category"]}");
            Console.WriteLine($"Price: {row["Price"]}");
            Console.WriteLine($"Quantity: {row["Quantity"]}");
            Console.WriteLine("__________________________");
        }
    }

    
    public void UpdateProduct(string id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        string checkQuery = "SELECT COUNT(1) FROM Products WHERE Id = @Id";
        using var checkCmd = new SqlCommand(checkQuery, connection);
        checkCmd.Parameters.AddWithValue("@Id", id);
        int exists = (int)(checkCmd.ExecuteScalar() ?? 0);

        if (exists == 0)
        {
            Console.WriteLine("Product not found");
            return;
        }

        bool updating = true;
        while (updating)
        {
            Console.WriteLine("\n--- Choose Information to Update ---");
            Console.WriteLine("1. Update Name");
            Console.WriteLine("2. Update Price");
            Console.WriteLine("3. Update Quantity");
            Console.WriteLine("4. Exit Update");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();
            string updateQuery = "";

            switch (choice)
            {
                case "1":
                    Console.Write("Enter new name: ");
                    string newName = Console.ReadLine()!;
                    updateQuery = "UPDATE Products SET Name = @Value WHERE Id = @Id";
                    ExecuteUpdate(connection, updateQuery, newName, id);
                    break;
                
                case "2":
                    Console.Write("Enter new price: ");
                    decimal newPrice = Convert.ToDecimal(Console.ReadLine());
                    updateQuery = "UPDATE Products SET Price = @Value WHERE Id = @Id";
                    ExecuteUpdate(connection, updateQuery, newPrice, id);
                    break;

                case "3":
                    Console.Write("Enter new quantity: ");
                    int newQty = Convert.ToInt32(Console.ReadLine());
                    updateQuery = "UPDATE Products SET Quantity = @Value WHERE Id = @Id";
                    ExecuteUpdate(connection, updateQuery, newQty, id);
                    break;

                case "4":
                    updating = false;
                    break;

                default: 
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    private void ExecuteUpdate(SqlConnection connection, string query, object value, string id)
    {
        using var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Value", value);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Product updated successfully in Database!");
    }

    
    public void DeleteProduct(string id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        string query = "DELETE FROM Products WHERE Id = @Id";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        int rowsAffected = command.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
            Console.WriteLine("Product deleted successfully from Database!");
        }
        else
        {
            Console.WriteLine("Product not found");
        }
    }
}