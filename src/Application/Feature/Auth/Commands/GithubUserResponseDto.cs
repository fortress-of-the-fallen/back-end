using System.Text.Json.Serialization;

namespace Application.Feature.Auth.Commands;

public class GithubUserResponseDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("login")]
    public required string Login { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("avatar_url")]
    public required string AvatarUrl { get; set; } 
}