using Domain.IEntity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class BaseEntity : IBaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}