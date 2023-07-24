using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RestauranteStore.Core.Enums.Enums;

namespace RestaurantStore.Core.ModelViewModels
{
	public class OrderListRestaurantViewModel
	{
        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public string StatusOrder { get; set; }
        public double TotalPrice { get; set; }
        public string DateCreate { get; set; }
        public string DateModified { get; set; }

    }
}
