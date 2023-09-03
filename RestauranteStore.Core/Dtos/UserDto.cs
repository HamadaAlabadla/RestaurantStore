using Microsoft.AspNetCore.Http;
using RestaurantStore.Core.Validation;
using System.ComponentModel.DataAnnotations;
using static RestaurantStore.Core.Enums.Enums;

namespace RestaurantStore.Core.Dtos
{
    public class UserDto
    {
        [SafeText]
        public string? Id { get; set; }
        [Required(ErrorMessage = "اسم الآدمن مطلوب")]
        [Display(Name = "اسم الآدمن")]
        [SafeText]
        public string? Name { get; set; }
        [Required(ErrorMessage = "اسم الآدمن مطلوب")]
        [Display(Name = "اسم الآدمن")]
        [SafeText]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "اسم الآدمن مطلوب")]
        [Display(Name = "اسم الآدمن")]
        [SafeText]
        public string? LastName { get; set; }

        //[Required(ErrorMessage = "إدخال الصورة مطلوب")]
        [Display(Name = "الصورة")]
        [SafeAndValidImage]
        public IFormFile? Logo { get; set; }
        [SafeText]
        public string? image { get; set; }
        [Required(ErrorMessage = "إدخال الإيميل مطلوب")]
        [Display(Name = "الإيميل")]
        [SafeText]
        public string? Email { get; set; }
        [Required(ErrorMessage = "إدخال اسم المستخدم مطلوب")]
        [Display(Name = "اسم المستخدم")]
        [SafeText]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "إدخال رقم الهاتف مطلوب")]
        [Display(Name = "رقم الهاتف")]
        [RegularExpression(@"^(?:(?:\+?972|\(\+?972\)|\+?\(972\))?[ .-]?((?:59|56))|(?:\+?970|\(\+?970\)|\+?\(970\))?[ .-]?\d{2})(?:[ .-]?(?:\d{4}|\d{2}))$", ErrorMessage = "Invalid phone number format.")]
        public string? PhoneNumber { get; set; }
        [Required]
        public UserType UserType { get; set; }
        [Required(ErrorMessage = "إدخال اسم الفرع الرئيسي مطلوب")]
        [Display(Name = "اسم الفرع الرئيسي")]
        [SafeText]
        public string? MainBranchName { get; set; }
        [Required(ErrorMessage = "إدخال عنوان الفرع الرئيسي مطلوب")]
        [Display(Name = "عنوان الفرع الرئيسي")]
        [SafeText]
        public string? MainBranchAddress { get; set; }
        [SafeText]
        public string? filter { get; set; }
        [SafeText]
        public string? Password { get; set; }
        [SafeText]
        public string? ConfirmPassword { get; set; }
    }
}
