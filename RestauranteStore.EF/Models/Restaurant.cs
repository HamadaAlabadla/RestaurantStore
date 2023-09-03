using RestaurantStore.Core.Validation;
using System.ComponentModel.DataAnnotations;

namespace RestaurantStore.EF.Models
{
    public class Restaurant
    {
        [Required]
        [SafeText]
        public string? MainBranchName { get; set; }
        [SafeText]
        [Required]
        public string? MainBranchAddress { get; set; }
        [Key]
        [SafeText]
        public string? UserId { get; set; }
        public User? User { get; set; }
        public bool isDelete { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
