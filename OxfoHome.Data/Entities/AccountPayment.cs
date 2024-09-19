using System;
using System.Collections.Generic;

namespace OxfoHome.Data.Entities;

public partial class AccountPayment
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? EntityId { get; set; }

    public string? EntityType { get; set; }

    public decimal? Amount { get; set; }

    public string? Type { get; set; }

    public string? Notes { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public byte? Status { get; set; }
}
