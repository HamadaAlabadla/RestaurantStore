using RestauranteStore.EF.Models;

namespace RestauranteStore.Infrastructure.Services.SupplierService
{
	public interface ISupplierService
	{
		Supplier? GetSupplier(int id);
		Supplier? GetSupplierByUserId(string userId);
		IQueryable<Supplier> GetAllSuppliers(string search);
		int CreateSupplier(Supplier supplier);
		int UpdateSupplier(Supplier? supplier);
		Supplier? DeleteSupplier(int id);

	}
}
