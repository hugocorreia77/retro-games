using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RetroGames.Core.Abstractions.Configurations;
using RetroGames.Core.Abstractions.Models;
using RetroGames.Data.Abstractions.Models;
using RetroGames.Data.Abstractions.Repositories;

namespace RetroGames.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private IMongoClient _mongoClient;
        private MongoDbConfigurations _mongoDbConfigurations;
        private IMongoDatabase _database;

        public GameRepository(IMongoClient mongoClient, IOptions<MongoDbConfigurations> mongoDbConfigurations)
        {
            _mongoClient = mongoClient;
            _mongoDbConfigurations = mongoDbConfigurations.Value;
            _database = _mongoClient.GetDatabase(_mongoDbConfigurations.Database);
        }

        public async Task AddGameAsync(Game game)
        {
            var gameEntity = new GameEntity
            {
                GameId = game.GameId,
                Name = game.Name,
                Link = game.Link,
                ProviderId = game.ProviderId,
                CommentsIds = new List<MongoDB.Bson.ObjectId>()
            };

            var collection = _database.GetCollection<GameEntity>(_mongoDbConfigurations.Collections.Game);
            await collection.InsertOneAsync(gameEntity);
        }

        public async Task<IEnumerable<Game>> GetGamesAsync()
        {
            var collection = _database.GetCollection<GameEntity>(_mongoDbConfigurations.Collections.Game);
            var entities = await collection.Find(_ => true).ToListAsync();
            return entities.Select(n => new Game { 
                GameId = n.GameId,
                Name = n.Name,
                Link = n.Link,
                ProviderId = n.ProviderId
            });
        }

        public async Task<Game?> GetGameAsync(Guid id)
        {
            var collection = _database.GetCollection<GameEntity>(_mongoDbConfigurations.Collections.Game);
            var game = await collection.Find(k => k.GameId == id).FirstOrDefaultAsync();
            return game is null ? null :
                new Game
                {
                    GameId = game.GameId,
                    Name = game.Name,
                    Link = game.Link,
                    ProviderId = game.ProviderId
                };
        }
    }
}
