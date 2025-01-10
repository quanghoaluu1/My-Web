using CloudinaryDotNet;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
public class CloudinaryController: ControllerBase
{
    [HttpPost("upload")]
    public IActionResult Upload(IFormFile file, [FromServices] CloudinaryService cloudinaryService)
    {
        if (file.Length == 0)
        {
            return BadRequest("No file uploaded");
        }

        var url = cloudinaryService.Upload(file);
        return Ok(url);
    }
}