﻿using Microsoft.AspNetCore.Mvc;
using RetroGames.Core.Abstractions.Models;
using RetroGames.Core.Abstractions.Services;

namespace RetroGames.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProvidersController : ControllerBase
    {
        private readonly ILogger<ProvidersController> _logger;
        private readonly IGamesService _gamesService;

        public ProvidersController(ILogger<ProvidersController> logger, IGamesService gamesService)
        {
            _logger = logger;
            _gamesService = gamesService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _gamesService.GetProviders());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var provider = await _gamesService.GetProvider(id);
            return provider is null ? NotFound() : Ok(provider);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(Provider provider)
        {
            try
            {
                await _gamesService.AddProvider(provider);
            }
            catch (ArgumentNullException argEx)
            {
                _logger.LogWarning("Argument {argument} can not be null!", argEx.ParamName);
                return BadRequest(argEx.Message);
            }
            catch (ArgumentException argumEx)
            {
                _logger.LogWarning("Provider already exists!", argumEx.ParamName);
                return BadRequest(argumEx.Message);
            }

            return new ObjectResult(provider) { StatusCode = StatusCodes.Status201Created };
        }

    }
}
