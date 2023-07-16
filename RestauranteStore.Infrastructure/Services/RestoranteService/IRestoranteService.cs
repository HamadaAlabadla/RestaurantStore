using RestauranteStore.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteStore.Infrastructure.Services.RestoranteService
{
	public interface IRestoranteService
	{
		string? CreateRestorante(Restorante restorante);
	}
}
