using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace FortressOfTheFallen.Presentation.Controller.v1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}")]
public class HelloController : BaseController
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello from version 1");
    }
}