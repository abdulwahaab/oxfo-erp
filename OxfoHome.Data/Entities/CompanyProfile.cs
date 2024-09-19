using System;
using System.Collections.Generic;

namespace OxfoHome.Data.Entities;

public partial class CompanyProfile
{
    public int Id { get; set; }

    public string? CompanyName { get; set; }

    public string? Address { get; set; }

    public string? ContactName { get; set; }

    public string? ContactPhone { get; set; }

    public string? Notes { get; set; }

    public DateTime? LastModified { get; set; }
}
