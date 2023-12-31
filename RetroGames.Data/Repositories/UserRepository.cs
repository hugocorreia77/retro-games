﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RetroGames.Core.Abstractions.Configurations;
using RetroGames.Core.Abstractions.Models;
using RetroGames.Data.Abstractions.Repositories;

namespace RetroGames.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IMongoClient _mongoClient;
        private MongoDbConfigurations _mongoDbConfigurations;
        private IMongoDatabase _database;
        private IMongoCollection<User> _users;

        public UserRepository(IMongoClient mongoClient, IOptions<MongoDbConfigurations> mongoDbConfigurations)
        {
            _mongoClient = mongoClient;
            _mongoDbConfigurations = mongoDbConfigurations.Value;
            _database = _mongoClient.GetDatabase(_mongoDbConfigurations.Database);
            _users = _database.GetCollection<User>(_mongoDbConfigurations.Collections.User);
        }

        public Task AddUserAsync(User user)
            => _users.InsertOneAsync(user);

        public async Task<User?> GetUserAsync(Guid id)
            => (await _users.FindAsync(k => k.UserId == id)).FirstOrDefault();
    }
}
