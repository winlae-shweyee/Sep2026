using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Sep2026.MartInventoryConsoleApp2;

public class ProductService
{
    private readonly string _connectionString;

    public ProductService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void AddProduct(ProductDto product)
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = @"INSERT INTO Products (Id, Name, Category, Price, Quantity)
                       VALUES (@Id, @Name, @Category, @Price, @Quantity)";
        connection.Execute(sql, product);
        Console.WriteLine($"Added new product: {product.Name}");
    }

    public List<ProductDto> GetAllProducts()
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = "SELECT Id, Name, Category, Price, Quantity FROM Products";
        return connection.Query<ProductDto>(sql).ToList();
    }

    public void UpdateStock(string id, int quantity)
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = "UPDATE Products SET Quantity = @Quantity WHERE Id = @Id";
        int rows = connection.Execute(sql, new { Id = id, Quantity = quantity });

        Console.WriteLine(rows > 0
            ? "Updated stock successfully."
            : $"ID {id} not found.");
    }

    public List<ProductDto> SearchProducts(string keyword)
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = @"SELECT Id, Name, Category, Price, Quantity
                       FROM Products
                       WHERE Name LIKE @Keyword OR Category LIKE @Keyword";
        return connection.Query<ProductDto>(sql, new { Keyword = $"%{keyword}%" }).ToList();
    }

    public void DeleteProduct(string id)
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = "DELETE FROM Products WHERE Id = @Id";
        int rows = connection.Execute(sql, new { Id = id });

        Console.WriteLine(rows > 0
            ? $"ID {id} deleted successfully."
            : $"ID {id} not found.");
    }
}