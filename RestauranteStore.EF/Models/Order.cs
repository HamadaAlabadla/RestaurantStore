using RestaurantStore.Core.Validation;
using System.ComponentModel.DataAnnotations;
using static RestaurantStore.Core.Enums.Enums;

namespace RestaurantStore.EF.Models
{
    public class Order
    {
        public int Id { get; set; }
        [SafeText]
        public string SupplierId { get; set; }
        public User Supplier { get; set; }
        [SafeText]
        public string RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public double TotalPrice { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }
        public DateTime DateModified { get; set; }
        [SafeText]
        public string ShippingAddress { get; set; }
        [SafeText]
        public string ShippingCity { get; set; }
        public StatusOrder StatusOrder { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public bool isDelete { get; set; } = false;
    }
}
