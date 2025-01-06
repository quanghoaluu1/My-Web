using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class UserController: ControllerBase, IUserController
{
    private readonly ILogger<UserController> _logger;
    private readonly MyDBContext _context;

    public UserController(ILogger<UserController> logger, MyDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    public List<User> GetUsers()
    {
        throw new NotImplementedException();
    }
    
    public User? GetUserByEmail(string email)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        return user;
    }
    
}