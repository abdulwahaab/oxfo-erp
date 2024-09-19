﻿
namespace OxfoHome.DAL.DTOs;

public partial class ErrorLogDTO
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Url { get; set; }

    public string? Source { get; set; }

    public string? ErrorMessage { get; set; }

    public string? InnerException { get; set; }

    public string? StackTrace { get; set; }

    public string? ClientIp { get; set; }

    public string? UserAgent { get; set; }

    public string? AdditionalData { get; set; }

    public DateTime? CreatedOn { get; set; }
}
