using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestauranteStore.EF.Models;
using RestauranteStore.Infrastructure.Services.UserService;

namespace RestauranteStore.Web.Controllers
{
	public class BaseController : Controller
	{
		protected string? userId;
		protected string? role;
		protected User? myUser;
		private IUserService _userManager => HttpContext.RequestServices.GetService<IUserService>()!;

		public override async void OnActionExecuting(ActionExecutingContext context)
		{

			//var user = context.HttpContext.User;
			//if (user.Identity!.IsAuthenticated)
			//{
			//	var userName = user.Identity.Name;
			//	userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			//	ViewData["UserName"] = userName;
			//	ViewData["UserId"] = userId;
			//	myUser = await _userManager.GetUserAsync(userId ?? "");
			//	var domyUser = new User()
			//	{
			//		Admin = new Admin()
			//		{
			//			Logo = "00",
			//		}
			//	};
			//	if (myUser == null)
			//		myUser = domyUser;
			//	ViewData["User"] = myUser;
			//	role = await _userManager.GetRoleByUser(userId ?? "");
			//	ViewData["role"] = role;
			//}
			//base.OnActionExecuting(context);
		}
	}
}
