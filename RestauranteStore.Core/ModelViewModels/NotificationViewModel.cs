using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantStore.Core.ModelViewModels
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string FromUserId { get; set; }
        public string FromUserImage { get; set; }
        public string FromUserName { get; set; }
        public string ToUserId { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public bool isRead { get; set; }
        public string DateReady { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
