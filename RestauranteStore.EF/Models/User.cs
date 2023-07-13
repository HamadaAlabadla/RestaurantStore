using Microsoft.AspNetCore.Identity;
using RestauranteStore.Core.Enums;

namespace RestauranteStore.EF.Models
{
	public class User : IdentityUser
	{
		public Customer? Customer { get; set; }
		public Supplier? Supplier { get; set; }
		public Admin? Admin { get; set; }
        public UserType UserType { get; set; }
        public bool isDelete { get; set; }

	}
}
