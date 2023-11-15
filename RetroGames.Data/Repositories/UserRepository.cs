using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RetroGames.Core.Abstractions.Configurations;
using RetroGames.Core.Abstractions.Models;
using RetroGames.Data.Abstractions.Models;
using RetroGames.Data.Abstractions.Repositories;

namespace RetroGames.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IMongoClient _mongoClient;
        private MongoDbConfigurations _mongoDbConfigurations;
        private IMongoDatabase _database;

        public UserRepository(IMongoClient mongoClient, IOptions<MongoDbConfigurations> mongoDbConfigurations)
        {
            _mongoClient = mongoClient;
            _mongoDbConfigurations = mongoDbConfigurations.Value;
            _database = _mongoClient.GetDatabase(_mongoDbConfigurations.Database);
        }

        public async Task AddUserAsync(User user)
        {
            var userEntity = new UserEntity
            {
                Name = user.Name,
                Password = user.Password,
                Username = user.Name,
                UserId = user.UserId
            };

            var collection = _database.GetCollection<UserEntity>(_mongoDbConfigurations.Collections.User);
            await collection.InsertOneAsync(userEntity);
        }

        public async Task<User?> GetUserAsync(Guid id)
        {
            var collection = _database.GetCollection<UserEntity>(_mongoDbConfigurations.Collections.User);

            var query = Builders<UserEntity>.Filter.Eq(z => z.UserId, id);
            var entity = await collection.Find(query).FirstOrDefaultAsync();

            if (entity == null)
            {
                return null;
            }

            return new User
            {
                UserId = entity.UserId,
                Name = entity.Name,
                Password = entity.Password,
                Username = entity.Username
            };
        }

    }
}
