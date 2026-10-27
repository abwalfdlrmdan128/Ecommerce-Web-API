using Ecommerce.Core.DTOS;
using System.Security.Claims;
using Ecommerce.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)?? throw new UnauthorizedAccessException( "User ID was not found.");
        }

        [HttpGet]
        public async Task<IActionResult> GetMyNotifications()
        {
            var userId = GetUserId();

            var notifications =await _notificationService.GetMyNotificationsAsync(userId);

            return Ok(notifications);
        }

        [HttpPatch("{notificationId:int}/read")]
        public async Task<IActionResult> MarkAsRead(int notificationId)
        {
            var userId = GetUserId();

            await _notificationService.MarkAsReadAsync( notificationId, userId);

            return NoContent();
        }

        [HttpPatch("read-all")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var userId = GetUserId();

            await _notificationService.MarkAllAsReadAsync(userId);

            return NoContent();
        }


        [HttpPost("broadcast")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Broadcast([FromBody] CreateNotificationDTO request)
        {
            var notification = new NotificationDTO
            {
                Title = request.Title,
                Message = request.Message,
                Type = request.Type
            };

            await _notificationService.SendToAllAsync(notification);

            return Ok(new
            {
                message = "Notification sent to customers."
            });
        }

    }
}
