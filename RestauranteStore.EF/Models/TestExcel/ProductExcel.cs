using System.ComponentModel.DataAnnotations;
using static RestaurantStore.Core.Enums.Enums;

namespace RestaurantStore.EF.Models.TestExcel
{
    public class ProductExcel
    {
        [Key]
        [StringLength(100)]
        public string SKU { get; set; }
        [Required]
        public int Band { get; set; }
        [Required]
        public Manufacturer Manufacturer { get; set; }
        [Required]
        public string CategoryExcelCode { get; set; }
        [Required]
        public CategoryExcel CategoryExcel { get; set; }
        [Required]
        [StringLength(250)]
        public string Description { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double ListPrice { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double MinimumDiscount { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double DiscountedPrice { get; set; }
    }
}
