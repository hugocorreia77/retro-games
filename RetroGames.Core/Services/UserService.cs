﻿using RetroGames.Core.Abstractions.Models;
using RetroGames.Core.Abstractions.Services;
using RetroGames.Data.Abstractions.Repositories;

namespace RetroGames.Core.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _usersRepository;

        public UserService(IUserRepository repository)
        {
            _usersRepository = repository;
        }

        public async Task AddUserAsync(User user)
        {
            if (string.IsNullOrEmpty(user.Username))
            {
                throw new ArgumentNullException(nameof(user.Username));
            }

            if (string.IsNullOrEmpty(user.Name))
            {
                throw new ArgumentNullException(nameof(user.Name));
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentNullException(nameof(user.Password));
            }

            var existingUser = await _usersRepository.GetUserAsync(user.UserId);
            if (existingUser != null)
            {
                throw new ArgumentException($"User {user.UserId} already exists!");
            }

            await _usersRepository.AddUserAsync(user);
        }

        public async Task<User?> GetUserAsync(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await _usersRepository.GetUserAsync(id);
        }
    }
}
