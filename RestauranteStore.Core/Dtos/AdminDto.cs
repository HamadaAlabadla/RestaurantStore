﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RestauranteStore.Core.Dtos
{
    public class AdminDto
    {
        public int Id { get; set; }
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
