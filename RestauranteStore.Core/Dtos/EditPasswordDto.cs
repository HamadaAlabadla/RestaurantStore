using RestaurantStore.Core.Validation;

namespace RestaurantStore.Core.Dtos
{
	public class EditPasswordDto
	{
		[SafeText]
		public string Id { get; set; }
		[SafeText]
		public string Password { get; set; }
		[SafeText]
		public string NewPassword { get; set; }
		[SafeText]
		public string ConfirmPassword { get; set; }
	}
}
