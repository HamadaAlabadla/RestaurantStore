using RestauranteStore.EF.Models;
using RestaurantStore.Core.Validation;

namespace RestaurantStore.EF.Models
{
	public class Notification
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public Order Order { get; set; }
		[SafeText]
		public string FromUserId { get; set; }
		public User FromUser { get; set; }
		[SafeText]
		public string ToUserId { get; set; }
		[SafeText]
		public string Header { get; set; }
		[SafeText]
		public string Body { get; set; }
		[SafeText]
		public bool isRead { get; set; }
		[SafeText]
		public string URL { get; set; }
		public DateTime? DateReady { get; set; }
		public DateTime DateAdded { get; set; }
	}
}
