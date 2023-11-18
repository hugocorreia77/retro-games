using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RetroGames.Core.Abstractions.Models;
using RetroGames.Core.Abstractions.Models.Input;
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
        private readonly ICommentsService _commentsService;

        public GamesController(ILogger<GamesController> logger
            , IGamesService gamesService, ICommentsService commentsService)
        {
            _logger = logger;
            _gamesService = gamesService;
            _commentsService = commentsService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddGameAsync(GameInput gameInput)
        {
            _logger.LogInformation("Adding game...");
            
            var game = new Game
            {
                GameId = Guid.NewGuid(),
                Link = gameInput.Link,
                Name = gameInput.Name,
                ProviderId = gameInput.ProviderId
            };

            try
            {
                await _gamesService.AddGameAsync(game);
            }
            catch (ArgumentNullException argNullEx)
            {
                _logger.LogError("Argument {argument} can not be null.", argNullEx.ParamName);
                return BadRequest($"Argument {argNullEx.ParamName} can not be null.");
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx.Message);
                return BadRequest(argEx.Message);
            }

            _logger.LogInformation("Game {game} successfully added!", game.Name);
            return new ObjectResult(game) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet("{gameId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGameAsync(Guid gameId)
        {
            var game = await _gamesService.GetGameAsync(gameId);

            if (game is null)
            {
                _logger.LogWarning("Game not found exception! Id: {gameId}", gameId);
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


        [HttpPost("{gameId}/comments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCommentAsync(Guid gameId, CommentInput commentInput)
        {
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                GameId = gameId,
                UserId = commentInput.UserId,
                Text = commentInput.Text
            };

            try
            {
                await _commentsService.AddCommentAsync(comment);
            }
            catch (ArgumentNullException argNullEx)
            {
                _logger.LogError("Argument {argument} can not be null.", argNullEx.ParamName);
                return BadRequest($"Argument {argNullEx.ParamName} can not be null.");
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx.Message);
                return BadRequest(argEx.Message);
            }

            return new ObjectResult(comment) { StatusCode = StatusCodes.Status201Created };            
        }

        [HttpGet("{gameId}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetGameCommentAsync(Guid gameId)
        {
            var comments = await _commentsService.GetCommentsAsync(gameId);
            return Ok(comments);
        }

    }
}