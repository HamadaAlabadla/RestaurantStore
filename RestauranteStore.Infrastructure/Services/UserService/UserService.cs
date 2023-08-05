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
using RestaurantStore.Core.Dtos;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using static RestauranteStore.Core.Enums.Enums;

namespace RestauranteStore.Infrastructure.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
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
            IRestaurantService restoranteService,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _userStore = userStore;
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.fileService = fileService;
            this.restoranteService = restoranteService;
            this.signInManager = signInManager;
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

        public User? DeleteUser(string id)
        {
            var user = GetUser(id);
            if (user == null) return null;
            user.isDelete = true;
            dbContext.users.Update(user);
            dbContext.SaveChanges();
            return user;
        }

        public async Task<User?> FindByUserNameAsync(string Name)
        {
            return await userManager.FindByNameAsync(Name);
        }
        private IQueryable<User> GetAllUsers(string search, string filter)
        {
            UserType filterEnum ;
            //if (!string.IsNullOrEmpty(filter))
            //{
            //    try
            //    {
            //        filterEnum = (UserType)Enum.Parse(typeof(UserType), filter, true);
            //    }
            //    catch
            //    {
            //        filterEnum = null;
            //    }
            //}

            var users = dbContext.users.Include(x => x.Restaurant)
                .Where(x => !x.isDelete
                && (
                    (!string.IsNullOrEmpty(filter))?(Enum.TryParse(filter, out filterEnum)? x.UserType == filterEnum :false):true
                    )
                );
            return users;

        }



        public async Task<object?> GetAllUsers(int pageLength, int skiped, StringValues searchData, StringValues sortColumn, StringValues sortDir, StringValues filter)
        {

            var users = GetAllUsers(searchData[0] ?? "", filter[0] ?? "");
            var recordsTotal = users.Count();

            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDir))
                users = users.OrderBy(string.Concat(sortColumn, " ", sortDir));


            var data = users.Skip(skiped).Take(pageLength)
                .ToList().Where(x => (string.IsNullOrEmpty(searchData[0]) ? true
                                   : (
                                       (x.Name ?? "").Contains(searchData[0]!)
                                       || x.UserType.ToString().Contains(searchData[0]!)
                                   )
                               ));
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
            if (user != null)
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
            var user = GetUser(userId);
            return user;
        }

        public List<User>? GetAllSuppliersAsync()
        {
            return dbContext.users.Where(x => !x.isDelete && x.UserType == UserType.supplier).ToList();
        }

        public async Task<string?> UpdateUserDetails(UserDto? userDto)
        {
            if (userDto == null) return null;
            var user = GetUser(userDto.Id ?? "");
            if (user == null) return null;
            user.Name = $"{userDto.FirstName} {userDto.LastName}";
            user.PhoneNumber = userDto.PhoneNumber;
            if (userDto.Logo != null)
            {
                var logoPath = await fileService.UploadFile(userDto.Logo, "users", user.UserName ?? "");
                user.Logo = logoPath;
            }
            dbContext.users.Update(user);
            dbContext.SaveChanges();
            return user.Id;

        }

        public async Task<string?> UpdateEmail(EditEmailDto? editEmailDto)
        {
            if (editEmailDto == null) return null;
            var user = GetUser(editEmailDto.Id ?? "");
            if (user == null) return null;
            var result = await signInManager.CheckPasswordSignInAsync(user, editEmailDto.Password, lockoutOnFailure: false);
            if (!result.Succeeded) return null;
            user.Email = editEmailDto.Email;
            dbContext.users.Update(user);
            dbContext.SaveChanges();
            return user.Id;
        }

        public async Task<string?> UpdatePassword(EditPasswordDto? editPasswordDto)
        {
            if (editPasswordDto == null || !editPasswordDto.NewPassword.Equals(editPasswordDto.ConfirmPassword)) return null;
            var user = GetUser(editPasswordDto.Id ?? "");
            if (user == null) return null;
            var result = await userManager.ChangePasswordAsync(user, editPasswordDto.Password, editPasswordDto.NewPassword);
            if (!result.Succeeded) return null;
            return user.Id;
        }
    }
}
