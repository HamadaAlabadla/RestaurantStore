using RestaurantStore.Core.Validation;

namespace RestaurantStore.Core.Dtos
{
	public class EditEmailDto
	{
		[SafeText]
		public string Id { get; set; }
		[SafeText]
		public string Email { get; set; }
		[SafeText]
		public string Password { get; set; }
	}
}
