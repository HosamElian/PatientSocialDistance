using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientSocialDistance.BusinessLogic.Services.IServices;
using PatientSocialDistance.Persistence.NotDbModels;

namespace PatientSocialDistance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterModel model)
        {
            //check if data completed
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //check if data correct
            var result = await _authService.RegisterAsync(model);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            //check if data completed
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //check if data correct
            var result = await _authService.GetTokenAsync(model);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {

            //check if data completed
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //check if data correct
            var result = await _authService.AddRoleAsync(model);
            if (!String.IsNullOrWhiteSpace(result))
            {
                return BadRequest(result);
            }
            return Ok(model);
        }

    }
}
