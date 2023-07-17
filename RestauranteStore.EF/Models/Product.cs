using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteStore.EF.Models
{
	public class Product
	{
		[Key]
		public int ProductNumber { get; set; }
		[Required(AllowEmptyStrings = false)]
		[StringLength(30)]
		public string Name { get; set; }
		[Required(AllowEmptyStrings = false)]
		public string Image { get; set; }
		[Required(AllowEmptyStrings = false)]
		[StringLength(100)]
		public string? Description { get; set; }
		[Required(AllowEmptyStrings = false)]
        public DateTime DateCreate { get; set; }
        [Required(AllowEmptyStrings = false)]
		public string UserId { get; set; }
		public User User { get; set; }
		public int CategoryId { get; set; }
		[DeleteBehavior(DeleteBehavior.NoAction)]
        public Category Category { get; set; }
        public int QuantityUnitId { get; set; }
        public QuantityUnit QuantityUnit { get; set; }
        public int UnitPriceId { get; set; }
        public UnitPrice UnitPrice { get; set; }
		public bool isDelete { get; set; } = false;
    }
}
