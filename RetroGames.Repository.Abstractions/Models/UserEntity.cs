using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RetroGames.Data.Abstractions.Models
{
    public class UserEntity
    {
        [BsonId, BsonElement("_id")]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
