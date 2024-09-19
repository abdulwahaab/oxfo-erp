using System;
using System.Collections.Generic;

namespace OxfoHome.Data.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int PricePerDozen { get; set; }

    public string? Color { get; set; }

    public string? Size { get; set; }

    public int? Quantity { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedOn { get; set; }

    public byte Status { get; set; }
}
