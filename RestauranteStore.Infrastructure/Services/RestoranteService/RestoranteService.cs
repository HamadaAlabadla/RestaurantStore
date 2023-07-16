using RestauranteStore.EF.Data;
using RestauranteStore.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteStore.Infrastructure.Services.RestoranteService
{
	public class RestoranteService : IRestoranteService
	{
		private readonly ApplicationDbContext dbContext;

		public RestoranteService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public string? CreateRestorante(Restorante restorante)
		{
			if (restorante == null) return null;
			dbContext.Restorantes.Add(restorante);
			dbContext.SaveChanges();
			return restorante.UserId;
		}
	}
}
