using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.Enums;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Requests;

namespace WebApplication1.Controllers;

[ApiController]
[Route("user")]
public class UserController(ILogger<UserController> logger, MyDBContext context) : ControllerBase, IUserController
{
    [HttpGet("all")]
    public List<User> GetUsers()
    {
       return context.Users.ToList();
    }
    
    [HttpGet("user/{email}")]
    public User? GetUserByEmail(string email)
    {
        var user = context.Users.FirstOrDefault(u => u.Email == email);
        return user;
    }

    [HttpGet("user/{id:int}")]
    public User? GetUserById(int id)
    {
        var user = context.Users.FirstOrDefault(u => u.Id == id);
        return user;
    }

    [HttpDelete("user/{id:int}")]
    public void DeleteUser(int id)
    {
        var user = context.Users.FirstOrDefault(u => u.Id == id);
        user.IsActive = false;
        context.Users.Update(user);
        context.SaveChanges();
    }

    [HttpPut("user/{id:int}")]
    public User? UpdateUser(int id, [FromBody]UpdateUserRequest updateUserRequest)
    {
        var user = GetUserById(id);
        user = MapUpdateUserRequest(updateUserRequest, user);
        context.Users.Update(user);
        context.SaveChanges();
        return user;
        
    }

    private User MapUpdateUserRequest(UpdateUserRequest updateUserRequest, User user)
    {
        if (!string.IsNullOrEmpty(updateUserRequest.Name)) user.Name = updateUserRequest.Name;
        if (!string.IsNullOrEmpty(updateUserRequest.Email)) user.Email = updateUserRequest.Email;
        if (!string.IsNullOrEmpty(updateUserRequest.PhoneNumber)) user.PhoneNumber = updateUserRequest.PhoneNumber;
        if (!string.IsNullOrEmpty(updateUserRequest.Address)) user.Address = updateUserRequest.Address;
        if (!string.IsNullOrEmpty(updateUserRequest.Password))
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(updateUserRequest.Password);
            user.Password = hashedPassword;

        }
        return user;
    }

    [HttpPut("user/{id:int}/{role}")]
    public void ChangeRole(int id, Role role)
    {
        var user = GetUserById(id);
        user.Role = role;
        context.Users.Update(user);
        context.SaveChanges();
    }
}