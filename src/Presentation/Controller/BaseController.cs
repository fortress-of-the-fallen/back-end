using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Responses.Base;

namespace FortressOfTheFallen.Presentation.Controller;

[ApiController]
public class BaseController : ControllerBase
{
    [NonAction]
    protected IActionResult InternalServerError(object? data)
    {
        if (data is ExecutionRes apiResult)
        {
            apiResult.Error = string.IsNullOrEmpty(apiResult.Error)
                ? "Something went wrong. Please try again later"
                : apiResult.Error;
        }

        return StatusCode(StatusCodes.Status500InternalServerError, data);
    }

    [NonAction]
    protected IActionResult BadRequest<T>(ResultRes<T> response, string errorCode)
    {
        response.ErrorCode = errorCode;
        return BadRequest(response);
    }
}
