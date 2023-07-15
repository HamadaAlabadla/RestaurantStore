using Microsoft.AspNetCore.Identity;
using RestauranteStore.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace RestauranteStore.EF.Models
{
	public class User : IdentityUser
	{
		public Restorante? Restorante { get; set; }
		[Required, StringLength(50), DataType(dataType: DataType.Text)]
		public string? Name { get; set; }
		[Required]
		public string? Logo { get; set; }
		[Required]
		public UserType UserType { get; set; }
		public DateTime DateCreate { get; set; }

		public bool isDelete { get; set; }

	}
}
