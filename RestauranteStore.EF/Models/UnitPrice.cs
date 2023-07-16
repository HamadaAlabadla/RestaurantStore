using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteStore.EF.Models
{
	public class UnitPrice
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ShortenName { get; set; }
        public bool isDelete { get; set; } = false;
    }
}
