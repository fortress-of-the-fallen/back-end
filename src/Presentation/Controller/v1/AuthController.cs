using Application.Feature.Auth.Commands;
using Asp.Versioning;
using FortressOfTheFallen.Presentation.Controller;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Responses.Base;

namespace Presentation.Controller.v1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : BaseController
{
    /// <summary>
    /// Login with GitHub
    /// </summary>
    /// <remarks>
    /// Possible error codes:
    /// - Login.InvalidGithubCode: Invalid GitHub code
    /// - Login.UserAlreadyLoggedIn: User is already logged in
    /// </remarks>
    [Route("github-login")]
    [HttpGet]
    public async Task<IActionResult> GithubLogin(ISender sender, [FromQuery] string code)
    {
        var response = new ExecutionRes();

        var errorCode = await sender.Send(new GithubLoginCommand { Code = code });
        if (!string.IsNullOrEmpty(errorCode))
        {
            response.ErrorCode = errorCode;
            return BadRequest(response);
        }

        return Ok(response);
    }
}