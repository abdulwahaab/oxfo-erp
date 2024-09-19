using System;
using System.Collections.Generic;

namespace OxfoHome.Data.Entities;

public partial class DyeingColorRate
{
    public int Id { get; set; }

    public int? DyeingVendorId { get; set; }

    public string? Color { get; set; }

    public int? Price { get; set; }

    public string? Notes { get; set; }
}
