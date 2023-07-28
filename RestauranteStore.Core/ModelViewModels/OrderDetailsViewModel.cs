using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantStore.Core.ModelViewModels
{
	public class OrderDetailsViewModel
	{
        public string DateAdded { get; set; }
        public string PaymentMethod { get; set; }
        public string StatusOrder { get; set; }
    }
}
