using System;

namespace Sep2026.MartInventoryConsoleApp;

public class ProductDto
{
    public string Id{ get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
