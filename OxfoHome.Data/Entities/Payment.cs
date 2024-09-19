using System;
using System.Collections.Generic;

namespace OxfoHome.Data.Entities;

public partial class Payment
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public int? ToPayeeId { get; set; }

    public int? FromPayeeId { get; set; }

    public decimal? Amount { get; set; }

    public string? TransactionType { get; set; }

    public string? Attachment { get; set; }

    public string? Notes { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public byte? Status { get; set; }
}
