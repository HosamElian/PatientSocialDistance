using Microsoft.AspNetCore.Mvc;
using PatientSocialDistance.BusinessLogic.Services.IServices;
using PatientSocialDistance.Persistence;
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

        [HttpGet("GetAllApprovedVisits")]
        public async Task<IActionResult> GetGetAllApprovedAll(string username)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _visitService.GetAllVisits(username);

            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result.Value);
        }
        
        [HttpGet("GetAllNotApprovedVisits")] //
        public async Task<IActionResult> GetAllNotApproved(string username)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _visitService.GetAllVisits(username, false);

            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result.Value);
        }
        [HttpGet("GetAllNotApprovedByDate")]
        public async Task<IActionResult> GetAllNotApprovedByDate(string username, string date)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _visitService.GetAllVisits(username, false);

            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result.Value);
        }
        [HttpGet("GetAllApprovedByDate")] //
        public async Task<IActionResult> GetAllApprovedByDate(string username, string date)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            DateOnly visitDate = DateOnly.FromDateTime( DateTime.Parse(date));
            

            var result = await _visitService.GetVisitsByDate(username, visitDate);

            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result.Value);
        }


        [HttpPost("ReserveVisit")] //
        public IActionResult CreateVisit([FromBody] VisitDto model)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _visitService.CreateVisit(model);

            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("VisitApproval")] //
        public async Task<IActionResult> VisitApproval(VisitApprovalDto model)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _visitService.VisitApproval(model);

            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result);
        }

    }
}
