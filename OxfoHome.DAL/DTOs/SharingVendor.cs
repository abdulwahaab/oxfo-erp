using System.ComponentModel;

namespace OxfoHome.DAL.DTOs;

public class SharingVendorDTO
{
    [IgnoreDisplay]
    public int Id { get; set; }

    [DisplayName("Business Name")]
    public string? Name { get; set; }

    [DisplayName("Contact Name")]
    public string? ContactName { get; set; }

    [DisplayName("Contact Phone")]
    public string? ContactPhone { get; set; }

    [DisplayName("Second Phone")]
    public string? Phone2 { get; set; }

    public int? Price { get; set; }

    public string? Address { get; set; }

    public string? Notes { get; set; }

    [DisplayName("Created On")]
    public DateTime? CreatedOn { get; set; }

    public byte? Status { get; set; }
}
