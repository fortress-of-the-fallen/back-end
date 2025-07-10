using Application.Feature.Samples.Command;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FortressOfTheFallen.Presentation.Controller.v1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}")]
public class HelloController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get(ISender sender)
    {
        var result = await sender.Send(new CreateSampleCommand("Sample", "Sample Description"));

        return Ok(result);
    }
}