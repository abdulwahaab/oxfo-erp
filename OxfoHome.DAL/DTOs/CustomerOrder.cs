namespace OxfoHome.DAL.DTOs;

public class CustomerOrderDTO
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? ExpectedDelivery { get; set; }

    public int? TotalAmount { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public byte? Status { get; set; }
}
