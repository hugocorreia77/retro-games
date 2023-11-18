using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RetroGames.Core.Abstractions.Configurations;
using RetroGames.Core.Abstractions.Models;
using RetroGames.Data.Abstractions.Repositories;

namespace RetroGames.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private IMongoClient _mongoClient;
        private MongoDbConfigurations _mongoDbConfigurations;
        private IMongoDatabase _database;
        private IMongoCollection<Game> _games;

        public GameRepository(IMongoClient mongoClient, IOptions<MongoDbConfigurations> mongoDbConfigurations)
        {
            _mongoClient = mongoClient;
            _mongoDbConfigurations = mongoDbConfigurations.Value;
            _database = _mongoClient.GetDatabase(_mongoDbConfigurations.Database);
            _games = _database.GetCollection<Game>(_mongoDbConfigurations.Collections.Game);
        }

        public Task AddGameAsync(Game game)
            => _games.InsertOneAsync(game);
        
        public async Task<IEnumerable<Game>> GetGamesAsync()
            => (await _games.FindAsync(_ => true)).ToEnumerable();
        
        public async Task<Game?> GetGameAsync(Guid id)
            => (await _games.FindAsync(k => k.GameId == id)).FirstOrDefault();
    }
}
