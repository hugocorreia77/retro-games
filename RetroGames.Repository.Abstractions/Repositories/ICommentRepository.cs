using RetroGames.Core.Abstractions.Models;

namespace RetroGames.Data.Abstractions.Repositories
{
    public interface ICommentRepository
    {
        Task AddCommentAsync(Comment comment);
        Task<IEnumerable<Comment>> GetCommentsAsync(Guid gameId);
    }
}
