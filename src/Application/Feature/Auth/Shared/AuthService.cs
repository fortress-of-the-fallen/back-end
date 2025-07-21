using Application.Interface.Caching;
using Domain.Constains;
using Domain.Constants;
using Domain.Entity;
using Microsoft.AspNetCore.Http;

namespace Application.Feature.Auth.Shared;

public interface IAuthService
{
    Task CreateSessionAsync(User user, CancellationToken cancellationToken);
}

public class AuthService(
    ICacheManager cacheManager,
    IHttpContextAccessor httpContextAccessor
) : IAuthService
{
    public async Task CreateSessionAsync(User user, CancellationToken cancellationToken)
    {
        var session = new Session
        {
            Id = user.CurrentSessionId!,
            UserId = user.Id,
            DeviceFingerprint = httpContextAccessor.HttpContext?.Request.Headers[GlobalConstants.Headers.DeviceFingerprint].ToString() ?? string.Empty,
            IpAddress = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty,
            UserAgent = httpContextAccessor.HttpContext?.Request.Headers[GlobalConstants.Headers.UserAgent].ToString() ?? string.Empty
        };

        await cacheManager.SetData<Session>(CacheKeys.Session.SessionId(user.CurrentSessionId!), session, TimeSpan.FromHours(CacheKeys.Session.SessionDurationHours), cancellationToken);
    }
}