using System.ComponentModel.DataAnnotations;

namespace RestauranteStore.EF.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        [Required, StringLength(50), DataType(dataType: DataType.Text)]
        public string? Name { get; set; }
        [Required]
        public string? Logo { get; set; }
        public DateTime DateCreate { get; set; }

        [Required]
        public string? UserId { get; set; }
        public User? User { get; set; }
        public bool isDelete { get; set; }

    }
}
