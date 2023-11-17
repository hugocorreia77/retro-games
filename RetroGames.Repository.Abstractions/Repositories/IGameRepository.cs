using RetroGames.Core.Abstractions.Models;

namespace RetroGames.Data.Abstractions.Repositories
{
    public interface IGameRepository
    {
        Task AddGameAsync(Game game);
        Task<Game?> GetGameAsync(Guid id);
        Task<IEnumerable<Game>> GetGamesAsync();
    }
}
