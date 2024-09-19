using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OxfoHome.DAL.DTOs;

public class RawMaterialDTO
{
    [IgnoreDisplay]
    public int Id { get; set; }

    [DisplayName("Item Name")]
    [Required(ErrorMessage = "Item Name is required")]
    public string ItemName { get; set; } = null!;

    [IgnoreDisplay]
    public string? ItemNumber { get; set; }

    [DisplayName("Yarn Count")]
    [Required(ErrorMessage = "Yarn Count is required")]
    public int? YarnCount { get; set; }

    public decimal? Quantity { get; set; }

    public string? Description { get; set; }
}
