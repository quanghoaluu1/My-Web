using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class UserController(ILogger<UserController> logger, MyDBContext context) : ControllerBase, IUserController
{
    public List<User> GetUsers()
    {
       return context.Users.ToList();
    }
    
    public User? GetUserByEmail(string email)
    {
        var user = context.Users.FirstOrDefault(u => u.Email == email);
        return user;
    }
    
}