using Microsoft.AspNetCore.Mvc;
using RetroGames.Core.Abstractions.Models;
using RetroGames.Core.Abstractions.Models.Input;
using RetroGames.Core.Abstractions.Services;

namespace RetroGames.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProvidersController : ControllerBase
    {
        private readonly ILogger<ProvidersController> _logger;
        private readonly IProviderService _providerService;

        public ProvidersController(ILogger<ProvidersController> logger, IProviderService providerService)
        {
            _logger = logger;
            _providerService = providerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _providerService.GetProviders());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var provider = await _providerService.GetProvider(id);
            return provider is null ? NotFound() : Ok(provider);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(ProviderInput providerInput)
        {
            _logger.LogInformation("Adding provider...");

            var provider = new Provider
            {
                ProviderId = Guid.NewGuid(),
                Name = providerInput.Name
            };

            try
            {
                await _providerService.AddProvider(provider);
            }
            catch (ArgumentNullException argEx)
            {
                _logger.LogError("Argument {argument} can not be null!", argEx.ParamName);
                return BadRequest(argEx.Message);
            }
            catch (ArgumentException argumEx)
            {
                _logger.LogError("Provider {provider} already exists!", argumEx.ParamName);
                return BadRequest($"{argumEx.Message} {argumEx.ParamName}");
            }

            _logger.LogInformation("Provider {provider} successfully added!", provider.Name);

            return new ObjectResult(provider) { StatusCode = StatusCodes.Status201Created };
        }

    }
}
