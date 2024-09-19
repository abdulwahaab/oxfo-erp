using System;
using System.Collections.Generic;

namespace OxfoHome.Data.Entities;

public partial class PayingEntity
{
    public int Id { get; set; }

    public int? EntityId { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedOn { get; set; }

    public byte? Status { get; set; }
}
