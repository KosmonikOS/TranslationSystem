using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TranslationSystem.Domain.Models;

public class Word
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("content")]
    public string Content { get; set; }

    [BsonElement("definition")]
    public string Definition { get; set; }

    [BsonElement("translation")]
    public string Translation { get; set; }

    [BsonElement("user_id")]
    public long UserId { get; set; }
    [BsonElement("is_already_exported")]
    public bool IsAlreadyExported { get; set; }
    
}

