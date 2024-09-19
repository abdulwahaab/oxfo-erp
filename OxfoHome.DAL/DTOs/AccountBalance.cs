using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OxfoHome.DAL.DTOs;

public class AccountBalance
{
    public decimal? TotalPaid { get; set; }
    public decimal? TotalReceived { get; set; }
    public decimal? Balance { get; set; }
}
