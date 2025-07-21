namespace Domain.Entity;

public class User : BaseEntity
{
    public string? Email { get; set; }

    public string? UserName { get; set; }

    public string? PasswordHash { get; set; }

    public string? CurrentSessionId { get; set; }

    public GithubUser? GithubUser { get; set; }
}

public class GithubUser
{
    public long Id { get; set; }

    public required string Login { get; set; }

    public required string Name { get; set; }

    public string? Email { get; set; }

    public required string AvatarUrl { get; set; } 
}