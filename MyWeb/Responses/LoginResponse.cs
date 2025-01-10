
namespace WebApplication1.Responses;

public class LoginResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }
}