using Core.Dto;

namespace Entities.DTOs;

public class UserForLoginDto : IDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}