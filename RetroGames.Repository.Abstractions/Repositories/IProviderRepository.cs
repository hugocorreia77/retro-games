using RetroGames.Core.Abstractions.Models;

namespace RetroGames.Data.Abstractions.Repositories
{
    public interface IProviderRepository
    {
        Task AddProviderAsync(Provider provider);
        Task<Provider?> GetProviderAsync(Guid id);
        Task<IEnumerable<Provider>> GetProvidersAsync();
    }
}