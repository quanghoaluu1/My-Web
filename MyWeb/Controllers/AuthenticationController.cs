using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.Enums;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Requests;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/authentication/")]
public partial class AuthenticationController: ControllerBase, IAuthenticationController
{
    private readonly ILogger<AuthenticationController> _logger;
    private MyDBContext _context;
    private readonly IUserController _userController;

    public AuthenticationController(ILogger<AuthenticationController> logger, MyDBContext context, IUserController userController)
    {
        _logger = logger;
        _context = context;
        _userController = userController;
    }
    
    [HttpPost("login")]
    public IActionResult Login([FromBody]LoginRequest request)
    {
        var user = _userController.GetUserByEmail(request.Email);

        if (user != null && request.Password.Equals(user.Password))
        {
            return Ok("Login successful");
        }
        return Unauthorized("Invalid username or password");
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody]RegisterRequest registerRequest)
    {
        if (_userController.GetUserByEmail(registerRequest.Email) != null)
        {
            return BadRequest("Email already exists");
        }

        try
        {
            switch (CheckValidPassword(registerRequest.Password, registerRequest.ConfirmPassword))
            {
                case 1:
                    return BadRequest("Password must be between 1 and 20 characters");
                case 2:
                    return BadRequest("Password must include at least one letter and one number.");
                case 3:
                    return BadRequest("Password do not match");
                case 0:
                    var user = MatchUserToRegister(registerRequest);
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return Ok("Registration successful");
            }
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
        return BadRequest();
    }

    [GeneratedRegex(@"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{1,20}$")]
    private static partial Regex MyRegex();

    private int CheckValidPassword(string password, string confirmPassword)
    {
        if (string.IsNullOrEmpty(password) || password.Length < 1 ||
            password.Length > 20)
        {
            return 1;
        }

        if (!MyRegex().IsMatch(password))
        {
            return 2;
        }

        if (password != confirmPassword)
        {
            return 3;
        }
        return 0;
    }

    private User MatchUserToRegister(RegisterRequest registerRequest)
    {
        string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(registerRequest.Password);
        User user = new User()
        {
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            Email = registerRequest.Email,
            Password = hashedPassword,
            IsActive = true,
            Type = AccountEnum.Customer
        };
        return user;
    }
}