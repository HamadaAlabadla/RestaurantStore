using RestauranteStore.EF.Models;

namespace RestauranteStore.Infrastructure.Services.RestoranteService
{
	public interface IRestaurantService
	{
		string? CreateRestorante(Restaurant restorante);
		Restaurant? GetRestaurant(string id);
	}
}
