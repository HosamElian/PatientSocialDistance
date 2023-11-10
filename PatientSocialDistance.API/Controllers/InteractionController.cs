using Microsoft.AspNetCore.Mvc;
using PatientSocialDistance.BusinessLogic.Services.IServices;
using PatientSocialDistance.Persistence.DTOs;

namespace PatientSocialDistance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteractionController : ControllerBase
    {
        private readonly IInteractionService _interactionService;

        public InteractionController(IInteractionService interactionService)
        {
            _interactionService = interactionService;
        }

        [HttpGet("GetAllInteraction")]
        public async Task<IActionResult> GetAllInteraction(string username)
        {
            if (String.IsNullOrWhiteSpace(username)) return BadRequest(ModelState);

            var result = await _interactionService.GetAllAsync(username);

            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result.Value);

        }

        [HttpPost("AddInteraction")]
        public IActionResult AddInteraction([FromBody] InteractionDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _interactionService.Add(model);

            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result.Value);
        }

        [HttpGet("AddInteraction2")]
        public IActionResult AddInteraction2()
        {

            return Ok("Hosam");
        }

    }
}
