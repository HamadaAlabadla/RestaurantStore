using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static RestauranteStore.Core.Enums.Enums;

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
        public IFormFile? Image { get; set; }
        public string? ImageTitle { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string? Description { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        //[DeleteBehavior(DeleteBehavior.NoAction)]
        public int QuantityUnitId { get; set; }
        public int UnitPriceId { get; set; }
        public double QTY { get; set; } = 0;
        public double Price { get; set; } = 0;
        public StatusProduct Status { get; set; }
        public bool isDelete { get; set; } = false;
    }
}
