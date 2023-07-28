using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantStore.Core.Dtos
{
	public class EditPaymentDetailsDto
	{
        public int Id { get; set; }
		public string ShippingAddress { get; set; }
		public string ShippingCity { get; set; }
	}
}
