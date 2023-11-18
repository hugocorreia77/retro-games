using Microsoft.AspNetCore.Mvc;
using RetroGames.Core.Abstractions.Models;
using RetroGames.Core.Abstractions.Models.Input;
using RetroGames.Core.Abstractions.Services;

namespace RetroGames.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUserAsync(UserInput userInput)
        {
            _logger.LogInformation("Adding user...");
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Name = userInput.Name,
                Password = userInput.Password,
                Username = userInput.Username
            };

            try
            {
                await _userService.AddUserAsync(user);
            }
            catch (ArgumentNullException argNullEx)
            {
                _logger.LogError("Argument {argument} can not be null.", argNullEx.ParamName);
                return BadRequest($"Argument {argNullEx.ParamName} can not be null.");
            }
            catch (ArgumentException argExc)
            {
                return BadRequest(argExc.Message);
            }

            _logger.LogInformation("User {user} successfully added!", user.Name);
            return new ObjectResult(user) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserAsync(Guid guid)
        {
            var user = await _userService.GetUserAsync(guid);

            if(user is null)
            {
                _logger.LogError("User not found exception! Id: {id}", guid);
                return NotFound();
            }

            return Ok(user);
        }
    }
}
