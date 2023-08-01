using RestauranteStore.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantStore.EF.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string FromUserId { get; set; }
        public User FromUser { get; set; }
        public string ToUserId { get; set; }
        public string Header { get; set; } 
        public string Body { get; set; }
        public bool isRead { get; set; }
        public DateTime? DateReady { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
