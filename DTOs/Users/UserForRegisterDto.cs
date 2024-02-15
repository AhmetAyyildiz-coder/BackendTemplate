using System.ComponentModel.DataAnnotations;

namespace DTOs.Users;

public class UserForRegisterDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }


    [MinLength(8, ErrorMessage = "New password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "New password must contain at least one uppercase letter, one lowercase letter, and one number.")]
    public required string Password { get; set; }
}