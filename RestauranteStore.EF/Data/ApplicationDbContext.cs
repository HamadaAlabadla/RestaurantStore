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

            builder.Entity<Customer>()
                .HasOne(b => b.User)
                .WithOne(x => x.Customer)
                .HasForeignKey<Customer>(b => b.UserId);

            builder.Entity<Supplier>()
                .HasOne(b => b.User)
                .WithOne(x => x.Supplier)
                .HasForeignKey<Supplier>(b => b.UserId);

            builder.Entity<User>()
                .HasOne(b => b.Admin)
                .WithOne(x => x.User)
                .HasForeignKey<Admin>(b => b.UserId);


            var userId = Guid.NewGuid().ToString();
            var roleId = Guid.NewGuid().ToString();
            var user = new User()
            {
                Id = userId,
                Email = "superadmin@admin.com",
                NormalizedEmail = "SUPERADMIN@ADMIN.COM",
                PhoneNumber = "0596549873",
                UserName = "superadmin",
                NormalizedUserName = "superadmin",
                PasswordHash = "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==",

            };
            builder.Entity<User>()
                .HasData(user);

            builder.Entity<Admin>()
                .HasData(new Admin()
                {
                    Id = 1,
                    AdminType = AdminType.SuperAdmin,
                    UserId = userId,
                    DateCreate = DateTime.Now,
                    Logo = "superadmin - Logo01"
                });

            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole()
                {
                    Id = roleId,
                    Name = "superadmin",
                    NormalizedName = "SUPERADMIN"
                });
            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>()
                {
                    RoleId = roleId,
                    UserId = userId
                });


        }

        public DbSet<User> users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}