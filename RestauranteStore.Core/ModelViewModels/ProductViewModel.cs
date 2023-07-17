using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteStore.Core.ModelViewModels
{
	public class ProductViewModel
	{
		public int ProductNumber { get; set; }
		[Required(AllowEmptyStrings = false)]
		[StringLength(30)]
		public string Name { get; set; }
		[Required(AllowEmptyStrings = false)]
		public string Image { get; set; }
		[Required(AllowEmptyStrings = false)]
		[StringLength(100)]
		public string? Description { get; set; }
        public string DateCreate { get; set; }
        [Required(AllowEmptyStrings = false)]
		public string NameSupplier { get; set; }
        public string NameCategory { get; set; }
        public string NameShortenQuantityUnit { get; set; }
        public string NameShortenUnitPrice { get; set; }
        public bool isDelete { get; set; } = false;
	}
}
