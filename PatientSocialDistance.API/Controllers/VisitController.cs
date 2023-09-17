using Microsoft.AspNetCore.Mvc;
using PatientSocialDistance.BusinessLogic.Services.IServices;
using PatientSocialDistance.Persistence.DTOs;

namespace PatientSocialDistance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitController : ControllerBase
    {
        private readonly IVisitService _visitService;

        public VisitController(IVisitService visitService)
        {
            _visitService = visitService;
        }
        [HttpPost("ReserveVisit")]
        public IActionResult CreateVisit(VisitDto model)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _visitService.CreateVisit(model);
            
            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("VisitApproval")]
        public async Task<IActionResult> VisitApproval(VisitApprovalDto model)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _visitService.VisitApproval(model);

            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string userId)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _visitService.GetAllVisits(userId);

            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result.Value);
        }

    }
}
