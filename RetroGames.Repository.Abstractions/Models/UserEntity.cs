using MongoDB.Bson;
using RetroGames.Core.Abstractions.Models;

namespace RetroGames.Data.Abstractions.Models
{
    public class UserEntity : User
    {
        public ObjectId _id { get; set; }
    }
}
