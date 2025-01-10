using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Requests;

public class RegisterRequest
{
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string Phone { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }
}