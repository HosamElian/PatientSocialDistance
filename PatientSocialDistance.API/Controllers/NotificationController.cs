using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientSocialDistance.BusinessLogic.Services.IServices;
using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.NotDbModels;

namespace PatientSocialDistance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet("GetAllByUser")]
        public async Task<IActionResult> GetAllByUser(string username) 
        {
            var result = await _notificationService.GetAllNotificationsforUserAsync(username);
            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result.Value);
        }

        [HttpPost("addNotification")]
        public async Task<IActionResult> CreateOrDeleteBlock(NotificationDTO notificationDTO)
        {
            var result = await _notificationService.AddNotification(notificationDTO);
            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result.Value);
        }

        [HttpPost("ChangeDateResponse")]
        public async Task<IActionResult> ChangeDateResult(ChangeRequestDTO request)
        {
            var result = await _notificationService.ChangeDateResultAsync(request);
            if (!result.IsCompleted) return BadRequest(result.Message);

            return Ok(result.Value);
        }
    }
}
