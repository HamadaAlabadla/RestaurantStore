using RestaurantStore.EF.Data;
using RestaurantStore.EF.Models;

namespace RestaurantStore.Infrastructure.Services.QuantityService
{
    public class QuantityService : IQuantityService
    {
        private readonly ApplicationDbContext dbContext;
        public QuantityService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int CreateQuantity(QuantityUnit quantity)
        {
            var quantityOld = GetQuantity(quantity.Name ?? "");
            if (quantityOld != null) return -1;
            dbContext.QuantityUnits.Add(quantity);
            dbContext.SaveChanges();
            return quantity.Id;
        }

        public QuantityUnit? DeleteQuantity(int id)
        {
            var quantity = GetQuantity(id);
            if (quantity == null) return null;
            quantity.isDelete = true;
            UpdateQuantity(quantity);
            return quantity;
        }

        public QuantityUnit? DeleteQuantity(string name)
        {
            var quantity = GetQuantity(name);
            if (quantity == null) return null;
            quantity.isDelete = true;
            UpdateQuantity(quantity);
            return quantity;
        }

        public List<QuantityUnit>? GetQuantities()
        {
            return dbContext.QuantityUnits.Where(x => !x.isDelete).ToList();
        }

        public QuantityUnit? GetQuantity(int id)
        {
            return dbContext.QuantityUnits.Where(x => !x.isDelete).FirstOrDefault(x => x.Id == id);
        }

        public QuantityUnit? GetQuantity(string name)
        {
            return dbContext.QuantityUnits.Where(x => !x.isDelete).FirstOrDefault(x => (x.Name ?? "").Equals(name));
        }

        public int UpdateQuantity(QuantityUnit? quantity)
        {
            if (quantity == null) return -1;
            var quantityNew = GetQuantity(quantity.Id);
            if (quantityNew == null) return -1;
            quantityNew.Name = quantity.Name;
            quantityNew.isDelete = quantity.isDelete;
            quantityNew.shortenQuantity = quantity.shortenQuantity;
            dbContext.QuantityUnits.Update(quantityNew);
            dbContext.SaveChanges();
            return quantity.Id;
        }
    }
}
