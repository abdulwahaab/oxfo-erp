using System;
using System.Collections.Generic;

namespace OxfoHome.Data.Entities;

public partial class ActivityLog
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Url { get; set; }

    public string? Source { get; set; }

    public string? Message { get; set; }

    public string? StackTrace { get; set; }

    public string? ClientIp { get; set; }

    public string? UserAgent { get; set; }

    public string? AdditionalData { get; set; }

    public DateTime? CreatedOn { get; set; }
}
