﻿using Microsoft.Extensions.Options;
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

        public RetrogamesRepository(IMongoClient mongoClient, IOptions<MongoDbConfigurations> mongoDbConfigurations)
        {
            _mongoClient = mongoClient;
            _mongoDbConfigurations = mongoDbConfigurations.Value;
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

            var dbRetroGames = _mongoClient.GetDatabase(_mongoDbConfigurations.Database);

            var collection = dbRetroGames.GetCollection<UserEntity>(_mongoDbConfigurations.Collections.User);
            await collection.InsertOneAsync(userEntity);
        }

        public async Task<User?> GetUserAsync(Guid id)
        {
            var dbRetroGames = _mongoClient.GetDatabase(_mongoDbConfigurations.Database);
            var collection = dbRetroGames.GetCollection<UserEntity>(_mongoDbConfigurations.Collections.User);

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