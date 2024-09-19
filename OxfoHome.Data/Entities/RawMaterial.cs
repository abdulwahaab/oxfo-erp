using System;
using System.Collections.Generic;

namespace OxfoHome.Data.Entities;

public partial class RawMaterial
{
    public int Id { get; set; }

    public string ItemName { get; set; } = null!;

    public string? ItemNumber { get; set; }

    public int? YarnCount { get; set; }

    public string? Description { get; set; }

    public decimal? Quantity { get; set; }
}
