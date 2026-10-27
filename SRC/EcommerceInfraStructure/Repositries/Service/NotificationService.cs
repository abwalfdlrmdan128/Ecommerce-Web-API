
using Ecommerce.Core.DTOS;
using Ecommerce.Core.Models;
using Ecommerce.Core.Services;
using EcommerceInfraStructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace EcommerceInfraStructure.Repositories.Service
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _context;
        private readonly INotificationHubService _hubService;

        public NotificationService( AppDbContext context,INotificationHubService hubService)
        {
            _context = context;
            _hubService = hubService;
        }

        public async Task SendToUserAsync( string userId, NotificationDTO notification)
        {
            var entity = new Notification
            {
                UserId = userId,
                Title = notification.Title,
                Message = notification.Message,
                Type = notification.Type,
                RelatedEntityId = notification.RelatedEntityId,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(entity);
            await _context.SaveChangesAsync();

            notification.Id = entity.Id;
            notification.IsRead = entity.IsRead;
            notification.CreatedAt = entity.CreatedAt;

            await _hubService.SendToUserAsync(userId, notification);
        }

        public async Task SendToAllAsync(NotificationDTO notification)
        {
            var userIds = await ( from user in _context.Users
                 join userRole in _context.UserRoles
                   on user.Id equals userRole.UserId
                 join role in _context.Roles
                   on userRole.RoleId equals role.Id
                 where role.Name == "Customer"
                  select user.Id ).Distinct().ToListAsync();

            var entities = userIds.Select(userId => new Notification
            {
                UserId = userId,
                Title = notification.Title,
                Message = notification.Message,
                Type = notification.Type,
                RelatedEntityId = notification.RelatedEntityId,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            if (entities.Count == 0) return;

            _context.Notifications.AddRange(entities);
            await _context.SaveChangesAsync();

            foreach (var entity in entities)
            {
                var dto = new NotificationDTO
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Message = entity.Message,
                    Type = entity.Type,
                    RelatedEntityId = entity.RelatedEntityId,
                    IsRead = entity.IsRead,
                    CreatedAt = entity.CreatedAt
                };

                await _hubService.SendToUserAsync(entity.UserId, dto);
            }
        }

        public async Task<IReadOnlyList<NotificationDTO>>GetMyNotificationsAsync(string userId)
        {
            return await _context.Notifications.AsNoTracking().Where(n => n.UserId == userId).OrderByDescending(n => n.CreatedAt).Select(n => new NotificationDTO
                {
                    Id = n.Id,
                    Title = n.Title,
                    Message = n.Message,
                    Type = n.Type,
                    RelatedEntityId = n.RelatedEntityId,
                    IsRead = n.IsRead,
                    CreatedAt = n.CreatedAt
                }).ToListAsync();
        }

        public async Task MarkAsReadAsync(int notificationId, string userId)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(n =>n.Id == notificationId && n.UserId == userId);

            if (notification is null) return;

            notification.IsRead = true;
            await _context.SaveChangesAsync();
        }

        public async Task MarkAllAsReadAsync(string userId)
        {
            var notifications = await _context.Notifications.Where(n => n.UserId == userId && !n.IsRead).ToListAsync();

            foreach (var notification in notifications) notification.IsRead = true;

            await _context.SaveChangesAsync();
        }
    }
}
