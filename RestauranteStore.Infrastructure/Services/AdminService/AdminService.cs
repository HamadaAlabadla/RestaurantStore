using RestauranteStore.EF.Data;
using RestauranteStore.EF.Models;
using System.Data.Entity;

namespace RestauranteStore.Infrastructure.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext dbContext;

        public AdminService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
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
            admin.isDelete = true;
            dbContext.Admins.Update(admin);
            dbContext.SaveChanges();
            return admin;
        }

        public Admin? GetAdmin(int id)
        {
            return dbContext.Admins.Include(x => x.User).FirstOrDefault(x => x.Id == id);
        }

        public Admin? GetAdminByUserId(string userId)
        {
            return dbContext.Admins.Include(x => x.User).FirstOrDefault(x => x.UserId!.Equals(userId));
        }

        public IQueryable<Admin> GetAllAdmins(string search)
        {
            var admins = dbContext.Admins.Where(x => !x.isDelete);
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
            dbContext.Admins.Update(adminDb);
            dbContext.SaveChanges();
            return admin.Id;
        }
    }

}
