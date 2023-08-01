using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestauranteStore.EF.Data;
using RestaurantStore.Core.ModelViewModels;
using RestaurantStore.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantStore.Infrastructure.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        public NotificationService(ApplicationDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public Notification? Create(Notification notification)
        {
            if (notification == null) return null;
            notification.DateAdded = DateTime.UtcNow;
            dbContext.Notifications.Add(notification);
            dbContext.SaveChanges();    
            return notification;
        }

        public List<NotificationViewModel>? GetAllNotifications(string userId)
        {
            var notifications = dbContext.Notifications.Where(x => x.ToUserId.Equals(userId)).OrderByDescending(x => x.DateAdded).Include(x => x.FromUser).ToList();
            var notificationsViewModel = mapper.Map<IEnumerable<NotificationViewModel>>(notifications).ToList();
            return notificationsViewModel ;
        }

		public int SetRead(int id, string userId)
		{
            var notifi = GetNotification(id);
            if(notifi == null || !notifi.ToUserId.Equals(userId)) return -1;
            notifi.isRead = true;
            dbContext.Notifications.Update(notifi); 
            dbContext.SaveChanges();
            return notifi.Id;
		}
        public Notification? GetNotification(int id)
        {
            return dbContext.Notifications.FirstOrDefault(x => x.Id == id);
        }
	}
}
