// using Sep2026.MartInventoryConsoleApp;

// ProductService service = new ProductService();
// var categoryService = new CategoryService();
// bool running = true;

// while (running)
// {   
//     Console.WriteLine("\n====================================");
//     Console.WriteLine("     Mini Mart Inventory System");
//     Console.WriteLine("====================================");
//     Console.WriteLine("Choose one of these features to continue");
//     Console.WriteLine("1. Add Product");
//     Console.WriteLine("2. View All Products");
//     Console.WriteLine("3. Search Product");
//     Console.WriteLine("4. Update Product");
//     Console.WriteLine("5. Delete Product");
//     Console.WriteLine("6. View Restock Recommendations");
//     Console.WriteLine("7. Manage Categories");
//     Console.WriteLine("8. Exit from the System");
//     Console.WriteLine("Enter your choice: ");

//     string option = Console.ReadLine();

//     switch (option)
//     {
//         case "1":
//             Console.Write("Please enter product name: ");
//             string name = Console.ReadLine();

//             Console.Write("Enter category: ");
//             string category = Console.ReadLine();

//             Console.Write("Enter product price: ");
//             decimal price = Convert.ToDecimal(Console.ReadLine());

//             Console.Write ("Enter product quantity: ");
//             int quantity = Convert.ToInt32(Console.ReadLine());

//             service.AddProduct (name, category, price, quantity);
//             break;

//         case "2":
//             service.ViewAllProducts();
//             break;

//         case "3":
//             Console.WriteLine("Search Product selected by product name, and category");
//             string keyword = Console.ReadLine()!;
//             service.SearchProduct (keyword);
//             break;

//         case "4":
//             Console.WriteLine("\n--- Update Product ---");

//             service.ViewAllProducts();

//             Console.Write ("Enter the product ID you want to update: ");
//             string productId = Console.ReadLine();

//             service.UpdateProduct(productId);
//             break;

//         case "5":
//             Console.WriteLine("\n--- Delete selected product ---");
//             service.ViewAllProducts();
//             Console.Write("Enter the product ID you want to delete: ");
//             string deleteId = Console.ReadLine();

//             service.DeleteProduct(deleteId);
//             break;

//         case "6":
//             service.ViewRestockProducts();
//             break;

//         case "7":
//             bool managingCategories = true;
//             while (managingCategories)
//                         {
//                             Console.WriteLine("\n--- Manage Categories ---");
//                             Console.WriteLine("1. View Categories");
//                             Console.WriteLine("2. Add Category");
//                             Console.WriteLine("3. Update Category");
//                             Console.WriteLine("4. Delete Category");
//                             Console.WriteLine("5. Back to Main Menu");
//                             Console.Write("ရွေးချယ်ပါ (1-5): ");

//                             string catChoice = Console.ReadLine();
//                             switch (catChoice)
//                             {
//                                 case "1":
//                                     var categories = categoryService.GetAllCategories();
//                                     Console.WriteLine("\n--- Category List ---");
//                                     foreach (var c in categories)
//                                     {
//                                         Console.WriteLine($"ID: {c.Id}, Name: {c.Name}, Description: {c.Description}");
//                                     }
//                                     break;

//                                 case "2":
//                                     var newCategory = new CategoryDto();
//                                     Console.Write("Enter Category ID: ");
//                                     newCategory.Id = int.Parse(Console.ReadLine() ?? "0");
//                                     Console.Write("Enter Category Name: ");
//                                     newCategory.Name = Console.ReadLine() ?? "";
//                                     Console.Write("Enter Description: ");
//                                     newCategory.Description = Console.ReadLine() ?? "";

//                                     categoryService.AddCategory(newCategory);
//                                     break;

//                                 case "3":
//                                     Console.Write("Enter Category ID to update: ");
//                                     int catId = int.Parse(Console.ReadLine() ?? "0");
//                                     Console.Write("Enter new Category Name: ");
//                                     string newName = Console.ReadLine() ?? "";
//                                     Console.Write("Enter new Description: ");
//                                     string newDesc = Console.ReadLine() ?? "";
//                                     categoryService.UpdateCategory(catId, newName, newDesc);
//                                     break;

