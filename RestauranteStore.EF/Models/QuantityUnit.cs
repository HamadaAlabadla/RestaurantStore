using RestaurantStore.Core.Validation;

namespace RestauranteStore.EF.Models
{
	public class QuantityUnit
	{
		public int Id { get; set; }
		[SafeText]
		public string? Name { get; set; }
		[SafeText]
		public string? shortenQuantity { get; set; }
		public bool isDelete { get; set; } = false;
	}
}
