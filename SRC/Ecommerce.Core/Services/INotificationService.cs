using Ecommerce.Core.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Services
{
    public interface INotificationService
    {
        Task SendToUserAsync( string userId,NotificationDTO notification);

        Task SendToAllAsync( NotificationDTO notification);

        Task<IReadOnlyList<NotificationDTO>>GetMyNotificationsAsync(string userId);

        Task MarkAsReadAsync(int notificationId,string userId);
 
        Task MarkAllAsReadAsync(string userId);

     }
}

