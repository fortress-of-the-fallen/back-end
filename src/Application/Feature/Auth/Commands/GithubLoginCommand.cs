using System.Text.Json;
using Application.Feature.Auth.Shared;
using Application.Interface.DataAccess;
using Application.Interface.Identity;
using Application.Interfaces.Restful;
using Domain.Entity;
using Domain.Message;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Feature.Auth.Commands;

public record GithubLoginCommand : IRequest<string>
{
    public required string Code { get; init; }
}

public class GithubLoginCommandHandler(
    IConfiguration configuration,
    IRestfulService restfulService,
    IWriteUnitOfWork writeUnitOfWork,
    IIdGenerator idGenerator,
    IAuthService authService
) : IRequestHandler<GithubLoginCommand, string>
{
    private readonly string _clientId = configuration[Domain.Constains.ConfigKeys.Authorization.GithubSSO.ClientId];
    private readonly string _clientSecret = configuration[Domain.Constains.ConfigKeys.Authorization.GithubSSO.ClientSecret];
    private readonly string _callbackUrl = configuration[Domain.Constains.ConfigKeys.Authorization.GithubSSO.CallbackUrl];

    public async Task<string> Handle(GithubLoginCommand request, CancellationToken cancellationToken)
    {
        var (errorCode, accessToken) = await GetAcessTokenAsync(request.Code);
        if (!string.IsNullOrEmpty(errorCode) || string.IsNullOrEmpty(accessToken))
        {
            return errorCode;
        }

        var (userErrorCode, githubUserResponseDto) = await GetUserInfoAsync(accessToken);
        if (!string.IsNullOrEmpty(userErrorCode) || githubUserResponseDto == null)
        {
            return userErrorCode;
        }

        var (storeErrorCode, user) = await StoreUserAsync(githubUserResponseDto, cancellationToken);
        if (!string.IsNullOrEmpty(storeErrorCode) || user == null)
        {
            return storeErrorCode;
        }

        await authService.CreateSessionAsync(user, cancellationToken);

        return string.Empty;
    }

    private async Task<(string, string?)> GetAcessTokenAsync(string Code)
    {
        var formData = new Dictionary<string, string>
        {
            { "client_id", _clientId },
            { "client_secret", _clientSecret },
            { "code", Code },
            { "redirect_uri", _callbackUrl }
        };

        var header = new Dictionary<string, string>
        {
            { "Accept", "application/json" }
        };

        var (status, res) = await restfulService.Post(
            configuration[Domain.Constains.ConfigKeys.Authorization.GithubSSO.AccessTokenUrl],
            formData,
            header
        );

        if (status != System.Net.HttpStatusCode.OK)
        {
            return (AuthControllerMsg.Login.InvalidGithubCode, null);
        }

        var json = JsonDocument.Parse(res);
        if (!json.RootElement.TryGetProperty("access_token", out var accessTokenElement))
        {
            return (AuthControllerMsg.Login.InvalidGithubCode, null);
        }

        var accessToken = accessTokenElement.GetString();
        return (string.Empty, accessToken);
    }

    private async Task<(string, GithubUserResponseDto?)> GetUserInfoAsync(string accessToken)
    {
        var headers = new Dictionary<string, string>
        {
            { "Authorization", $"Bearer {accessToken}" },
            { "User-Agent", "FoTF" },
            { "Accept", "application/json" }
        };

        var (status, json) = await restfulService.Get(
            configuration[Domain.Constains.ConfigKeys.Authorization.GithubSSO.UserApiUrl],
            headers
        );

        if (status != System.Net.HttpStatusCode.OK)
        {
            return (AuthControllerMsg.Login.InvalidGithubCode, null);
        }

        var user = JsonSerializer.Deserialize<GithubUserResponseDto>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return (string.Empty, user);
    }

    private async Task<(string, User)> StoreUserAsync(GithubUserResponseDto userDto, CancellationToken cancellationToken)
    {
        var user = await writeUnitOfWork.GetRepository<User>()
            .Single(
                u => u.GithubUser != null && u.GithubUser.Id == userDto.Id
            );

        if (user == null)
        {
            user = new User
            {
                CurrentSessionId = idGenerator.GenerateSessionId(),
                GithubUser = new GithubUser
                {
                    Id = userDto.Id,
                    Login = userDto.Login,
                    AvatarUrl = userDto.AvatarUrl,
                    Name = userDto.Name,
                    Email = userDto.Email,
                }
            };

            writeUnitOfWork.GetRepository<User>().Insert(user);
        }
        else
        {
            if (!string.IsNullOrEmpty(user.CurrentSessionId))
            {
                return (AuthControllerMsg.Login.UserAlreadyLoggedIn, user);
            }

            user.CurrentSessionId = idGenerator.GenerateSessionId();
            writeUnitOfWork.GetRepository<User>().Update(user);
        }

        await writeUnitOfWork.ExecuteAsync(cancellationToken);
        return (string.Empty, user);
    }
}