using RetroGames.Core.Abstractions.Models;

namespace RetroGames.Core.Abstractions.Services
{
    public interface IGamesService
    {
        Task AddProvider(Provider provider);
        Task<Provider?> GetProvider(Guid id);
        Task<IEnumerable<Provider>> GetProviders();

    }
}