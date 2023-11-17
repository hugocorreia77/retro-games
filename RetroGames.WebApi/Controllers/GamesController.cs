using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RetroGames.Core.Abstractions.Models;
using RetroGames.Core.Abstractions.Services;
using RetroGames.Core.Services;

namespace RetroGames.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {        
        private readonly ILogger<GamesController> _logger;
        private readonly IGamesService _gamesService;

        public GamesController(ILogger<GamesController> logger, IGamesService gamesService)
        {
            _logger = logger;
            _gamesService = gamesService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddGameAsync(Game game)
        {
            _logger.LogInformation("Adding game");

            try
            {
                await _gamesService.AddGameAsync(game);
            }
            catch (ArgumentNullException argNullEx)
            {
                _logger.LogWarning("Argument {argument} can not be null.", argNullEx.ParamName);
                return BadRequest($"Argument {argNullEx.ParamName} can not be null.");
            }
            catch (ArgumentException argEx)
            {
                _logger.LogWarning(argEx.Message);
                return BadRequest(argEx.Message);
            }

            return new ObjectResult(game) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet("{guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGameAsync(Guid guid)
        {
            var game = await _gamesService.GetGameAsync(guid);

            if (game is null)
            {
                _logger.LogWarning("Game not found exception! Id: {id}", guid);
                return NotFound();
            }

            return Ok(game);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGamesAsync()
        {
            var games = await _gamesService.GetGamesAsync();
            return Ok(games);
        }

    }
}