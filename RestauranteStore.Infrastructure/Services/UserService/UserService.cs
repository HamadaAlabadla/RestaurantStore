using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Core.Enums;
using RestauranteStore.Core.ModelViewModels;
//using Microsoft.EntityFrameworkCore;
using RestauranteStore.EF.Data;
using RestauranteStore.EF.Models;
using RestauranteStore.Infrastructure.Services.FileService;
using RestauranteStore.Infrastructure.Services.RestoranteService;
using System.Data.Entity;
using System.Linq.Dynamic.Core;
using static RestauranteStore.Core.Enums.Enums;

namespace RestauranteStore.Infrastructure.Services.UserService
{
	public class UserService : IUserService
	{
		private readonly UserManager<User> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly IUserStore<User> _userStore;
		private readonly ApplicationDbContext dbContext;
		private readonly IMapper mapper;
		private readonly IFileService fileService;
		private readonly IRestoranteService restoranteService;

		public UserService(UserManager<User> userManager,
			RoleManager<IdentityRole> roleManager,
			IUserStore<User> userStore,
			ApplicationDbContext dbContext,
			IMapper mapper,
			IFileService fileService,
			IRestoranteService restoranteService)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			_userStore = userStore;
			this.dbContext = dbContext;
			this.mapper = mapper;
			this.fileService = fileService;
			this.restoranteService = restoranteService;
		}

		public async Task<string?> CreateUser(UserDto userDto, string role)
		{
			if (userDto == null) return null;
			User? user;
			if (userDto.UserType != UserType.restorante)
				user = mapper.Map<User>(userDto);
			else
			{
				user = mapper.Map<User>(userDto.RestoranteDto);
				user.UserType = UserType.restorante;
			}

			if (user == null
				|| string.IsNullOrWhiteSpace(role))
				return null;
			role = role.ToLower();
			await _userStore.SetUserNameAsync(user, user.UserName, CancellationToken.None);
			user.Logo = await fileService.UploadFile(userDto.Logo??userDto.RestoranteDto.Logo, userDto.UserName??"");
			user.Logo ??= "";
			user.DateCreate = DateTime.UtcNow;
			var result = await userManager.CreateAsync(user, "user_123_USER");

			if (result.Succeeded)
			{
				var isExsit = await roleManager.RoleExistsAsync(role);
				if (!isExsit)
					await roleManager.CreateAsync(new IdentityRole { Name = role });
				await userManager.AddToRoleAsync(user, role);
				if(userDto.UserType == UserType.restorante)
				{
					var restorante = mapper.Map<Restorante>(userDto.RestoranteDto);
					restorante.UserId = user.Id;
					restoranteService.CreateRestorante(restorante);
				}
				return user.Id;
			}
			return null;
		}

		public async Task<User?> DeleteUser(string id)
		{
			var user = await GetUserAsync(id);
			if (user == null) return null;
			user.isDelete = true;
			await userManager.UpdateAsync(user);
			return user;
		}

		public async Task<User?> FindByUserNameAsync(string Name)
		{
			return await userManager.FindByNameAsync(Name);
		}
		private IQueryable<User> GetAllUsers(string search , string filter)
		{
			UserType? filterEnum = null ;
			if (!string.IsNullOrEmpty(filter))
			{
				filterEnum = (UserType)Enum.Parse(typeof(UserType), filter, true); 
			}
			var users = dbContext.users.Include(x => x.Restorante)
				.Where(x => !x.isDelete && (filterEnum == null)?true:x.UserType == filterEnum);
			//.Where(x => string.IsNullOrEmpty(search)
			//? true
			//: (x.AdminType.ToString().Contains(search)));
			////|| x.User!.UserName!.Contains(search)
			////|| x.User.Email!.Contains(search))
			////|| x.User.PhoneNumber!.Contains(search)

			return users;


		}

		public async Task<object?> GetAllUsers(int pageLength, int skiped, StringValues searchData, StringValues sortColumn, StringValues sortDir, StringValues filter)
		{

			var users = GetAllUsers(searchData[0] ?? "" , filter[0]??"");
			var recordsTotal = users.Count();

			if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDir))
				users = users.OrderBy(string.Concat(sortColumn, " ", sortDir));


			var data = users.Skip(skiped).Take(pageLength).ToList();
			var usersViewModel = mapper.Map<IEnumerable<UserViewModel>>(data);
			foreach (var item in usersViewModel)
			{
				item.Role = await GetRoleByUser(item.Id ?? "");
			}
			var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data = usersViewModel };

			return jsonData;

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

		public async Task<string?> UpdateUser(UserDto? userDto)
		{
			var user = mapper.Map<User>(userDto);
			if (user == null || string.IsNullOrEmpty(user.Id)) return null;
			var userDb = await GetUserAsync(user.Id);
			if (userDb == null) return null;
			userDb.UserName = user.UserName;
			userDb.NormalizedUserName = user.NormalizedUserName;
			userDb.Email = user.Email;
			userDb.NormalizedEmail = user.NormalizedEmail;
			userDb.PhoneNumber = user.PhoneNumber;
			userDb.UserType = user.UserType;
			await userManager.UpdateAsync(userDb);
			return user.Id;
		}
	}
}