//                                 case "4":
//                                     Console.Write("Enter Category ID to delete: ");
//                                     int delId = int.Parse(Console.ReadLine() ?? "0");
//                                     categoryService.DeleteCategory(delId);
//                                     break;

//                                 case "5":
//                                     managingCategories = false; //to exit the category management loop
//                                     break;

//                                 default:
//                                     Console.WriteLine("Invalid option. Please try again.");
//                                     break;
//                             }
//                         }
//                         break;

//         case "8":
//             running = false;
//             break;

//         default:
//             Console.WriteLine("Invalid option. Try again.");
//             break;

//     }
// }

// Console.WriteLine ("Exiting app....");

using System;
using Sep2026.MartInventoryConsoleApp;

ProductService service = new ProductService();
var categoryService = new CategoryService();
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
    Console.WriteLine("7. Manage Categories");
    Console.WriteLine("8. Exit from the System");
    Console.Write("Enter your choice: ");

    string option = Console.ReadLine() ?? "";

    switch (option)
    {
        case "1":
            Console.Write("Please enter product name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter category: ");
            string category = Console.ReadLine()!;

            Console.Write("Enter product price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter product quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            service.AddProduct(name, category, price, quantity);
            break;

        case "2":
            service.ViewAllProducts();
            break;

        case "3":
            Console.Write("Enter product name or category to search: ");
            string keyword = Console.ReadLine()!;
            service.SearchProduct(keyword);
            break;

        case "4":
            Console.WriteLine("\n--- Update Product ---");
            service.ViewAllProducts();

            Console.Write("Enter the product ID you want to update: ");
            string productId = Console.ReadLine()!;

            service.UpdateProduct(productId);
            break;

        case "5":
            Console.WriteLine("\n--- Delete selected product ---");
            service.ViewAllProducts();
            Console.Write("Enter the product ID you want to delete: ");
            string deleteId = Console.ReadLine()!;

            service.DeleteProduct(deleteId);
            break;

        case "6":
            service.ViewRestockProducts();
            break;

        case "7":
            bool managingCategories = true;
            while (managingCategories)
            {
                Console.WriteLine("\n--- Manage Categories ---");
                Console.WriteLine("1. View Categories");
                Console.WriteLine("2. Add Category");
                Console.WriteLine("3. Update Category");
                Console.WriteLine("4. Delete Category");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("ရွေးချယ်ပါ (1-5): ");

                string catChoice = Console.ReadLine() ?? "";
                switch (catChoice)
                {
                    case "1":
                        var categories = categoryService.GetAllCategories();
                        Console.WriteLine("\n--- Category List ---");
                        foreach (var c in categories)
                        {
                            Console.WriteLine($"ID: {c.Id}, Name: {c.Name}, Description: {c.Description}");
                        }
                        break;

                    case "2":
                        var newCategory = new CategoryDto();
                        Console.Write("Enter Category Name: ");
                        newCategory.Name = Console.ReadLine()!;
                        Console.Write("Enter Description: ");
                        newCategory.Description = Console.ReadLine()!;

                        categoryService.AddCategory(newCategory);
                        break;

                    case "3":
                        Console.Write("Enter Category ID to update: ");
                        int catId = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("Enter new Category Name: ");
                        string newName = Console.ReadLine()!;
                        Console.Write("Enter new Description: ");
                        string newDesc = Console.ReadLine()!;
                        categoryService.UpdateCategory(catId, newName, newDesc);
                        break;

                    case "4":
                        Console.Write("Enter Category ID to delete: ");
                        int delId = int.Parse(Console.ReadLine() ?? "0");
                        categoryService.DeleteCategory(delId);
                        break;

                    case "5":
                        managingCategories = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            break;

        case "8":
            running = false;
            break;

        default:
            Console.WriteLine("Invalid option. Try again.");
            break;
    }
}

Console.WriteLine("Exiting app....");
