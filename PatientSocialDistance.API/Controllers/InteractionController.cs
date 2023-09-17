using Microsoft.AspNetCore.Mvc;
using PatientSocialDistance.BusinessLogic.Services.IServices;
using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.NotDbModels;

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

        [HttpPost("AddInteraction")]
        public IActionResult AddInteraction([FromBody] InteractionDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _interactionService.Add(model);
            
            if (!result.IsCompleted) return BadRequest(result.Message);
            
            return Ok(result.Value);
        }

        [HttpGet("GetAllInteraction")]
        public async Task<IActionResult> GetAllInteraction(string userId)
        {
            if(String.IsNullOrWhiteSpace(userId)) return BadRequest(ModelState);
            
            var result = await _interactionService.GetAllAsync(userId);
            
            if (!result.IsCompleted) return BadRequest(result.Message);
            
            return Ok(result.Value);

        }

    }
}
