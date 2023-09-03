using System.ComponentModel.DataAnnotations;

namespace RestaurantStore.Core.ModelViewModels
{
    public class SupplierViewModel
    {
        public int Id { get; set; }
        [Display(Name = "اسم البائع")]
        public string? Name { get; set; }

        [Display(Name = "الإيميل")]
        public string? Email { get; set; }
        [Display(Name = "اسم المستخدم")]
        public string? UserName { get; set; }
        [Display(Name = "رقم الهاتف")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "تاريخ الإضافة")]
        public DateTime DateCreate { get; set; }
        public string? DateCreateText { get; set; }

        [Display(Name = "الصورة")]
        public string? Logo { get; set; }
        [Display(Name = "نشط")]
        public bool isDelete { get; set; }
    }
}
