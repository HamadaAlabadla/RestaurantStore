using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestauranteStore.EF.Models;

namespace RestauranteStore.Infrastructure.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserStore<User> _userStore;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
            IUserStore<User> userStore)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _userStore = userStore;
        }

        public async Task<string?> CreateUser(User user, string role)
        {
            if (user == null
                || string.IsNullOrWhiteSpace(role))
                return null;

            await _userStore.SetUserNameAsync(user, user.Email, CancellationToken.None);
            var result = await userManager.CreateAsync(user, "user_123_USER");

            if (result.Succeeded)
            {
                var isExsit = await roleManager.RoleExistsAsync(role);
                if (!isExsit)
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                await userManager.AddToRoleAsync(user, role);
                return user.Id;
            }
            return null;
        }

        public async Task<User?> DeleteUser(string id)
        {
            var user = await GetUserAsync(id);
            if (user == null) return null;
            await userManager.DeleteAsync(user);
            return user;
        }

        public async Task<List<User>?> GetAllUsersAsync()
        {
            return await userManager.Users.ToListAsync();
        }

        public async Task<User?> GetUserAsync(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task<string?> UpdateUser(User? user)
        {
            if (user == null || string.IsNullOrEmpty(user.Id)) return null;
            var userDb = await GetUserAsync(user.Id);
            if (userDb == null) return null;
            userDb.UserName = user.UserName;
            userDb.NormalizedUserName = user.NormalizedUserName;
            userDb.Email = user.Email;
            userDb.NormalizedEmail = user.NormalizedEmail;
            userDb.PhoneNumber = user.PhoneNumber;
            await userManager.UpdateAsync(userDb);
            return user.Id;
        }
    }
}
