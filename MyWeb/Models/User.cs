using System.ComponentModel.DataAnnotations;
using WebApplication1.Enums;

namespace WebApplication1.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public required string FirstName { get; set; }
    [Required]
    [StringLength(50)]
    public required string LastName { get; set; }
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    [StringLength(50)]
    public string Password { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
    public AccountEnum Type { get; set; }
}