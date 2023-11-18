using MongoDB.Bson.Serialization.Attributes;

namespace RetroGames.Core.Abstractions.Models
{
    public class User
    {
        [BsonId, BsonElement("_id")]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public User()
        {
            UserId = default;
            Username = string.Empty;
            Password = string.Empty;
            Name = string.Empty;
        }

        public User(Guid userid, string username, string password, string name)
        {
            UserId = userid;
            Username = username;
            Password = password;
            Name = name;
        }

    }
}
