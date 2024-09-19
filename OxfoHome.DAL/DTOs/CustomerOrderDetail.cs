namespace OxfoHome.DAL.DTOs;

public class CustomerOrderDetailDTO
{
    public int Id { get; set; }

    public int? CustomerOrderId { get; set; }

    public int? ProductId { get; set; }

    public int? QuantityOrdered { get; set; }

    public int? Price { get; set; }

    public DateTime? CreatedOn { get; set; }

    public byte? Status { get; set; }
}
