using System.ComponentModel.DataAnnotations;

namespace RestaurantStore.Core.ModelViewModels
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
        public double QTY { get; set; }
        public string NameShortenUnitPrice { get; set; }
        [Range(0, 100)]
        public float Rating { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public bool isDelete { get; set; } = false;
    }
}
