using System;
using System.Collections.Generic;

namespace OxfoHome.Data.Entities;

public partial class Account
{
    public int Id { get; set; }

    public string? AccountName { get; set; }

    public string? BankName { get; set; }

    public string? AccountNumber { get; set; }

    public string? AccountType { get; set; }

    public decimal? Balance { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedOn { get; set; }

    public byte? Status { get; set; }
}
