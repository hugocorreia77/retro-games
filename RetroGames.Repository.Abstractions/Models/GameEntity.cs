using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RetroGames.Data.Abstractions.Models
{
    public class GameEntity
    {
        [BsonId, BsonElement("_id")]
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public Guid ProviderId { get; set; }
        public List<ObjectId> CommentsIds { get; set; }
    }
}
