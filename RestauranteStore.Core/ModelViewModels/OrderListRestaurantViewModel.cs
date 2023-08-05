namespace RestaurantStore.Core.ModelViewModels
{
    public class OrderListRestaurantViewModel
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string? SupplierImage { get; set; }
        public string StatusOrder { get; set; }
        public double TotalPrice { get; set; }
        public string DateCreate { get; set; }
        public string DateModified { get; set; }

    }
}
