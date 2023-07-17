using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteStore.Core.Dtos
{
	public class ProductDto
	{
		[Key]
		public int ProductNumber { get; set; }
		[Required(AllowEmptyStrings = false)]
		[StringLength(30)]
		public string Name { get; set; }
		[Required(AllowEmptyStrings = false)]
		public IFormFile Image { get; set; }
		[Required(AllowEmptyStrings = false)]
		[StringLength(100)]
		public string? Description { get; set; }
		[Required(AllowEmptyStrings = false)]
        public DateTime DateCreate { get; set; }
        [Required(AllowEmptyStrings = false)]
		public string UserId { get; set; }
		public int CategoryId { get; set; }
		//[DeleteBehavior(DeleteBehavior.NoAction)]
		public int QuantityUnitId { get; set; }
		public int UnitPriceId { get; set; }
		public bool isDelete { get; set; } = false;
	}
}
