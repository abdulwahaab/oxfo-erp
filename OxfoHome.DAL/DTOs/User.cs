using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OxfoHome.DAL.DTOs;

public class UserDTO
{
    [IgnoreDisplay]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    [IgnoreDisplay]
    [Required(ErrorMessage = "Role is required")]
    public int RoleId { get; set; }

    public string Role { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password needs to be at least 6 characters")]
    public string Password { get; set; } = null!;

    [IgnoreDisplay]
    [CompareAttribute("Password", ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; set; }

    [DisplayName("Last Login Date")]
    public DateTime? LastLogin { get; set; }

    [DisplayName("Created On")]
    public DateTime CreatedOn { get; set; }

    public byte Status { get; set; }
}
