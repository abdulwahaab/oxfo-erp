using System;
using System.Collections.Generic;

namespace OxfoHome.Data.Entities;

public partial class DyeingProcess
{
    public int Id { get; set; }

    public int? DyeingVendorId { get; set; }

    public int? VoucherNumber { get; set; }

    public int? RollNo { get; set; }

    public int Pcs { get; set; }

    public decimal? Weight { get; set; }

    public string? Color { get; set; }

    public decimal? Price { get; set; }

    public DateTime? ExpectedDelivery { get; set; }

    public string? Notes { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ReceivedOn { get; set; }

    public DateTime? CreatedOn { get; set; }

    public byte? Status { get; set; }
}
