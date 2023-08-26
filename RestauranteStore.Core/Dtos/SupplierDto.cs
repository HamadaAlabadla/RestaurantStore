using Microsoft.AspNetCore.Http;
using RestaurantStore.Core.Validation;
using System.ComponentModel.DataAnnotations;

namespace RestauranteStore.Core.Dtos
{
	public class SupplierDto
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "اسم البائع مطلوب")]
		[Display(Name = "اسم البائع")]
		[SafeText]
		public string? Name { get; set; }

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
		[SafeText]
		public string? PhoneNumber { get; set; }

		[Required(ErrorMessage = "ادخال الصورة مطلوب")]
		[Display(Name = "الصورة")]
		[SafeAndValidImage]
		public IFormFile? Logo { get; set; }
		[Required]
		public bool isDelete { get; set; }
	}
}
