using RetroGames.Core.Abstractions.Models;
using RetroGames.Core.Abstractions.Services;
using RetroGames.Data.Abstractions.Repositories;
using System.Xml.Linq;

namespace RetroGames.Core.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentRepository _commentsRepository;
        private readonly IUserRepository _usersRepository;
        private readonly IGameRepository _gamesRepository;

        public CommentsService(ICommentRepository commentsRepository, 
            IUserRepository userRepository, IGameRepository gameRepository)
        {
            _commentsRepository = commentsRepository;
            _usersRepository = userRepository;
            _gamesRepository = gameRepository;
        }

        public async Task AddCommentAsync(Comment comment)
        {
            if (comment is null) throw new ArgumentNullException(nameof(comment));
            if (string.IsNullOrEmpty(comment.Text)) throw new ArgumentNullException(nameof(comment.Text));
            if (comment.UserId == default) throw new ArgumentException($"{nameof(comment.UserId)} {comment.UserId} does not exists.");
            if (comment.GameId == default) throw new ArgumentException($"{nameof(comment.GameId)} {comment.GameId} does not exists.");

            _ = await _gamesRepository.GetGameAsync(comment.GameId) 
                ?? throw new ArgumentException($"{nameof(comment.GameId)} {comment.GameId} does not exists.");
            
            _ = await _usersRepository.GetUserAsync(comment.UserId) 
                ?? throw new ArgumentException($"{nameof(comment.UserId)} {comment.UserId} does not exists.");

            await _commentsRepository.AddCommentAsync(comment);
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(Guid gameId)
        {
            if (gameId == default) throw new ArgumentException($"{nameof(gameId)} {gameId} does not exists.");

            return await _commentsRepository.GetCommentsAsync(gameId);
        }
    }
}
