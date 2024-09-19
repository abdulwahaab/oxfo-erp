using System;
using System.Collections.Generic;

namespace OxfoHome.Data.Entities;

public partial class RawMaterialOrder
{
    public int Id { get; set; }

    public int? SupplierId { get; set; }

    public DateTime? ExpectedDelivery { get; set; }

    public int? TotalAmount { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public byte? Status { get; set; }
}
