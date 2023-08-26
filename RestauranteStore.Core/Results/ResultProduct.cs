using static RestauranteStore.Core.Enums.Enums;

namespace RestaurantStore.Core.Results
{
	public class ResultProduct
	{
		public Status Status { get; set; }
		public Process Process { get; set; }
		public List<string>? SKUs { get; set; } = new List<string>();
		public List<string>? SKUsFailed { get; set; } = new List<string>();
		public string? SKU { get; set; }
	}
}
