using Microsoft.AspNetCore.Http;
using RestauranteStore.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace RestauranteStore.Core.ModelViewModels
{
    public class AdminViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نوع الآدمن")]
        public AdminType AdminType { get; set; }
        public string? AdminTypeText { get; set; }
        [Display(Name = "الصورة")]
        public string? Logo { get; set; }
        [Display(Name = "تاريخ الإضافة")]
        public DateTime DateCreate { get; set; }
        public string? DateCreateText { get; set; }
        [Display(Name = "الإيميل")]
        public string? Email { get; set; }
        [Display(Name = "اسم المستخدم")]
        public string? UserName { get; set; }
        [Display(Name = "رقم الهاتف")]
        public string? PhoneNumber { get; set; }
    }
}
