using System.ComponentModel;

namespace OxfoHome.DAL.DTOs;

public class DyeingColorRateDTO
{
    [IgnoreDisplay]
    public int Id { get; set; }

    [IgnoreDisplay]
    public int? DyeingVendorId { get; set; }

    [DisplayName("Dyeing Vendor Name")]
    public string? DyeingVendorName { get; set; }

    public string? Color { get; set; }

    public int? Price { get; set; }

    public string? Notes { get; set; }
}
