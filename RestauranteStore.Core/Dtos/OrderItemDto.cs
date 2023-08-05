namespace RestauranteStore.Core.Dtos
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int OrderId { get; set; }
        public string Image { get; set; }
        public double QTY { get; set; }
        public double QTYRequierd { get; set; }
        public double Price { get; set; }
        public bool isDelete { get; set; } = false;
    }
}
