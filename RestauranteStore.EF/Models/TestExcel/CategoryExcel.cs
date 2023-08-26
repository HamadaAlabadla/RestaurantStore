using System.ComponentModel.DataAnnotations;

namespace RestaurantStore.EF.Models.TestExcel
{
	public class CategoryExcel
	{
		[Key]
		[Required]
		[StringLength(100)]
		public string Code { get; set; }
		[Required]
		[StringLength(50)]
		public string Name { get; set; }
	}
}
