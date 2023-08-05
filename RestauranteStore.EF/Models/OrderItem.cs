namespace RestauranteStore.EF.Models
{
    public class OrderItem
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime DateModified { get; set; }
        public double QTY { get; set; }
        public double Price { get; set; }
        public bool isDelete { get; set; } = false;

    }
}
