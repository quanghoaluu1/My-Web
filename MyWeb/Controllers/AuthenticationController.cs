using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.Interfaces;
using WebApplication1.Requests;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/authentication/")]
public class AuthenticationController: ControllerBase
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
            return Redirect("/");
        }
        return Unauthorized("Invalid username or password");
    }
}