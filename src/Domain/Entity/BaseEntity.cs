using Domain.IEntity;
using MongoDB.Bson.Serialization.Attributes;

public class BaseEntity : IBaseEntity
{
    [BsonId]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid? CreatedBy { get; set; } = null;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public Guid? UpdatedBy { get; set; } = null;

    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public bool IsDeleted { get; set; } = false;
}