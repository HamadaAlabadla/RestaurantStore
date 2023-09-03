using Microsoft.AspNetCore.Identity;
using RestaurantStore.Core.Validation;
using System.ComponentModel.DataAnnotations;
using static RestaurantStore.Core.Enums.Enums;

namespace RestaurantStore.EF.Models
{
    public class User : IdentityUser
    {
        public Restaurant? Restaurant { get; set; }
        [Required, StringLength(50), DataType(dataType: DataType.Text)]
        [SafeText]
        public string? Name { get; set; }

        [RegularExpression(@"^\+\d{3}\s\d{2}-\d{3}-\d{4}$", ErrorMessage = "Invalid phone number format.")]
        public string? PhoneNumber { get; set; }
        [Required]
        [SafeText]
        public string? Logo { get; set; }
        [Required]
        public UserType UserType { get; set; }
        public DateTime DateCreate { get; set; }

        public bool isDelete { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
