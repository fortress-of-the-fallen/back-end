using Domain.Constants;

namespace Domain.Entity;

public class Session
{
    public required string Id { get; set; }

    public required Guid UserId { get; set; }

    public required string DeviceFingerprint { get; set; }

    public required string IpAddress { get; set; }

    public required string UserAgent { get; set; }

    public bool IsRevoked { get; set; } = false;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset ExpiredAt { get; set; } = DateTimeOffset.UtcNow.AddHours(CacheKeys.Session.SessionDurationHours);

    public bool IsExpired() => DateTimeOffset.UtcNow >= ExpiredAt;

    public bool IsActive() => !IsRevoked && !IsExpired();

    public void Extend(TimeSpan duration)
    {
        if (!IsRevoked && !IsExpired())
        {
            ExpiredAt = DateTimeOffset.UtcNow.Add(duration);
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}