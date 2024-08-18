using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Blog
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    public string Title { get; set; }
    public string Content { get; set; }
    public bool isFeatured { get; set; } = false;
    public string CoverImg { get; set; }
    public string Author { get; set; }
    public DateTime CreatedAt { get; set; }
}
