using System;
using System.Collections.Generic;

namespace Sep2026.MartInventoryConsoleApp3.Database.AppDbContextModels;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
