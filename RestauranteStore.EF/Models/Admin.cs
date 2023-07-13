using RestauranteStore.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace RestauranteStore.EF.Models
{
	public class Admin
	{
		public int Id { get; set; }
		[Required]
		public string? UserId { get; set; }
		public string? Logo { get; set; }
		public User? User { get; set; }
		public DateTime DateCreate { get; set; } = DateTime.UtcNow;
		public bool isDelete { get; set; }

	}
}
