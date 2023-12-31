﻿using RetroGames.Core.Abstractions.Models;

namespace RetroGames.Core.Abstractions.Services
{
    public interface IGamesService
    {
        Task AddGameAsync(Game game);
        Task<Game?> GetGameAsync(Guid id);
        Task<IEnumerable<Game>> GetGamesAsync();
    }
}