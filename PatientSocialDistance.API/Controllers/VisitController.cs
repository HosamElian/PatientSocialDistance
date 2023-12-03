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

        [HttpPost("GetAllVisits")]
        public async Task<IActionResult> GetGetAllApprovedAll(GetVisitRequest request)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _visitService.GetAllVisits(request);

            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result.Value);
        }

        [HttpPost("GetAllVisitsByDate")] 
        public async Task<IActionResult> GetAllVisitsByDate(GetVisitByDateRequest request)
            {

            if (!ModelState.IsValid) return BadRequest(ModelState);


            var result = await _visitService.GetVisitsByDate(request);

            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result.Value);
        }


        [HttpPost("ReserveVisit")] 
        public async Task<IActionResult> CreateVisit([FromBody] VisitDto model)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _visitService.CreateVisit(model);

            if (!result.IsCompleted)
            {
                if((int) result.Value == 403) return Forbid(result.Message);
                if((int)result.Value == 400) return BadRequest(result.Message);
            } 
                

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
