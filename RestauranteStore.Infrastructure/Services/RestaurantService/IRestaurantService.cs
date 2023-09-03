using RestaurantStore.EF.Models;

namespace RestaurantStore.Infrastructure.Services.RestaurantService
{
    public interface IRestaurantService
    {
        string? CreateRestorante(Restaurant restorante);
        Restaurant? GetRestaurant(string id);
    }
}
