using AutoMapper;
using Microsoft.Extensions.Primitives;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Core.Error;
using RestauranteStore.Core.ModelViewModels;
using RestauranteStore.EF.Models;
using RestauranteStore.Infrastructure.Services.FileService;
using RestauranteStore.Infrastructure.Services.UserService;
using System.Linq.Dynamic.Core;

namespace RestauranteStore.Infrastructure.Services.SupplierService
{
	public class SupplierUserService : ISupplierUserService
	{
		private readonly ISupplierService supplierService;
		private readonly IUserService userService;
		private readonly IMapper mapper;
		private readonly IFileService fileService;

		public SupplierUserService(ISupplierService supplierService
			, IUserService userService,
			IMapper mapper,
			IFileService fileService)
		{
			this.supplierService = supplierService;
			this.userService = userService;
			this.mapper = mapper;
			this.fileService = fileService;
		}

		public async Task<ErrorModel> CreateSupplierWithUser(SupplierDto supplierDto)
		{
			var errorModel = new ErrorModel();
			var user = mapper.Map<User>(supplierDto);
			var userResult = await userService.CreateUser(user, "Supplier");
			if (string.IsNullOrEmpty(userResult))
			{
				errorModel.AddError("Error in Create User");
				return errorModel;
			}
			var supplier = mapper.Map<Supplier>(supplierDto);
			var filePath = await fileService.UploadFile(supplierDto.Logo!, user.UserName ?? "");
			supplier.Logo = filePath;
			var supplierResult = supplierService.CreateSupplier(supplier);
			if (supplierResult <= 0)
			{
				await userService.DeleteUser(userResult);
				errorModel.AddError("Error in Create Supplier");
				return errorModel;
			}
			return errorModel;
		}

		public async Task<ErrorModel> DeleteSupplierWithUser(int supplierId)
		{
			var errorModel = new ErrorModel();
			var supplier = await GetSupplierWithUserById(supplierId);
			if (supplier == null || string.IsNullOrEmpty(supplier.UserId))
			{
				errorModel.AddError("This supplier is not found !!!");
				return errorModel;
			}

			var userResult = await userService.DeleteUser(supplier.UserId);
			if (userResult == null)
			{
				errorModel.AddError("Error in delete user");
				return errorModel;
			}
			var supplierResult = supplierService.DeleteSupplier(supplierId);
			if (supplierResult == null)
			{
				errorModel.AddError("Error in delete supplier");
				return errorModel;
			}

			return errorModel;
		}

		public async Task<object?> GetAllSupplierWithUsers(int pageLength, int skiped, StringValues searchData, StringValues sortColumn, StringValues sortDir)
		{
			var suppliers = supplierService.GetAllSuppliers(searchData[0] ?? "");
			var recordsTotal = suppliers.Count();

			if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortDir)))
				suppliers = suppliers.OrderBy(string.Concat(sortColumn, " ", sortDir));


			var data = suppliers.Skip(skiped).Take(pageLength).ToList();
			foreach (var item in data)
			{
				if (string.IsNullOrEmpty(item.UserId))
					data.Remove(item);
				item.User = await userService.GetUserAsync(item.UserId!);
			}
			var suppliersViewModel = mapper.Map<IEnumerable<SupplierViewModel>>(data);
			var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data = suppliersViewModel };

			return jsonData;
		}

		public async Task<Supplier?> GetSupplierWithUserById(int supplierId)
		{
			var supplier = supplierService.GetSupplier(supplierId);
			if (supplier == null || string.IsNullOrEmpty(supplier.UserId))
				return null;
			supplier.User = await userService.GetUserAsync(supplier.UserId);
			return supplier;
		}

		public async Task<ErrorModel> UpdateSupplierWithUser(SupplierDto supplierDto)
		{
			var errorModel = new ErrorModel();
			if (supplierDto == null)
			{
				errorModel.AddError("Model is null");
				return errorModel;
			}
			var supplier = await GetSupplierWithUserById(supplierDto.Id);
			if (supplier == null)
			{
				errorModel.AddError("This supplier is not found !!!");
				return errorModel;
			}
			var userDb = await userService.GetUserAsync(supplier.UserId!);
			if (userDb == null)
			{
				errorModel.AddError("Error : user not found");
				return errorModel;
			}
			userDb.UserName = supplierDto.UserName;
			userDb.NormalizedUserName = supplierDto.UserName ?? "".ToUpper();
			userDb.PhoneNumber = supplierDto.PhoneNumber;
			userDb.Email = supplierDto.Email;
			userDb.NormalizedEmail = supplierDto.Email ?? "".ToUpper();
			var userResult = await userService.UpdateUser(userDb);
			if (string.IsNullOrEmpty(userResult))
			{
				errorModel.AddError("Error in update user");
				return errorModel;
			}
			var pathLogo = supplier.Logo;
			supplier = mapper.Map<Supplier>(supplierDto);
			if (supplierDto.Logo != null)
			{
				var logoPath = await fileService.UploadFile(supplierDto.Logo!, userDb.UserName!);
				supplier.Logo = logoPath;
			}
			else
				supplier.Logo = pathLogo;
			var adminResult = supplierService.UpdateSupplier(supplier);
			if (adminResult <= 0)
			{
				errorModel.AddError("Error in update supplier");
				return errorModel;
			}
			return errorModel;
		}
	}
}
