using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RetroGames.Core.Abstractions.Configurations;
using RetroGames.Core.Abstractions.Models;
using RetroGames.Data.Abstractions.Repositories;

namespace RetroGames.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private IMongoClient _mongoClient;
        private MongoDbConfigurations _mongoDbConfigurations;
        private IMongoDatabase _database;
        private IMongoCollection<Comment> _comments;

        public CommentRepository(IMongoClient mongoClient, IOptions<MongoDbConfigurations> mongoDbConfigurations)
        {
            _mongoClient = mongoClient;
            _mongoDbConfigurations = mongoDbConfigurations.Value;
            _database = _mongoClient.GetDatabase(_mongoDbConfigurations.Database);
            _comments = _database.GetCollection<Comment>(_mongoDbConfigurations.Collections.Comment);
        }

        public Task AddCommentAsync(Comment comment)
            => _comments.InsertOneAsync(comment);
        
        public async Task<IEnumerable<Comment>> GetCommentsAsync(Guid gameId)
            =>  (await _comments.FindAsync(z => z.GameId == gameId)).ToEnumerable();
    }
}
