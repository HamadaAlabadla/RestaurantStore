using Microsoft.EntityFrameworkCore;
using RestauranteStore.EF.Data;
using RestauranteStore.EF.Models;

namespace RestauranteStore.Infrastructure.Services.RestoranteService
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext dbContext;

        public RestaurantService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string? CreateRestorante(Restaurant restorante)
        {
            if (restorante == null) return null;
            dbContext.Restaurants.Add(restorante);
            dbContext.SaveChanges();
            return restorante.UserId;
        }

        public Restaurant? GetRestaurant(string id)
        {
            return dbContext.Restaurants.Include(x => x.User).FirstOrDefault(x => (x.UserId ?? "").Equals(id ?? ""));
        }
    }
}
