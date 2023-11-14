﻿using RetroGames.Core.Abstractions.Models;

namespace RetroGames.Data.Abstractions.Repositories
{
    public interface IRetrogamesRepository
    {
        Task AddUserAsync(User user);
        Task<User?> GetUserAsync(Guid id);
    }
}