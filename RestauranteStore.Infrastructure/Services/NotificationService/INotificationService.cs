using RestaurantStore.Core.ModelViewModels;
using RestaurantStore.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantStore.Infrastructure.Services.NotificationService
{
    public interface INotificationService
    {
        Notification? Create(Notification notification);
        List<NotificationViewModel>? GetAllNotifications(string userId);
        int SetRead(int id , string userId);
    }
}
