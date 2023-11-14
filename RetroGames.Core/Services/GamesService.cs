using RetroGames.Core.Abstractions.Models;
using RetroGames.Core.Abstractions.Services;
using RetroGames.Data.Abstractions.Repositories;

namespace RetroGames.Core.Services
{
    public class GamesService : IGamesService
    {
        private IRetrogamesRepository _retrogamesRepository;

        public GamesService(IRetrogamesRepository repository) 
        { 
            _retrogamesRepository = repository;
        }

    }
}