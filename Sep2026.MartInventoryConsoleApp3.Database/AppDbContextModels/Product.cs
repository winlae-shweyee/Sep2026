using System;
using System.Collections.Generic;

namespace Sep2026.MartInventoryConsoleApp3.Database.AppDbContextModels;

public partial class Product
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Category { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}
