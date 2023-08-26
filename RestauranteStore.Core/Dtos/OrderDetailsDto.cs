using System.ComponentModel.DataAnnotations;
using static RestauranteStore.Core.Enums.Enums;

namespace RestaurantStore.Core.Dtos
{
	public class OrderDetailsDto
	{
		public int Id { get; set; }

		[Required]
		public DateTime OrderDate { get; set; }
		[Required]
		public PaymentMethod PaymentMethod { get; set; }
		[Required]
		public bool IsDraft { get; set; }
	}
}
