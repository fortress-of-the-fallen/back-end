using Microsoft.AspNetCore.Mvc;

namespace FortressOfTheFallen.Presentation.Controller.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello from version 1");
    }
}