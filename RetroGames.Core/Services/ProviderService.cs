﻿using RetroGames.Core.Abstractions.Models;
using RetroGames.Core.Abstractions.Services;
using RetroGames.Data.Abstractions.Repositories;

namespace RetroGames.Core.Services
{
    public class ProviderService : IProviderService
    {

        private IProviderRepository _retrogamesRepository;

        public ProviderService(IProviderRepository repository)
        {
            _retrogamesRepository = repository;
        }

        public async Task AddProvider(Provider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (string.IsNullOrEmpty(provider.Name))
            {
                throw new ArgumentNullException(nameof(provider.Name));
            }

            if (provider.ProviderId == default)
            {
                throw new ArgumentNullException(nameof(provider.ProviderId));
            }

            var exists = await _retrogamesRepository.GetProviderAsync(provider.ProviderId);

            if (exists != null)
            {
                throw new ArgumentException("Provider already exists!", nameof(provider.ProviderId));
            }

            await _retrogamesRepository.AddProviderAsync(provider);
        }

        public Task<Provider?> GetProvider(Guid id)
            => _retrogamesRepository.GetProviderAsync(id);

        public Task<IEnumerable<Provider>> GetProviders()
            => _retrogamesRepository.GetProvidersAsync();
    }
}
