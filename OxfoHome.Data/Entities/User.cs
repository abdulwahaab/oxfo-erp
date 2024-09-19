using System;
using System.Collections.Generic;

namespace OxfoHome.Data.Entities;

public partial class User
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string Password { get; set; } = null!;

    public DateTime? LastLogin { get; set; }

    public DateTime CreatedOn { get; set; }

    public byte Status { get; set; }
}
