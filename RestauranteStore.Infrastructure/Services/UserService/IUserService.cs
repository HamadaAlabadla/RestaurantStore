using RestauranteStore.EF.Models;

namespace RestauranteStore.Infrastructure.Services.UserService
{
    public interface IUserService
    {
        Task<User?> GetUserAsync(string id);
        Task<User?> FindByUserNameAsync(string Email);
        Task<List<User>?> GetAllUsersAsync();
        Task<User?> DeleteUser(string id);
        Task<string?> UpdateUser(User? user);
        Task<string?> CreateUser(User user, string role);
        Task<string?> GetRoleByUser(string userId);

    }
}
