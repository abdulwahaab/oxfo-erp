using System.ComponentModel;

namespace OxfoHome.DAL.DTOs;

public class ProductDTO
{
    [IgnoreDisplay]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [DisplayName("Price Per Dozen")]
    public int? PricePerDozen { get; set; }

    public string? Color { get; set; }

    [IgnoreDisplay]
    public string? Size { get; set; }

    public int? Quantity { get; set; }

    public string? Description { get; set; }

    [DisplayName("Created On")]
    public DateTime CreatedOn { get; set; }

    [IgnoreDisplay]
    public byte Status { get; set; }
}
