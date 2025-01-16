using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace API.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)] // Use string to store GUID as a string
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Automatically generate a GUID

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
    }
}
