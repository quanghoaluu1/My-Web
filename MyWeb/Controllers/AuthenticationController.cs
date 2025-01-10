using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Context;
using WebApplication1.Enums;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Requests;
using WebApplication1.Responses;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebApplication1.Controllers;

[ApiController]
[Route("authentication")]
public partial class AuthenticationController(
    MyDBContext context,
    IConfiguration config,
    IUserController userController)
    : ControllerBase, IAuthenticationController
{

    [HttpPost("login")]
    public IActionResult Login([FromBody]LoginRequest request)
    {
        var user = userController.GetUserByEmail(request.Email);

        if (user != null && BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.Password))
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? string.Empty));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCredentials
            );
            var response = new LoginResponse()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role.ToString(),
                IsActive = user.IsActive,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };
            return Ok(new {LoginResponse = response });
        }
        return Unauthorized("Invalid username or password");
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody]RegisterRequest registerRequest)
    {
        if (userController.GetUserByEmail(registerRequest.Email) != null)
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
                    context.Users.Add(user);
                    context.SaveChanges();
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
            Name = registerRequest.Name,
            Email = registerRequest.Email,
            PhoneNumber = registerRequest.Phone,
            Password = hashedPassword,
            IsActive = true,
            Role = Role.Customer
        };
        return user;
    }
}