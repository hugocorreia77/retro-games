using MongoDB.Bson.Serialization.Attributes;

namespace RetroGames.Core.Abstractions.Models
{
    public class Provider
    {
        [BsonId, BsonElement("_id")]
        public Guid ProviderId { get; set; }
        public string Name { get; set; }
    }
}
