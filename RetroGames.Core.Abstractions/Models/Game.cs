﻿using MongoDB.Bson.Serialization.Attributes;

namespace RetroGames.Core.Abstractions.Models
{
    public class Game
    {
        [BsonId, BsonElement("_id")]
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public Guid ProviderId { get; set; }
    }
}
