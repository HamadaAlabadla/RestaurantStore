using RestauranteStore.EF.Data;
using RestauranteStore.EF.Models;
using RestauranteStore.Infrastructure.Services.UserService;
using System.Data.Entity;

namespace RestauranteStore.Infrastructure.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUserService userService;


        public AdminService(ApplicationDbContext dbContext,
            IUserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }

        public int CreateAdmin(Admin admin)
        {
            if (admin == null) return -1;
            admin.DateCreate = DateTime.UtcNow;
            dbContext.Admins.Add(admin);
            dbContext.SaveChanges();
            return admin.Id;
        }

        public Admin? DeleteAdmin(int id)
        {
            var admin = GetAdmin(id);
            if (admin == null) return null;
            dbContext.Admins.Remove(admin);
            dbContext.SaveChanges();
            return admin;
        }

        public Admin? GetAdmin(int id)
        {
            return dbContext.Admins.Include(x => x.User).FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Admin> GetAllAdmins(string search)
        {
            var admins = dbContext.Admins;
                //.Where(x => string.IsNullOrEmpty(search)
                //? true
                //: (x.AdminType.ToString().Contains(search)));
                ////|| x.User!.UserName!.Contains(search)
                ////|| x.User.Email!.Contains(search))
                ////|| x.User.PhoneNumber!.Contains(search)
                
            return admins;
        }

        public int UpdateAdmin(Admin? admin)
        {
            if (admin == null) return -1;
            var adminDb = GetAdmin(admin.Id);
            if (adminDb == null) return -1;
            adminDb.Logo = admin.Logo;
            adminDb.AdminType = admin.AdminType;
            dbContext.Admins.Update(adminDb);
            dbContext.SaveChanges();
            return admin.Id;
        }
    }

}
