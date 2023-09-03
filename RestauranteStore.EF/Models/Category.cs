using RestaurantStore.Core.Validation;

namespace RestaurantStore.EF.Models
{
    public class Category
    {
        public int Id { get; set; }
        [SafeText]
        public string? Name { get; set; }
        public bool isDelete { get; set; } = false;
    }
}
