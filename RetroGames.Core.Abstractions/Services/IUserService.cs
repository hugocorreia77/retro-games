using RetroGames.Core.Abstractions.Models;

namespace RetroGames.Core.Abstractions.Services
{
    public interface IUserService
    {
        Task AddUserAsync(User user);
        Task<User?> GetUserAsync(Guid id);
    }
}
