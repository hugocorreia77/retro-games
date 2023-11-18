using MongoDB.Bson.Serialization.Attributes;

namespace RetroGames.Core.Abstractions.Models
{
    public class Comment
    {
        [BsonId, BsonElement("_id")]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public string Text { get; set; }
    }
}
