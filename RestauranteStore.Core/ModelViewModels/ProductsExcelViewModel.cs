using System.ComponentModel.DataAnnotations;

namespace RestaurantStore.Core.ModelViewModels
{
    public class ProductsExcelViewModel
    {
        [StringLength(100)]
        public string SKU { get; set; }
        [Required]
        public int Band { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string CategoryExcel { get; set; }
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
