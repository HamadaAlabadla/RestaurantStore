using RestauranteStore.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace RestauranteStore.Core.ModelViewModels
{
	public class AdminViewModel
	{
		public int Id { get; set; }
		[Display(Name = "نوع المستخدم")]
		public UserType UserType { get; set; }
		public string? UserTypeText { get; set; }
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
