using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
                Email = "admin@admin.com",
                NormalizedEmail = "SUPERADMIN@ADMIN.COM",
                PhoneNumber = "0596549873",
                UserName = "admin",
                NormalizedUserName = "admin",
                PasswordHash = "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==",

            };
            builder.Entity<User>()
                .HasData(user);

            builder.Entity<Admin>()
                .HasData(new Admin()
                {
                    Id = 1,
                    Name = "admin",
                    UserId = userId,
                    DateCreate = DateTime.Now,
                    Logo = "admin - Logo01.jpg"
                });

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
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}