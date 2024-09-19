using System.ComponentModel;

namespace OxfoHome.DAL.DTOs;

public partial class PayeeDTO
{
    [IgnoreDisplay]
    public int Id { get; set; }

    [IgnoreDisplay]
    public int? EntityId { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Notes { get; set; }

    public string? Type { get; set; }

    [DisplayName("Created On")]
    public DateTime? CreatedOn { get; set; }

    public byte? Status { get; set; }
}
