using RetroGames.Core.Abstractions.Models;
using RetroGames.Core.Abstractions.Services;
using RetroGames.Data.Abstractions.Repositories;

namespace RetroGames.Core.Services
{
    public class GamesService : IGamesService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IProviderRepository _providerRepository;

        public GamesService(IGameRepository gameRepository, IProviderRepository providerRepository) 
        { 
            _gameRepository = gameRepository;
            _providerRepository = providerRepository;
        }

        public async Task AddGameAsync(Game game)
        {
            if(game == null) throw new ArgumentNullException(nameof(game));
            if(string.IsNullOrEmpty(game.Name)) throw new ArgumentNullException(nameof(game.Name));
            if(string.IsNullOrEmpty(game.Link)) throw new ArgumentNullException(nameof(game.Link));
            if(game.ProviderId == default) throw new ArgumentException($"{nameof(game.ProviderId)} not accepted");

            var provider = _providerRepository.GetProvider(game.ProviderId);
            if(provider is null)
            {
                throw new ArgumentException($"Provider {game.ProviderId} not found.");
            }

            await _gameRepository.AddGameAsync(game);
        }

        public Task<IEnumerable<Game>> GetGamesAsync()
            => _gameRepository.GetGamesAsync();

        public async Task<Game?> GetGameAsync(Guid id)
        {
            if(id == default)
            {
                throw new Exception(nameof(id));
            }

            return await _gameRepository.GetGameAsync(id);
        }
    }
}