using Microsoft.AspNetCore.Http;
using RestauranteStore.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RestauranteStore.Core.Dtos
{
    public class AdminDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "نوع الآدمن مطلوب")]
        [Display(Name = "نوع الآدمن")]
        public AdminType AdminType { get; set; }
        //[Required(ErrorMessage = "إدخال الصورة مطلوب")]
        [Display(Name = "الصورة")]
        public IFormFile? Logo { get; set; }
        [Required(ErrorMessage = "إدخال الإيميل مطلوب")]
        [Display(Name = "الإيميل")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "إدخال اسم المستخدم مطلوب")]
        [Display(Name = "اسم المستخدم")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "إدخال رقم الهاتف مطلوب")]
        [Display(Name = "رقم الهاتف")]
        public string? PhoneNumber { get; set; }
    }
}
