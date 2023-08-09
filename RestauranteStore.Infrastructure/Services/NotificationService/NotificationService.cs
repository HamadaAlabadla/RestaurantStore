using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RestauranteStore.EF.Data;
using RestaurantStore.Core.ModelViewModels;
using RestaurantStore.EF.Models;
using RestaurantStore.Infrastructure.Hubs;

namespace RestaurantStore.Infrastructure.Services.NotificationService
{
    public class NotificationService : INotificationService, IToastNotification
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IHubContext<NotificationHub> hubContext;
        private readonly IMapper mapper;
        public NotificationService(ApplicationDbContext dbContext,
            IMapper mapper,
            IHubContext<NotificationHub> hubContext)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.hubContext = hubContext;
        }
        public async Task<Notification?> Create(Notification? notification)
        {
            if (notification == null) return null;
            notification.DateAdded = DateTime.UtcNow;
            dbContext.Notifications.Add(notification);
            dbContext.SaveChanges();
            notification = GetNotification(notification.Id);
            if (notification != null)
            {
                var notifiViewModel = mapper.Map<NotificationViewModel>(notification);
                await hubContext.Clients.Group(notification.ToUserId).SendAsync("ReceiveNotification", notifiViewModel);
            }
            return notification;
        }

        public List<NotificationViewModel>? GetAllNotifications(string userId)
        {
            var notifications = dbContext.Notifications.Where(x => x.ToUserId.Equals(userId)).OrderByDescending(x => x.DateAdded).Include(x => x.FromUser).ToList();
            var notificationsViewModel = mapper.Map<IEnumerable<NotificationViewModel>>(notifications).ToList();
            return notificationsViewModel;
        }

        public int SetRead(int id, string userId)
        {
            var notifi = GetNotification(id);
            if (notifi == null || !notifi.ToUserId.Equals(userId)) return -1;
            notifi.isRead = true;
            notifi.DateReady = DateTime.UtcNow;
            dbContext.Notifications.Update(notifi);
            dbContext.SaveChanges();
            return notifi.Id;
        }
        public Notification? GetNotification(int id)
        {
            return dbContext.Notifications.Include(x => x.FromUser).Include(x => x.Order).FirstOrDefault(x => x.Id == id);
        }
    }
}