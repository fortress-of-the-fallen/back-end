using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.IEntity;

public interface IBaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    Guid Id { get; set; }

    Guid CreatedBy { get; set; }

    DateTimeOffset CreatedAt { get; set; }

    Guid UpdatedBy { get; set; }

    DateTimeOffset UpdatedAt { get; set; }
}