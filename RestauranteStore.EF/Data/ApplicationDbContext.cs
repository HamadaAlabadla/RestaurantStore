using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestauranteStore.Core.Enums;
using RestauranteStore.EF.Models;
using static RestauranteStore.Core.Enums.Enums;

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
			builder.Entity<Category>()
				.HasData(new Category()
				{
					Id = 1,
					Name = "Meat"
				});
			builder.Entity<Category>()
				.HasData(new Category()
				{
					Id = 2,
					Name = "chickens"
				});

			builder.Entity<Category>()
				.HasData(new Category()
				{
					Id = 3,
					Name = "Dairy and Produce"
				});
			builder.Entity<Category>()
				.HasData(new Category()
				{
					Id = 4,
					Name = "Frozen Fruits and Vegetables"
				});

			builder.Entity<QuantityUnit>()
				.HasData(new QuantityUnit()
				{
					Id = 1,
					Name = "Kilogram",
					shortenQuantity = "KM"
				});
			builder.Entity<QuantityUnit>()
				.HasData(new QuantityUnit()
				{
					Id = 2,
					Name = "Gram",
					shortenQuantity = "G"
				});
			builder.Entity<QuantityUnit>()
				.HasData(new QuantityUnit()
				{
					Id = 3,
					Name = "Unit",
					shortenQuantity = "U"
				});
			
			builder.Entity<UnitPrice>()
				.HasData(new UnitPrice()
				{
					Id = 1,
					Name = "Dollar",
					ShortenName = "USD"
				});
			builder.Entity<UnitPrice>()
				.HasData(new UnitPrice()
				{
					Id = 2,
					Name = "Euro",
					ShortenName = "EUR"
				});

			builder.Entity<UnitPrice>()
				.HasData(new UnitPrice()
				{
					Id = 3,
					Name = "Yen",
					ShortenName = "JPY"
				});

			
			builder.Entity<UnitPrice>()
				.HasData(new UnitPrice()
				{
					Id = 4,
					Name = "Shekel ",
					ShortenName = "ILS"
				});


		}

		public DbSet<User> users { get; set; }
		public DbSet<Restorante> Restorantes { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<QuantityUnit> QuantityUnits { get; set; }
		public DbSet<UnitPrice> UnitsPrice { get; set; }
		public DbSet<Product> Products { get; set; }
	}
}