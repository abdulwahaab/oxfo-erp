using System;
using System.Collections.Generic;

namespace OxfoHome.Data.Entities;

public partial class Supplier
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ContactName { get; set; }

    public string? ContactPhone { get; set; }

    public string? Phone2 { get; set; }

    public string? Address { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedOn { get; set; }

    public byte Status { get; set; }
}
