using System;
using Microsoft.Data.SqlClient;
using Sep2026.MartInventoryConsoleApp2;

string connectionString = new SqlConnectionStringBuilder
{
    DataSource = ".",
    InitialCatalog = "Sep2026",
    UserID = "sa",
    Password = "sasa@123",
    TrustServerCertificate = true
}.ConnectionString;





var productService = new ProductService(connectionString);
var categoryService = new CategoryService(connectionString);


while (true)
{
    Console.WriteLine("\n=== Mart Inventory System (Dapper) ===");
    Console.WriteLine("1. View Products");
    Console.WriteLine("2. Add Product");
    Console.WriteLine("3. Update Product Stock");
    Console.WriteLine("4. Search Product");
    Console.WriteLine("5. View Categories");
    Console.WriteLine("6. Add Category");
    Console.WriteLine("0. Exit");
    Console.Write("Select an option: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            foreach (var p in productService.GetAllProducts())
                Console.WriteLine($"[{p.Id}] {p.Name} | Category: {p.Category} | Price: {p.Price} | Qty: {p.Quantity}");
            break;

        case "2":
            Console.Write("Name: ");
            var name = Console.ReadLine() ?? "";
            Console.Write("Category: ");
            var category = Console.ReadLine() ?? "";
            Console.Write("Price: ");
            var price = decimal.TryParse(Console.ReadLine(), out var pr) ? pr : 0m;
            Console.Write("Quantity: ");
            var qty = int.TryParse(Console.ReadLine(), out var q) ? q : 0;

            productService.AddProduct(new ProductDto
            {
                Name = name,
                Category = category,
                Price = price,
                Quantity = qty
            });
            break;

        case "3":
            Console.Write("Product Id: ");
            var upId = Console.ReadLine() ?? "";
            Console.Write("New Quantity: ");
            if (int.TryParse(Console.ReadLine(), out var newQty))
            {
                productService.UpdateStock(upId, newQty);
            }
            break;

        case "4":
            Console.Write("Search keyword (Name/Category): ");
            var keyword = Console.ReadLine() ?? "";
            foreach (var p in productService.SearchProducts(keyword))
                Console.WriteLine($"[{p.Id}] {p.Name} | Category: {p.Category} | Price: {p.Price} | Qty: {p.Quantity}");
            break;

        case "5":
            foreach (var c in categoryService.GetAllCategories())
                Console.WriteLine($"[{c.Id}] {c.Name} - {c.Description}");
            break;

        case "6":
            Console.Write("Category Name: ");
            var catName = Console.ReadLine() ?? "";
            Console.Write("Description: ");
            var catDesc = Console.ReadLine() ?? "";
            categoryService.AddCategory(new CategoryDto { Name = catName, Description = catDesc });
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}