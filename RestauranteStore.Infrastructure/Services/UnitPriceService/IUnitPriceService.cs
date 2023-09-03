using RestaurantStore.EF.Models;

namespace RestaurantStore.Infrastructure.Services.UnitPriceService
{
    public interface IUnitPriceService
    {
        UnitPrice? GetUnitPrice(int id);
        UnitPrice? GetUnitPrice(string name);
        List<UnitPrice>? GetUnitsPrice();
        int CreateUnitPrice(UnitPrice unitPrice);
        int UpdateUnitPrice(UnitPrice? unitPrice);
        UnitPrice? DeleteUnitPrice(int id);
        UnitPrice? DeleteUnitPrice(string name);
    }
}
