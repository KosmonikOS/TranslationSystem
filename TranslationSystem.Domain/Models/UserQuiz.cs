using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TranslationSystem.Domain.Models;

public class UserQuiz
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id{ get; set; }
    
    [BsonElement("user_id")]
    public long UserId{ get; set; }

    [BsonElement("next_word_id")]
    public string NextWordId{ get; set; }
}