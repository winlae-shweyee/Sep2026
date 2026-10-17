using System.Collections.Generic;
using System;

namespace Sep2026.MartInventoryConsoleApp;

public class ProductService
{
    public static List<ProductDto> products = new List<ProductDto>();

    //adding product
    public void AddProduct(string name, string category, decimal price, int quantity)
    {
       string id = GenerateProductId (category);
       
         ProductDto product = new ProductDto
        {
            Id = id ,
            Name = name,
            Category = category,
            Price = price,
            Quantity = quantity
        };

        products.Add(product);

        Console.WriteLine ($"Product added Successfully! ID:{id} ");
    }

    //generating id
    //use private : to calculate the ID from the back by calling the add product. 
    private string GenerateProductId(string category)
    {
        string prefix = category.Substring(0,1).ToUpper();
        int maxNumber = 0;

        foreach (ProductDto product in products)
        {
            //finding the same category
            if (product.Id.StartsWith(prefix))
            {
                //to use the number part by emitting the prefix
                string numberPart = product.Id.Substring (1);

                //try to change the string to int (if can't return false) and if it can change , out the integer,put to the number.
                if (int.TryParse(numberPart, out int number))//
                {
                    if (number > maxNumber)
                    {
                        maxNumber = number;
                    }
                }
            }
        }
        return $"{prefix}{(maxNumber + 1):D3}";
        //D3 : change to the 3 numbers like 001
    }
    

    //view product
    public void ViewAllProducts()
    {
        foreach (ProductDto product in products)
        {
            Console.WriteLine($"ID: {product.Id}");
            Console.WriteLine($"Name: {product.Name}");
            Console.WriteLine($"Category: {product.Category}");
            Console.WriteLine($"Price: {product.Price}");
            Console.WriteLine($"Quantity: {product.Quantity}");
            Console.WriteLine("___________________________________");
        }
}

    //View restock
    public void ViewRestockProducts()
    {
        bool found = false;

        foreach (ProductDto product in products)
        {
            if (product.Quantity < 10)
            {
                Console.WriteLine ($"ID: {product.Id}");
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Quantity : {product.Quantity}");
                Console.WriteLine("Restock recommended!");
                Console.WriteLine("__________________________________");

                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("All products have sufficient stock. No restocking needed!");
        }
    }

    
    //searching the product
    public void SearchProduct(string keyword)
    {
        bool found = false;

        foreach (ProductDto product in products)
        {
            if(product.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) || product.Category.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"ID: {product.Id} ");
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Category: {product.Category}");
                Console.WriteLine($"Price: {product.Price}");
                Console.WriteLine($"Quantity: {product.Quantity}");
                Console.WriteLine ("__________________________");

                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("No matching product found.");
        }
    }

    //updating the product
    public void UpdateProduct(string id)
    {
        foreach (ProductDto product in products)
        {
            if (product.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
            {
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

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter new name: ");
                            product.Name = Console.ReadLine();
                            Console.WriteLine("Name updated successfully!");
                            break;
                        
                        case "2":
                            Console.Write("Enter new price: ");
                            product.Price = Convert.ToDecimal (Console.ReadLine());
                            Console.WriteLine("Price updated successfully!");
                            break;

                        case "3":
                            Console.Write ("Enter new quantity: ");
                            product.Quantity = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Quantity updated successfully!");
                            break;

                        case "4":
                            updating = false;
                            break;

                        default: 
                            Console.WriteLine("Invalid option . Try again.");
                            break;
                    }
                }
                return;
                
            }
        }
        Console.WriteLine("Product not found");
    }

    //Delete Product
    public void DeleteProduct(string id)
    {
        foreach (ProductDto product in products)
        {
            if (product.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                products.Remove(product);
                Console.WriteLine("Product deleted successfully!");
                return;
            }
        }
        Console.WriteLine("Product not found");
    }
}
