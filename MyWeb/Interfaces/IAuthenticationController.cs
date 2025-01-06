
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Requests;

namespace WebApplication1.Interfaces;

public interface IAuthenticationController
{
    public IActionResult Login(LoginRequest loginRequest);
    public IActionResult Register(RegisterRequest registerRequest);
}