using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RetroGames.Core.Abstractions.Configurations;
using RetroGames.Core.Abstractions.Models;
using RetroGames.Data.Abstractions.Repositories;

namespace RetroGames.Data.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private IMongoClient _mongoClient;
        private MongoDbConfigurations _mongoDbConfigurations;
        private IMongoDatabase _database;
        private IMongoCollection<Provider> _providers;

        public ProviderRepository(IMongoClient mongoClient, IOptions<MongoDbConfigurations> mongoDbConfigurations)
        {
            _mongoClient = mongoClient;
            _mongoDbConfigurations = mongoDbConfigurations.Value;
            _database = _mongoClient.GetDatabase(_mongoDbConfigurations.Database);
            _providers = _database.GetCollection<Provider>(_mongoDbConfigurations.Collections.Provider);
        }

        public Task AddProviderAsync(Provider provider)
            => _providers.InsertOneAsync(provider);
        
        public async Task<Provider?> GetProviderAsync(Guid id)
            => (await _providers.FindAsync(z => z.ProviderId == id)).FirstOrDefault();

        public async Task<IEnumerable<Provider>> GetProvidersAsync()
            => (await _providers.FindAsync(_ => true)).ToEnumerable();
        
    }
}