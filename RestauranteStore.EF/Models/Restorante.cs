using System.ComponentModel.DataAnnotations;

namespace RestauranteStore.EF.Models
{
	public class Restorante
	{
		[Required]
		public string? MainBranchName { get; set; }
		[Required]
		public string? MainBranchAddress { get; set; }
		[Key]
		public string? UserId { get; set; }
		public User? User { get; set; }
		public bool isDelete { get; set; }

	}
}
