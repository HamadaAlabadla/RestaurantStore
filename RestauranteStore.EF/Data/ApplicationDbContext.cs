using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestauranteStore.Core.Enums;
using RestauranteStore.EF.Models;

namespace RestauranteStore.EF.Data
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Restorante>()
				.HasOne(b => b.User)
				.WithOne(x => x.Restorante)
				.HasForeignKey<Restorante>(b => b.UserId);


			var userId = Guid.NewGuid().ToString();
			var roleId = Guid.NewGuid().ToString();
			var user = new User()
			{
				Id = userId,
				Email = "admin@admin.com",
				NormalizedEmail = "SUPERADMIN@ADMIN.COM",
				PhoneNumber = "0596549873",
				UserName = "admin",
				NormalizedUserName = "ADMIN",
				PasswordHash = "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==",
				UserType = UserType.admin,
				Logo = "admin - Logo01.jpg",
				Name = "Hamada",
				DateCreate = DateTime.UtcNow,

			};
			builder.Entity<User>()
				.HasData(user);


			builder.Entity<IdentityRole>()
				.HasData(new IdentityRole()
				{
					Id = roleId,
					Name = "admin",
					NormalizedName = "ADMIN"
				});
			builder.Entity<IdentityUserRole<string>>()
				.HasData(new IdentityUserRole<string>()
				{
					RoleId = roleId,
					UserId = userId
				});


		}

		public DbSet<User> users { get; set; }
		public DbSet<Restorante> Customers { get; set; }
	}
}