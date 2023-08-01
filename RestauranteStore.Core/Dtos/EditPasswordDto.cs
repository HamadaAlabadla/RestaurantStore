namespace RestaurantStore.Core.Dtos
{
	public class EditPasswordDto
	{
		public string Id { get; set; }
		public string Password { get; set; }
		public string NewPassword { get; set; }
		public string ConfirmPassword { get; set; }
	}
}
