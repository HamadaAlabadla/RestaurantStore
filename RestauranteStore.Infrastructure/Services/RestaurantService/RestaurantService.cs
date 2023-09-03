using Microsoft.EntityFrameworkCore;
using RestaurantStore.EF.Data;
using RestaurantStore.EF.Models;

namespace RestaurantStore.Infrastructure.Services.RestaurantService
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
