using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Core.ModelViewModels;
//using Microsoft.EntityFrameworkCore;
using RestauranteStore.EF.Data;
using RestauranteStore.EF.Models;
using RestauranteStore.Infrastructure.Services.FileService;
using RestauranteStore.Infrastructure.Services.RestoranteService;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
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
		private readonly IRestaurantService restoranteService;

		public UserService(UserManager<User> userManager,
			RoleManager<IdentityRole> roleManager,
			IUserStore<User> userStore,
			ApplicationDbContext dbContext,
			IMapper mapper,
			IFileService fileService,
			IRestaurantService restoranteService)
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
			if (userDto.UserType != UserType.restaurant)
				user = mapper.Map<User>(userDto);
			else
			{
				user = mapper.Map<User>(userDto.RestoranteDto);
				user.UserType = UserType.restaurant;
			}

			if (user == null
				|| string.IsNullOrWhiteSpace(role))
				return null;
			role = role.ToLower();
			await _userStore.SetUserNameAsync(user, user.UserName, CancellationToken.None);
			user.Logo = await fileService.UploadFile(userDto.Logo ?? userDto.RestoranteDto!.Logo!, "users", userDto.UserName ?? "");
			user.Logo ??= "";
			user.DateCreate = DateTime.UtcNow;
			var result = await userManager.CreateAsync(user, "user_123_USER");

			if (result.Succeeded)
			{
				var isExsit = await roleManager.RoleExistsAsync(role);
				if (!isExsit)
					await roleManager.CreateAsync(new IdentityRole { Name = role });
				await userManager.AddToRoleAsync(user, role);
				if (userDto.UserType == UserType.restaurant)
				{
					var restorante = mapper.Map<Restaurant>(userDto.RestoranteDto);
					restorante.UserId = user.Id;
					restoranteService.CreateRestorante(restorante);
				}
				return user.Id;
			}
			return null;
		}

		public async Task<User?> DeleteUser(string id)
		{
			var user = GetUser(id);
			if (user == null) return null;
			user.isDelete = true;
			await userManager.UpdateAsync(user);
			return user;
		}

		public async Task<User?> FindByUserNameAsync(string Name)
		{
			return await userManager.FindByNameAsync(Name);
		}
		private IQueryable<User> GetAllUsers(string search, string filter)
		{
			UserType? filterEnum = null;
			if (!string.IsNullOrEmpty(filter))
			{
				filterEnum = (UserType)Enum.Parse(typeof(UserType), filter, true);
			}
			var users = dbContext.users.Include(x => x.Restaurant)
				.Where(x => !x.isDelete 
				&&( (filterEnum == null) ? true : 
					x.UserType == filterEnum
				)
				&&( string.IsNullOrEmpty(search)?true 
							: (
								(x.Name??"").Contains(search)
							)
						)
				);
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

			var users = GetAllUsers(searchData[0] ?? "", filter[0] ?? "");
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
			var user = GetUser(userId);
			if (user == null) return null;
			var role = (await userManager.GetRolesAsync(user))[0];
			return role;
		}

		public User? GetUser(string id)
		{
			var user = dbContext.users.Include(x => x.Restaurant).FirstOrDefault(x => x.Id.Equals(id));
			if(user != null)
				user.Restaurant = restoranteService.GetRestaurant(id);
			return user;
		}

		public async Task<string?> UpdateUser(UserDto? userDto)
		{
			if (userDto == null) return null;
			var user = mapper.Map<User>(userDto);
			if (user == null || string.IsNullOrEmpty(user.Id)) return null;
			var userDb = GetUser(user.Id);
			if (userDb == null) return null;
			if (userDto.Logo != null)
			{
				var logoPath = await fileService.UploadFile(userDto.Logo, "users", user.UserName ?? "");
				userDb.Logo = logoPath;
			}
			userDb.UserName = user.UserName;
			userDb.Name = user.Name;
			userDb.NormalizedUserName = user.NormalizedUserName;
			userDb.Email = user.Email;
			userDb.NormalizedEmail = user.NormalizedEmail;
			userDb.PhoneNumber = user.PhoneNumber;
			userDb.UserType = user.UserType;
			await userManager.UpdateAsync(userDb);
			return user.Id;
		}

		public User? GetUserByContext(HttpContext context)
		{
			var userContext = context.User;
			var userId = "";
			if (userContext.Identity!.IsAuthenticated)
			{
				userId = userContext.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			}
			if (string.IsNullOrEmpty(userId)) return null;
			var user =GetUser(userId);
			return user;
		}

		public List<User>? GetAllSuppliersAsync()
		{
			return  dbContext.users.Where(x => !x.isDelete && x.UserType == UserType.supplier).ToList(); 
		}
	}
}
