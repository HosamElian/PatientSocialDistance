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
        [HttpGet]
        public async Task<IActionResult> GetAllInteraction(string username)
        {
            if (String.IsNullOrWhiteSpace(username)) return BadRequest(ModelState);

            var result = await _interactionService.GetAllAsync(username);

            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result.Value);

        }


        [HttpGet("SetData")]
        public IActionResult SetData(string name)
        {
            var names = name.Split(',');

           var result = _interactionService.Add(names);
            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok();
        }

        [HttpGet("duration")]
        public IActionResult Duration(string name)
        {
            var names = name.Split(',');
            var result = _interactionService.returnDuration(names);
            
            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result.Value);
        }


    }
 
}
