using RestauranteStore.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteStore.Infrastructure.Services.UnitPriceService
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
