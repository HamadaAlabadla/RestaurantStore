using Microsoft.Extensions.Primitives;
using RestauranteStore.Core.Dtos;
using RestauranteStore.EF.Models;

namespace RestauranteStore.Infrastructure.Services.UserService
{
	public interface IUserService
	{
		Task<User?> GetUserAsync(string id);
		Task<User?> FindByUserNameAsync(string Email);
		Task<object?> GetAllUsers(int pageLength, int skiped, StringValues searchData, StringValues sortColumn, StringValues sortDir , StringValues filter);

		Task<User?> DeleteUser(string id);
		Task<string?> UpdateUser(UserDto? userDto);
		Task<string?> CreateUser(UserDto userDto, string role);
		Task<string?> GetRoleByUser(string userId);

	}
}
