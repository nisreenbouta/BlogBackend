using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Subscriber{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }=true;
    public DateTime SubscribedAt { get; set; }
    
}