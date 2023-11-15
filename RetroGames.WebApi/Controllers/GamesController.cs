using Microsoft.AspNetCore.Mvc;
using RetroGames.Core.Abstractions.Models;
using RetroGames.Core.Abstractions.Services;

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

    }
}