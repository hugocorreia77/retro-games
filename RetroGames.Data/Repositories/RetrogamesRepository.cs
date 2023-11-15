using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RetroGames.Core.Abstractions.Configurations;
using RetroGames.Core.Abstractions.Models;
using RetroGames.Data.Abstractions.Models;
using RetroGames.Data.Abstractions.Repositories;

namespace RetroGames.Data.Repositories
{
    public class RetrogamesRepository : IRetrogamesRepository
    {
        private IMongoClient _mongoClient;
        private MongoDbConfigurations _mongoDbConfigurations;
        private IMongoDatabase _database;

        public RetrogamesRepository(IMongoClient mongoClient, IOptions<MongoDbConfigurations> mongoDbConfigurations)
        {
            _mongoClient = mongoClient;
            _mongoDbConfigurations = mongoDbConfigurations.Value;
            _database = _mongoClient.GetDatabase(_mongoDbConfigurations.Database);
        }

        public async Task AddProvider(Provider provider)
        {
            var providerEntity = new ProviderEntity
            {
                Name = provider.Name,
                ProviderId = provider.ProviderId
            };

            var collection = _database.GetCollection<ProviderEntity>(_mongoDbConfigurations.Collections.Provider);
            await collection.InsertOneAsync(providerEntity);
        }

        public async Task<Provider?> GetProvider(Guid id)
        {
            var collection = _database.GetCollection<ProviderEntity>(_mongoDbConfigurations.Collections.Provider);
            var query = Builders<ProviderEntity>.Filter.Eq(z => z.ProviderId, id);
            var entity = await collection.Find(query).FirstOrDefaultAsync();

            return entity is null ? null : new Provider
            {
                Name = entity.Name,
                ProviderId = entity.ProviderId,
            };
        }

        public async Task<IEnumerable<Provider>> GetProviders()
        {
            var collection = _database.GetCollection<ProviderEntity>(_mongoDbConfigurations.Collections.Provider);
            var entities = await collection.Find(_ => true).ToListAsync();
            return entities.Select(n => new Provider { Name = n.Name, ProviderId = n.ProviderId });
        }
    }
}