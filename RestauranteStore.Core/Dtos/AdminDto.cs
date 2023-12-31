﻿using Microsoft.AspNetCore.Http;
using RestaurantStore.Core.Validation;
using System.ComponentModel.DataAnnotations;

namespace RestaurantStore.Core.Dtos
{
    public class AdminDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "اسم الآدمن مطلوب")]
        [Display(Name = "اسم الآدمن")]
        public string? Name { get; set; }

        //[Required(ErrorMessage = "إدخال الصورة مطلوب")]
        [Display(Name = "الصورة")]
        [SafeAndValidImage]
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
