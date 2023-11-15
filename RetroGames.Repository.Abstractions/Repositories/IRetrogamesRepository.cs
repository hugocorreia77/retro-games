using RetroGames.Core.Abstractions.Models;

namespace RetroGames.Data.Abstractions.Repositories
{
    public interface IRetrogamesRepository
    {
        Task AddProvider(Provider provider);
        Task<Provider?> GetProvider(Guid id);
        Task<IEnumerable<Provider>> GetProviders();
    }
}