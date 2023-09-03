using RestaurantStore.Core.Validation;

namespace RestaurantStore.EF.Models
{
    public class UnitPrice
    {
        public int Id { get; set; }
        [SafeText]
        public string? Name { get; set; }
        [SafeText]
        public string? ShortenName { get; set; }
        public bool isDelete { get; set; } = false;
    }
}
