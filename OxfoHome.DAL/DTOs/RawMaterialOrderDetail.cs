using System.ComponentModel;

namespace OxfoHome.DAL.DTOs;

public class RawMaterialOrderDetailDTO
{
    [IgnoreDisplay]
    public int Id { get; set; }

    [IgnoreDisplay]
    public int? RawMaterialOrderId { get; set; }

    [IgnoreDisplay]
    public int? RawMaterialId { get; set; }

    [DisplayName("Item Name")]
    public string? ItemName { get; set; }

    [DisplayName("Item Number")]
    public string? ItemNumber { get; set; }

    [DisplayName("Quantity Ordered")]
    public int? QuantityOrdered { get; set; }

    [DisplayName("Price Per Item")]
    public int? PricePerItem { get; set; }

    [DisplayName("Price Unit")]
    public string? PriceUnit { get; set; }

    [DisplayName("Received By")]
    public string? ReceivedBy { get; set; }

    [DisplayName("Received Date")]
    public DateTime? ReceivedDate { get; set; }

    [DisplayName("Created On")]
    public DateTime? CreatedOn { get; set; }

    public byte? Status { get; set; }
}
