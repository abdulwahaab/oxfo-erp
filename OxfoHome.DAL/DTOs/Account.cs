using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OxfoHome.DAL.DTOs;

public class AccountDTO
{
    [IgnoreDisplay]
    public int Id { get; set; }

    [DisplayName("Account Name")]
    [Required(ErrorMessage = "Account Name is required")]
    public string? AccountName { get; set; }

    [DisplayName("Account Number")]
    [Required(ErrorMessage = "Account Number is required")]
    public string? AccountNumber { get; set; }

    [DisplayName("Bank Name")]
    [Required(ErrorMessage = "Bank Name is required")]
    public string? BankName { get; set; }

    [DisplayName("Account Type")]
    public string? AccountType { get; set; }

    [Required(ErrorMessage = "Balance is required")]
    public decimal? Balance { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedOn { get; set; }

    public byte? Status { get; set; }
}
