using Sep2026.MartInventoryConsoleApp3.Database.AppDbContextModels;

AppDbContext db = new AppDbContext();

while (true)
{
    Console.WriteLine("\n=== Mart Inventory System (EF Core) ===");
    Console.WriteLine("1. View All Products");
    Console.WriteLine("2. Add Product");
    Console.WriteLine("3. Update Product Quantity");
    Console.WriteLine("4. Delete Product");
    Console.WriteLine("5. View All Categories");
    Console.WriteLine("6. Add Category");
    Console.WriteLine("0. Exit");
    Console.Write("Select an option: ");

    string choice = Console.ReadLine() ?? "";

    if (choice == "1")
    {
        var lst = db.Products.ToList();
        Console.WriteLine("\n--- Product List ---");
        foreach (var item in lst)
            Console.WriteLine($"Id: {item.Id} | Name: {item.Name} | Category: {item.Category} | Price: {item.Price} | Qty: {item.Quantity}");
    }
    else if (choice == "2")
    {
        Product item = new Product();
        Console.Write("Id (e.g., P001): ");
        item.Id = Console.ReadLine() ?? "";
        Console.Write("Name: ");
        item.Name = Console.ReadLine() ?? "";
        Console.Write("Category: ");
        item.Category = Console.ReadLine() ?? "";
        Console.Write("Price: ");
        item.Price = Convert.ToDecimal(Console.ReadLine());
        Console.Write("Quantity: ");
        item.Quantity = Convert.ToInt32(Console.ReadLine());
        db.Products.Add(item);
        db.SaveChanges();
        Console.WriteLine("Product added successfully!");
    }
    else if (choice == "3")
    {
        Console.Write("Enter Product Id to update: ");
        string id = Console.ReadLine() ?? "";
        var item = db.Products.FirstOrDefault(x => x.Id == id);
        if (item != null)
        {
            Console.Write("New Quantity: ");
            item.Quantity = Convert.ToInt32(Console.ReadLine());
            db.SaveChanges();
            Console.WriteLine("Product updated successfully!");
        }
        else Console.WriteLine("Product not found.");
    }
    else if (choice == "4")
    {
        Console.Write("Enter Product Id to delete: ");
        string id = Console.ReadLine() ?? "";
        var item = db.Products.FirstOrDefault(x => x.Id == id);
        if (item != null)
        {
            db.Products.Remove(item);
            db.SaveChanges();
            Console.WriteLine("Product deleted successfully!");
        }
        else Console.WriteLine("Product not found.");
    }
    else if (choice == "5")
    {
        var lst = db.Categories.ToList();
        Console.WriteLine("\n--- Category List ---");
        foreach (var item in lst)
            Console.WriteLine($"Id: {item.Id} | Name: {item.Name} | Description: {item.Description}");
    }
    else if (choice == "6")
    {
        Category item = new Category();
        Console.Write("Category Name: ");
        item.Name = Console.ReadLine() ?? "";
        Console.Write("Description: ");
        item.Description = Console.ReadLine() ?? "";
        db.Categories.Add(item);
        db.SaveChanges();
        Console.WriteLine("Category added successfully!");
    }
    else if (choice == "0")
    {
        break;
    }
    else
    {
        Console.WriteLine("Invalid option. Please try again.");
    }
}
