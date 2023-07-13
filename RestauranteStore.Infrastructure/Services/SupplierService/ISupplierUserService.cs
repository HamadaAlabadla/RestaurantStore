using Microsoft.Extensions.Primitives;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Core.Error;
using RestauranteStore.EF.Models;

namespace RestauranteStore.Infrastructure.Services.SupplierService
{
    public interface ISupplierUserService
    {
        Task<ErrorModel> CreateSupplierWithUser(SupplierDto supplierDto);
        Task<object?> GetAllSupplierWithUsers(int pageLength, int skiped, StringValues searchData, StringValues sortColumn, StringValues sortDir);
        Task<ErrorModel> DeleteSupplierWithUser(int supplierId);
        Task<ErrorModel> UpdateSupplierWithUser(SupplierDto supplierDto);
        Task<Supplier?> GetSupplierWithUserById(int supplierId);
    }
}
