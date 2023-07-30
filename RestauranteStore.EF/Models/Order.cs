using System.ComponentModel.DataAnnotations;
using static RestauranteStore.Core.Enums.Enums;

namespace RestauranteStore.EF.Models
{
	public class Order
	{
		public int Id { get; set; }
        public string SupplierId { get; set; }
		public User Supplier { get; set; }
		public string RestaurantId { get; set; }
		public Restaurant Restaurant { get; set; }
		public double TotalPrice { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime DateCreate { get; set; }
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime OrderDate { get; set; }
		public DateTime DateModified { get; set; }
		public string ShippingAddress { get; set; }
		public string ShippingCity { get; set; }
		public StatusOrder StatusOrder { get; set; }
		public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
		public bool isDelete { get; set; } = false;
	}
}
