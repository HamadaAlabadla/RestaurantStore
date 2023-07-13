using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Core.Enums;
using RestauranteStore.Core.Error;
using RestauranteStore.Core.ModelViewModels;
using RestauranteStore.EF.Models;
using RestauranteStore.Infrastructure.Services.UserService;
using System.Linq.Dynamic.Core;

namespace RestauranteStore.Infrastructure.Services.AdminService
{
	public class AdminUserService : IAdminUserService
	{
		private readonly IAdminService adminService;
		private readonly IUserService userService;
		private readonly IMapper mapper;

		public AdminUserService(IAdminService adminService,
			IUserService userService,
			IMapper mapper)
		{
			this.adminService = adminService;
			this.userService = userService;
			this.mapper = mapper;
		}

		public async Task<ErrorModel> CreateAdminWithUser(AdminDto adminDto)
		{
			var errorModel = new ErrorModel();
			var user = mapper.Map<User>(adminDto);
			var userResult = await userService.CreateUser(user, user.UserType.ToString());
			if (string.IsNullOrEmpty(userResult))
			{
				errorModel.AddError("Error in Create user");
				return errorModel;
			}
			var admin = mapper.Map<Admin>(adminDto);
			var logoPath = await UploadFile(adminDto.Logo!, user.UserName!, "Admin");
			admin.Logo = logoPath;
			admin.DateCreate = DateTime.UtcNow;
			admin.UserId = userResult;
			var adminResulte = adminService.CreateAdmin(admin);
			if (adminResulte <= 0)
			{
				errorModel.AddError("Error in Create Admin");
				await userService.DeleteUser(userResult);
				return errorModel;
			}
			return errorModel;
		}

		private async Task<string?> UploadFile(IFormFile ufile, string userName = "", string path = "")
		{
			if (ufile != null && ufile.Length > 0)
			{
				var fileName = Path.GetFileName(ufile.FileName);
				fileName = string.Concat(userName, " - ", fileName);
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), $@"wwwroot\images\{path}", fileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await ufile.CopyToAsync(fileStream);
				}
				return fileName;
			}
			return null;
		}

		public async Task<ErrorModel> DeleteAdminWithUser(int adminId)
		{
			var errorModel = new ErrorModel();
			var admin = await GetAdminWithUserById(adminId);
			if (admin == null || string.IsNullOrEmpty(admin.UserId))
			{
				errorModel.AddError("This admin is not found !!!");
				return errorModel;
			}
			
			var userResult = await userService.DeleteUser(admin.UserId);
			if (userResult == null)
			{
				errorModel.AddError("Error in delete user");
				return errorModel;
			}
			var adminResult = adminService.DeleteAdmin(adminId);
			if (adminResult == null)
			{
				errorModel.AddError("Error in delete admin");
				return errorModel;
			}

			return errorModel;
		}

		public async Task<Admin?> GetAdminWithUserById(int adminId)
		{
			var admin = adminService.GetAdmin(adminId);
			if (admin == null || string.IsNullOrEmpty(admin.UserId))
				return null;
			admin.User = await userService.GetUserAsync(admin.UserId);
			return admin;
		}

		public async Task<object?> GetAllAdminsWithUsers(int pageLength, int skiped, StringValues searchData, StringValues sortColumn, StringValues sortDir)
		{

			var admins = adminService.GetAllAdmins(searchData[0] ?? "");
			var recordsTotal = admins.Count();

			if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortDir)))
				admins = admins.OrderBy(string.Concat(sortColumn, " ", sortDir));


			var data = admins.Skip(skiped).Take(pageLength).ToList();
			foreach (var item in data)
			{
				if (string.IsNullOrEmpty(item.UserId))
					data.Remove(item);
				item.User = await userService.GetUserAsync(item.UserId!);
			}
			var adminsViewModel = mapper.Map<IEnumerable<AdminViewModel>>(data);
			var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data = adminsViewModel };

			return jsonData;

		}

		public async Task<ErrorModel> UpdateAdminWithUser(AdminDto adminDto)
		{
			var errorModel = new ErrorModel();
			if (adminDto == null)
			{
				errorModel.AddError("Model is null");
				return errorModel;
			}
			var admin = await GetAdminWithUserById(adminDto.Id);
			if (admin == null)
			{
				errorModel.AddError("This admin is not found !!!");
				return errorModel;
			}
			var userDb = await userService.GetUserAsync(admin.UserId!);
			if (userDb == null)
			{
				errorModel.AddError("Error : user not found");
				return errorModel;
			}
			userDb.UserName = adminDto.UserName;
			userDb.NormalizedUserName = adminDto.UserName ?? "".ToUpper();
			userDb.PhoneNumber = adminDto.PhoneNumber;
			userDb.Email = adminDto.Email;
			userDb.NormalizedEmail = adminDto.Email ?? "".ToUpper();
			var userResult = await userService.UpdateUser(userDb);
			if (string.IsNullOrEmpty(userResult))
			{
				errorModel.AddError("Error in update user");
				return errorModel;
			}
			var pathLogo = admin.Logo;
			admin = mapper.Map<Admin>(adminDto);
			if (adminDto.Logo != null)
			{
				var logoPath = await UploadFile(adminDto.Logo!, userDb.UserName!, "Admin");
				admin.Logo = logoPath;
			}
			else
				admin.Logo = pathLogo;
			var adminResult = adminService.UpdateAdmin(admin);
			if (adminResult <= 0)
			{
				errorModel.AddError("Error in update admin");
				return errorModel;
			}
			return errorModel;
		}
	}
}
