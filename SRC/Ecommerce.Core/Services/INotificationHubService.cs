using Ecommerce.Core.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Services
{
    public interface INotificationHubService
    {
      public Task SendToUserAsync(string userId,NotificationDTO notification);

       public Task SendToAllAsync(NotificationDTO notification);
    }
}

