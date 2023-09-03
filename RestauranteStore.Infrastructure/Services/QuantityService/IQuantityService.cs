using RestaurantStore.EF.Models;

namespace RestaurantStore.Infrastructure.Services.QuantityService
{
    public interface IQuantityService
    {
        QuantityUnit? GetQuantity(int id);
        QuantityUnit? GetQuantity(string name);
        List<QuantityUnit>? GetQuantities();
        int CreateQuantity(QuantityUnit quantity);
        int UpdateQuantity(QuantityUnit? quantity);
        QuantityUnit? DeleteQuantity(int id);
        QuantityUnit? DeleteQuantity(string name);
    }
}
