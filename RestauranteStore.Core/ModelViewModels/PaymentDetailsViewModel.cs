﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantStore.Core.ModelViewModels
{
	public class PaymentDetailsViewModel
	{
        public int Id { get; set; }
        public string ShippingAddress { get; set; }
		public string ShippingCity { get; set; }
	}
}