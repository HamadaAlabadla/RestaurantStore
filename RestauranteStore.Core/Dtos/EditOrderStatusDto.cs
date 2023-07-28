using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using static RestauranteStore.Core.Enums.Enums;

namespace RestaurantStore.Core.Dtos
{
	public class EditOrderStatusDto
	{
		public int Id { get; set; }
        public StatusOrder StatusOrder { get; set; }
        public List<SelectListItem> StatusOrders { get; set; } 
    }
}
