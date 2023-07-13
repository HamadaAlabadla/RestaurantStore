using Microsoft.Extensions.Primitives;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Core.Error;
using RestauranteStore.EF.Models;

namespace RestauranteStore.Infrastructure.Services.AdminService
{
	public interface IAdminUserService
	{
		Task<ErrorModel> CreateAdminWithUser(AdminDto adminDto);
		Task<object?> GetAllAdminsWithUsers(int pageLength, int skiped, StringValues searchData, StringValues sortColumn, StringValues sortDir);
		Task<ErrorModel> DeleteAdminWithUser(int adminId);
		Task<ErrorModel> UpdateAdminWithUser(AdminDto adminDto);
		Task<Admin?> GetAdminWithUserById(int adminId);
	}
}
