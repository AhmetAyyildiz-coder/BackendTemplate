using Core.Dto;

namespace DTOs.Users;

public class UserForLoginDto : IDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}