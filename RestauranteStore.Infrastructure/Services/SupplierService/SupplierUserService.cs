using Microsoft.Extensions.Primitives;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Core.Error;
using RestauranteStore.EF.Models;
using RestauranteStore.Infrastructure.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteStore.Infrastructure.Services.SupplierService
{
    public class SupplierUserService : ISupplierUserService
    {
        private readonly ISupplierService supplierService;
        private readonly IUserService userService;

        public SupplierUserService(ISupplierService supplierService, IUserService userService)
        {
            this.supplierService = supplierService;
            this.userService = userService;
        }

        public Task<ErrorModel> CreateSupplierWithUser(SupplierDto supplierDto)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorModel> DeleteSupplierWithUser(int supplierId)
        {
            throw new NotImplementedException();
        }

        public Task<object?> GetAllSupplierWithUsers(int pageLength, int skiped, StringValues searchData, StringValues sortColumn, StringValues sortDir)
        {
            throw new NotImplementedException();
        }

        public Task<Supplier?> GetSupplierWithUserById(int supplierId)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorModel> UpdateSupplierWithUser(SupplierDto supplierDto)
        {
            throw new NotImplementedException();
        }
    }
}
