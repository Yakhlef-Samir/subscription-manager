using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace subscription_Domain.Common;

public abstract class BaseEntities
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
