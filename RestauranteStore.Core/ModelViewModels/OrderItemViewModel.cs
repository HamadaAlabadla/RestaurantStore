namespace RestaurantStore.Core.ModelViewModels
{
    public class OrderItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public double QTYRequierd { get; set; }
        public double Price { get; set; }
        public string DateCreate { get; set; }
        public string DateModified { get; set; }
        public string OrderDate { get; set; }
    }
}
