using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OxfoHome.DAL.DTOs;

public class RawMaterialOrderDTO
{
    [DisplayName("Order ID")]
    public int Id { get; set; }

    [Required]
    [IgnoreDisplay]
    public int SupplierId { get; set; }

    [DisplayName("Supplier Name")]
    public string? SupplierName { get; set; }

    [DisplayName("Expected Delivery Date")]
    public DateTime? ExpectedDelivery { get; set; }

    [DisplayName("Total Amount")]
    public int? TotalAmount { get; set; }

    [DisplayName("Created By")]
    public int? CreatedBy { get; set; }

    [DisplayName("Created On")]
    public DateTime? CreatedOn { get; set; }

    public byte? Status { get; set; }
}
