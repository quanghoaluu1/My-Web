using WebApplication1.Enums;

namespace WebApplication1.Requests;

public class UpdateUserRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}