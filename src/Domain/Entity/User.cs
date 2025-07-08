using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity;

public class User : BaseEntity
{
    [MaxLength(32)]
    [MinLength(8)]
    public required string UserName { get; set; }

    [MaxLength(32)]
    [EmailAddress]
    [MinLength(8)]
    public required string Email { get; set; }

    public string? PasswordHash { get; set; }
}
