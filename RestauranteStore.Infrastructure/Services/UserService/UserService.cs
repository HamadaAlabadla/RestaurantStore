using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestauranteStore.Core.Enums;
using RestauranteStore.EF.Models;
using RestauranteStore.Infrastructure.Services.AdminService;

namespace RestauranteStore.Infrastructure.Services.UserService
{
	public class UserService : IUserService
	{
		private readonly UserManager<User> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly IUserStore<User> _userStore;

		public UserService(UserManager<User> userManager,
			RoleManager<IdentityRole> roleManager,
			IUserStore<User> userStore,
			IAdminService adminService)
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
			role = role.ToLower();
			await _userStore.SetUserNameAsync(user, user.UserName, CancellationToken.None);
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
			if (user.UserType == UserType.SuperAdmin)
			{
				user.isDelete = false;
				return null;
			}
			else
				user.isDelete = true;
			await userManager.UpdateAsync(user);
			return user;
		}

		public async Task<User?> FindByUserNameAsync(string Name)
		{
			return await userManager.FindByNameAsync(Name);
		}

		public async Task<List<User>?> GetAllUsersAsync()
		{
			return await userManager.Users.Where(x => !x.isDelete).ToListAsync();
		}

		public async Task<string?> GetRoleByUser(string userId)
		{
			var user = await GetUserAsync(userId); 
			if (user == null) return null;
			var role = (await userManager.GetRolesAsync(user))[0];
			return role;
		}

		public async Task<User?> GetUserAsync(string id)
		{
			var user = await userManager.FindByIdAsync(id);
			return user;
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
