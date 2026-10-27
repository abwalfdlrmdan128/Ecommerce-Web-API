
using Ecommerce.Core.DTOS;
using Ecommerce.Core.Services;
using EcommerceAPI.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace EcommerceAPI.Services
{
    public class SignalRNotificationHubService:INotificationHubService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public SignalRNotificationHubService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendToUserAsync( string userId,NotificationDTO notification)
        {
            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", notification);
        }

        public async Task SendToAllAsync(NotificationDTO notification)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
        }
    }
}
