using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity;

public class User
{
    [BsonId]
    public Guid Id { get; set; }

    [BsonElement("username")]
    [MaxLength(32)]
    [MinLength(8)]
    public required string UserName { get; set; }

    [BsonElement("email")]
    [MaxLength(32)]
    [EmailAddress]
    [MinLength(8)]
    public required string Email { get; set; }

    [BsonElement("passwordHash")]
    public string? PasswordHash { get; set; }
}
