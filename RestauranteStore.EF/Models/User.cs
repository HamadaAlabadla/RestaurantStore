using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static RestauranteStore.Core.Enums.Enums;

namespace RestauranteStore.EF.Models
{
	public class User : IdentityUser
	{
		public Restaurant? Restaurant { get; set; }
		[Required, StringLength(50), DataType(dataType: DataType.Text)]
		public string? Name { get; set; }
		[Required]
		public string? Logo { get; set; }
		[Required]
		public UserType UserType { get; set; }
		public DateTime DateCreate { get; set; }

		public bool isDelete { get; set; }
		public ICollection<Order> Orders { get; set; } = new List<Order>();

	}
}
