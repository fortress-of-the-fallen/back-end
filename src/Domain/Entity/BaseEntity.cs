using Domain.IEntity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class BaseEntity : IBaseEntity
{
    [BsonId]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid CreatedBy { get; set; } = Guid.Empty;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public Guid UpdatedBy { get; set; } = Guid.Empty;

    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public bool IsDeleted { get; set; } = false;
}