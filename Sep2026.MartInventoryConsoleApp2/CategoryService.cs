using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Sep2026.MartInventoryConsoleApp2;

public class CategoryService
{
    private readonly string _connectionString;

    public CategoryService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void AddCategory(CategoryDto category)
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = "INSERT INTO dbo.Categories (Name, Description) VALUES (@Name, @Description)";
        connection.Execute(sql, category);
        Console.WriteLine($"Successfully added category: {category.Name}");
    }

    public List<CategoryDto> GetAllCategories()
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = "SELECT Id, Name, Description FROM dbo.Categories";
        return connection.Query<CategoryDto>(sql).ToList();
    }

    public void UpdateCategory(int id, string newName, string newDescription)
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = "UPDATE dbo.Categories SET Name = @Name, Description = @Description WHERE Id = @Id";
        int rows = connection.Execute(sql, new { Id = id, Name = newName, Description = newDescription });

        Console.WriteLine(rows > 0
            ? $"Successfully updated category ID {id}"
            : $"Category with ID {id} not found.");
    }

    public void DeleteCategory(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = "DELETE FROM dbo.Categories WHERE Id = @Id";
        int rows = connection.Execute(sql, new { Id = id });

        Console.WriteLine(rows > 0
            ? $"Successfully deleted category ID {id}"
            : $"Category with ID {id} not found.");
    }
}