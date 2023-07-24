namespace RestauranteStore.EF.Models
{
	public class QuantityUnit
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? shortenQuantity { get; set; }
		public bool isDelete { get; set; } = false;
	}
}
