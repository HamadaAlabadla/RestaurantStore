using Microsoft.AspNetCore.Identity;

namespace RestauranteStore.EF.Models
{
    public class User : IdentityUser
    {
        public Customer? Customer { get; set; }
        public Supplier? Supplier { get; set; }
        public Admin? Admin { get; set; }

    }
}
