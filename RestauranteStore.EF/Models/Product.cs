using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static RestauranteStore.Core.Enums.Enums;

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
        public double QTY { get; set; }
        public int UnitPriceId { get; set; }
        public UnitPrice UnitPrice { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateAdded { get; set; }
        public double Price { get; set; }
        [Range(0, 100)]
        public float Rating { get; set; }
        public StatusProduct Status { get; set; }
        public bool isDelete { get; set; } = false;
    }
}
