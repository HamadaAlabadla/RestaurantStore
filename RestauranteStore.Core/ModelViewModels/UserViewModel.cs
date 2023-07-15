using System.ComponentModel.DataAnnotations;

namespace RestauranteStore.Core.ModelViewModels
{
	public class UserViewModel
	{
		public string? Id { get; set; }
		[Display(Name = "اسم الآددمن")]
		public string? Name { get; set; }
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
		[Display(Name = "نشط")]
		public bool isDelete { get; set; }
		public string? Role { get; set; }
	}
}
