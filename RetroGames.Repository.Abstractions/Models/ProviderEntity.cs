using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RetroGames.Data.Abstractions.Models
{
    public class ProviderEntity
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public Guid ProviderId { get; set; }
        public string Name { get; set; }
    }
}
