
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OxfoHome.DAL.DTOs;

public partial class PaymentDTO
{
    [IgnoreDisplay]
    public int Id { get; set; }

    [IgnoreDisplay]
    public int? AccountId { get; set; }

    [IgnoreDisplay]
    public int? FromPayeeId { get; set; }

    [DisplayName("Paid By")]
    public string? FromPayeeName { get; set; }

    [IgnoreDisplay]
    public int? ToPayeeId { get; set; }

    [DisplayName("Paid To")]
    public string? ToPayeeName { get; set; }

    public decimal? Amount { get; set; }

    [DisplayName("Transaction Type")]
    public string? TransactionType { get; set; }

    public string? Attachment { get; set; }

    public string? Notes { get; set; }

    [IgnoreDisplay]
    public int? CreatedBy { get; set; }

    [DisplayName("Created On")]
    public DateTime? CreatedOn { get; set; }

    public byte? Status { get; set; }
}
