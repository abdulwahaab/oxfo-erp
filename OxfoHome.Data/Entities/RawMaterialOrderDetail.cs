using System;
using System.Collections.Generic;

namespace OxfoHome.Data.Entities;

public partial class RawMaterialOrderDetail
{
    public int Id { get; set; }

    public int? RawMaterialOrderId { get; set; }

    public int? RawMaterialId { get; set; }

    public int? QuantityOrdered { get; set; }

    public int? PricePerItem { get; set; }

    public string? PriceUnit { get; set; }

    public string? ReceivedBy { get; set; }

    public DateTime? ReceivedDate { get; set; }

    public DateTime? CreatedOn { get; set; }

    public byte? Status { get; set; }
}
