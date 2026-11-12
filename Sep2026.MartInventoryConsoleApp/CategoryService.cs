// using System;

// namespace Sep2026.MartInventoryConsoleApp;

// public class CategoryService
// {
//     private readonly List<CategoryDto> _categories = new ();

//     public void AddCategory(CategoryDto category)
//         {
//             _categories.Add(category);
//             Console.WriteLine($"Successfully added category: {category.Name}");
//         }

//         public List<CategoryDto> GetAllCategories()
//         {
//             return _categories;
//         }

//         public CategoryDto? GetCategoryById(int id)
//         {
//             return _categories.FirstOrDefault(c => c.Id == id);
//         }

//         public void UpdateCategory(int id, string newName, string newDescription)
//         {
//             var category = _categories.FirstOrDefault(c => c.Id == id);
//             if (category != null)
//             {
//                 category.Name = newName;
//                 category.Description = newDescription;
//                 Console.WriteLine($"Successfully updated category: {category.Name}");
//             }
//             else
//             {
//                 Console.WriteLine($"Category with ID {id} not found.");
//             }
//         }

//         public void DeleteCategory(int id)
//         {
//             var category = _categories.FirstOrDefault(c => c.Id == id);
//             if (category != null)
//             {
//                 _categories.Remove(category);
//                 Console.WriteLine($"Successfully deleted category: {category.Name}");
//             }
//             else
//             {
//                 Console.WriteLine($"Category with ID {id} not found.");
//             }
//         }

// }
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Sep2026.MartInventoryConsoleApp;

public class CategoryService
{
    private readonly string _connectionString = "Server=.;Database=Sep2026;User Id=sa;Password=sasa@123;TrustServerCertificate=True;";

    public void AddCategory(CategoryDto category)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        string query = "INSERT INTO Categories (Name, Description) VALUES (@Name, @Description)";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", category.Name);
        command.Parameters.AddWithValue("@Description", category.Description ?? (object)DBNull.Value);

        command.ExecuteNonQuery();
        Console.WriteLine($"Successfully added category: {category.Name}");
    }

    public List<CategoryDto> GetAllCategories()
    {
        var categories = new List<CategoryDto>();
        using var connection = new SqlConnection(_connectionString);
        
        string query = "SELECT Id, Name, Description FROM Categories";
        using var command = new SqlCommand(query, connection);
        using var adapter = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        foreach (DataRow row in dt.Rows)
        {
            categories.Add(new CategoryDto
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString() ?? "",
                Description = row["Description"] == DBNull.Value ? "" : row["Description"].ToString() ?? ""
            });
        }

        return categories;
    }

    public void UpdateCategory(int id, string newName, string newDescription)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        string query = "UPDATE Categories SET Name = @Name, Description = @Description WHERE Id = @Id";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);
        command.Parameters.AddWithValue("@Name", newName);
        command.Parameters.AddWithValue("@Description", newDescription ?? (object)DBNull.Value);

        int rowsAffected = command.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
            Console.WriteLine($"Successfully updated category ID {id}");
        }
        else
        {
            Console.WriteLine($"Category with ID {id} not found.");
        }
    }

    public void DeleteCategory(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        string query = "DELETE FROM Categories WHERE Id = @Id";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        int rowsAffected = command.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
            Console.WriteLine($"Successfully deleted category ID {id}");
        }
        else
        {
            Console.WriteLine($"Category with ID {id} not found.");
        }
    }
}