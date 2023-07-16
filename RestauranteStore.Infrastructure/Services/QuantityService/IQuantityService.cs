using RestauranteStore.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteStore.Infrastructure.Services.QuantityService
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
