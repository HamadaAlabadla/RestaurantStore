using RestaurantStore.EF.Data;
using RestaurantStore.EF.Models;

namespace RestaurantStore.Infrastructure.Services.UnitPriceService
{
    public class UnitPriceService : IUnitPriceService
    {
        private readonly ApplicationDbContext dbContext;
        public UnitPriceService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int CreateUnitPrice(UnitPrice unitPrice)
        {
            var unitPriceOld = GetUnitPrice(unitPrice.Name ?? "");
            if (unitPriceOld != null) return -1;
            dbContext.UnitsPrice.Add(unitPrice);
            dbContext.SaveChanges();
            return unitPrice.Id;
        }

        public UnitPrice? DeleteUnitPrice(int id)
        {
            var unitPrice = GetUnitPrice(id);
            if (unitPrice == null) return null;
            unitPrice.isDelete = true;
            UpdateUnitPrice(unitPrice);
            return unitPrice;
        }

        public UnitPrice? DeleteUnitPrice(string name)
        {
            var unitPrice = GetUnitPrice(name);
            if (unitPrice == null) return null;
            unitPrice.isDelete = true;
            UpdateUnitPrice(unitPrice);
            return unitPrice;
        }

        public UnitPrice? GetUnitPrice(int id)
        {
            return dbContext.UnitsPrice.Where(x => !x.isDelete).FirstOrDefault(x => x.Id == id);
        }

        public UnitPrice? GetUnitPrice(string name)
        {

            return dbContext.UnitsPrice.Where(x => !x.isDelete).FirstOrDefault(x => (x.Name ?? "").Equals(name));
        }

        public List<UnitPrice>? GetUnitsPrice()
        {
            return dbContext.UnitsPrice.Where(x => !x.isDelete).ToList();
        }

        public int UpdateUnitPrice(UnitPrice? unitPrice)
        {
            if (unitPrice == null) return -1;
            var unitPriceNew = GetUnitPrice(unitPrice.Id);
            if (unitPriceNew == null) return -1;
            unitPriceNew.isDelete = unitPrice.isDelete;
            unitPriceNew.Name = unitPrice.Name;
            unitPriceNew.ShortenName = unitPrice.ShortenName;
            dbContext.UnitsPrice.Update(unitPriceNew);
            dbContext.SaveChanges();
            return unitPrice.Id;
        }
    }
}
