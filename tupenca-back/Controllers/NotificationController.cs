using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tupenca_back.Model.Notification;
using tupenca_back.Services;

namespace tupenca_back.Controllers
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationModel notificationModel)
        {
            var result = await _notificationService.SendNotification(notificationModel);
            return Ok(result);
        }

        [Route("RegisterDeviceId")]
        [HttpPost]
        [Authorize]
        public  IActionResult RegisterDeviceId(string deviceId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _notificationService.RegisterDeviceId(int.Parse(userId),deviceId);
            return Ok();
        }
    }
}