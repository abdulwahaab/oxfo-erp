namespace OxfoHome.DAL.DTOs;

public partial class AccountPaymentDTO
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? EntityId { get; set; }

    public string? EntityType { get; set; }

    public decimal? Amount { get; set; }

    public string? Type { get; set; }

    public string? Notes { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public byte? Status { get; set; }
}
