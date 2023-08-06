﻿using RestauranteStore.EF.Models;

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
        public string URL { get; set; }
        public DateTime? DateReady { get; set; }
        public DateTime DateAdded { get; set; }
    }
}