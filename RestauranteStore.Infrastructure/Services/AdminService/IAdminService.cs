using RestauranteStore.EF.Models;

namespace RestauranteStore.Infrastructure.Services.AdminService
{
	public interface IAdminService
	{
		Admin? GetAdmin(int id);
		Admin? GetAdminByUserId(string userId);
		IQueryable<Admin> GetAllAdmins(string search);
		int CreateAdmin(Admin admin);
		int UpdateAdmin(Admin? admin);
		Admin? DeleteAdmin(int id);

	}
}
