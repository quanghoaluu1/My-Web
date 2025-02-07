using System.ComponentModel.DataAnnotations;
using WebApplication1.Enums;

namespace WebApplication1.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    public string Password { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
    public Role Role { get; set; }
}