using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using RestaurantStore.Core.Dtos;
using RestaurantStore.EF.Models;

namespace RestaurantStore.Infrastructure.Services.UserService
{
    public interface IUserService
    {
        User? GetUser(string id);
        User? GetUserByContext(HttpContext context);
        Task<User?> FindByUserNameAsync(string Email);
        Task<object?> GetAllUsers(int pageLength, int skiped, StringValues searchData, StringValues sortColumn, StringValues sortDir, StringValues filter);
        List<User>? GetAllSuppliersAsync();
        User? DeleteUser(string id);
        Task<string?> UpdateUser(UserDto? userDto);
        Task<string?> CreateUser(UserDto userDto, string role);
        Task<string?> GetRoleByUser(string userId);
        Task<string?> UpdateUserDetails(UserDto? userDto);
        Task<string?> UpdateEmail(EditEmailDto? editEmailDto);
        Task<string?> UpdatePassword(EditPasswordDto? editPasswordDto);

    }
}
