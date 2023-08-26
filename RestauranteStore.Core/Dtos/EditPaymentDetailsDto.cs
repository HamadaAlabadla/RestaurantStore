using RestaurantStore.Core.Validation;

namespace RestaurantStore.Core.Dtos
{
	public class EditPaymentDetailsDto
	{
		public int Id { get; set; }
		[SafeText]
		public string ShippingAddress { get; set; }
		[SafeText]
		public string ShippingCity { get; set; }
	}
}
