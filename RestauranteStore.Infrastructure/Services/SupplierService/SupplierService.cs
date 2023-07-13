using RestauranteStore.EF.Data;
using RestauranteStore.EF.Models;
using System.Data.Entity;

namespace RestauranteStore.Infrastructure.Services.SupplierService
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext dbContext;

        public SupplierService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int CreateSupplier(Supplier supplier)
        {
            dbContext.Suppliers.Add(supplier);
            dbContext.SaveChanges();
            return supplier.Id;
        }

        public Supplier? DeleteSupplier(int id)
        {
            var supplier = GetSupplier(id);
            if (supplier == null) return null;
            supplier.isDelete = true;
            UpdateSupplier(supplier);
            return supplier;
        }

        public IQueryable<Supplier> GetAllSuppliers(string search)
        {
            var suppliers = dbContext.Suppliers.Include(x => x.User).Where(x =>
            string.IsNullOrEmpty(search) ? true
            : x.Name!.Contains(search)
            );
            return suppliers;
        }

        public Supplier? GetSupplier(int id)
        {
            var supplier = dbContext.Suppliers.FirstOrDefault(x => x.Id == id);
            return supplier;
        }

        public Supplier? GetSupplierByUserId(string userId)
        {
            var supplier = dbContext.Suppliers.Include(x => x.User).FirstOrDefault(x => x.UserId == userId);
            return supplier;
        }

        public int UpdateSupplier(Supplier? supplier)
        {
            if (supplier == null) return -1;
            supplier = GetSupplier(supplier.Id);
            if (supplier == null) return -1;
            dbContext.Suppliers.Add(supplier);
            dbContext.SaveChanges();
            return supplier.Id;
        }
    }
}
