using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
    public class RecipeModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)] // Use string to store GUID as a string
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Automatically generate a GUID

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("instructions")]
        public string Instructions { get; set; }

        [BsonElement("ingredients")]
        public List<string> Ingredients { get; set; }

        [BsonElement("dateAdded")]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        [BsonElement("photo")]
        public string Photo { get; set; }
    }
}
