using RetroGames.Core.Abstractions.Models;

namespace RetroGames.Data.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User?> GetUserAsync(Guid id);
    }
}
