using Microsoft.AspNetCore.Mvc;
using RetroGames.Core.Abstractions.Models;
using RetroGames.Core.Abstractions.Services;
using System.Net.Mime;

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
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUserAsync(User user)
        {
            _logger.LogInformation("Adding user");

            try
            {
                await _userService.AddUserAsync(user);
            }
            catch (ArgumentNullException argNullEx)
            {
                _logger.LogWarning("Argument {argument} can not be null.", argNullEx.ParamName);
                return BadRequest($"Argument {argNullEx.ParamName} can not be null.");
            }
            catch (ArgumentException argExc)
            {
                return BadRequest(argExc.Message);
            }

            return CreatedAtAction(nameof(GetUserAsync), user.UserId);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserAsync(Guid guid)
        {
            var user = await _userService.GetUserAsync(guid);

            if(user is null)
            {
                _logger.LogWarning("User not found exception! Id: {id}", guid);
                return NotFound();
            }

            return Ok(user);
        }
    }
}
