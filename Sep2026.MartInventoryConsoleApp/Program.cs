using Sep2026.MartInventoryConsoleApp;

ProductService service = new ProductService();
bool running = true;

while (running)
{   
    Console.WriteLine("\n====================================");
    Console.WriteLine("     Mini Mart Inventory System");
    Console.WriteLine("====================================");
    Console.WriteLine("Choose one of these features to continue");
    Console.WriteLine("1. Add Product");
    Console.WriteLine("2. View All Products");
    Console.WriteLine("3. Search Product");
    Console.WriteLine("4. Update Product");
    Console.WriteLine("5. Delete Product");
    Console.WriteLine("6. View Restock Recommendations");
    Console.WriteLine("7. Exit from the System");
    Console.WriteLine("Enter your choice: ");

    string option = Console.ReadLine();
    
    switch (option)
    {
        case "1":
            Console.Write("Please enter product name: ");
            string name = Console.ReadLine();

            Console.Write("Enter category: ");
            string category = Console.ReadLine();

            Console.Write("Enter product price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());

            Console.Write ("Enter product quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            service.AddProduct (name, category, price, quantity);
            break;
        
        case "2":
            service.ViewAllProducts();
            break;
        
        case "3":
            Console.WriteLine("Search Product selected by product name, and category");
            string keyword = Console.ReadLine()!;
            service.SearchProduct (keyword);
            break;

        case "4":
            Console.WriteLine("\n--- Update Product ---");

            service.ViewAllProducts();

            Console.Write ("Enter the product ID you want to update: ");
            string productId = Console.ReadLine();

            service.UpdateProduct(productId);
            break;

        case "5":
            Console.WriteLine("\n--- Delete selected product ---");
            service.ViewAllProducts();
            Console.Write("Enter the product ID you want to delete: ");
            string deleteId = Console.ReadLine();

            service.DeleteProduct(deleteId);
            break;

        case "6":
            service.ViewRestockProducts();
            break;

        case "7":
            running = false;
            break;

        default:
            Console.WriteLine("Invalid option. Try again.");
            break;

    }
}

Console.WriteLine ("Exiting app....");

