using RestaurantStore.Core.Validation;

namespace RestaurantStore.Core.Dtos
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        [SafeText]
        public string ProductName { get; set; }
        [SafeText]
        public string ProductImage { get; set; }
        [SafeText]
        public string SupplierId { get; set; }
        [SafeText]
        public string SupplierName { get; set; }
        public int OrderId { get; set; }
        [SafeText]
        public string Image { get; set; }
        public double QTY { get; set; }
        public double QTYRequierd { get; set; }
        public double Price { get; set; }
        public bool isDelete { get; set; } = false;
    }
}
