using RetroGames.Core.Abstractions.Models;

namespace RetroGames.Core.Abstractions.Services
{
    public interface ICommentsService
    {
        Task AddCommentAsync(Comment comment);
        Task<IEnumerable<Comment>> GetCommentsAsync(Guid gameId);
    }
}
