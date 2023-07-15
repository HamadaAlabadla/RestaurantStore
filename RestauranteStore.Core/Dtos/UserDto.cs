﻿using Microsoft.AspNetCore.Http;
using RestauranteStore.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace RestauranteStore.Core.Dtos
{
	public class UserDto
	{
		public string? Id { get; set; }
		[Required(ErrorMessage = "اسم الآدمن مطلوب")]
		[Display(Name = "اسم الآدمن")]
		public string? Name { get; set; }

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
		[Required]
		public UserType UserType { get; set; }
		public RestoranteDto? RestoranteDto { get; set; }
	}
}