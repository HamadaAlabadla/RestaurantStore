using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestauranteStore.Infrastructure.Services.UserService;
using RestaurantStore.Infrastructure.Services.NotificationService;

namespace RestaurantStore.Web.Controllers
{
	[Authorize]
	public class NotificationsController : Controller
	{
		private readonly INotificationService notificationService;
		private readonly IUserService userService;

		public NotificationsController(INotificationService notificationService,
			IUserService userService)
		{
			this.notificationService = notificationService;
			this.userService = userService;
		}
		// GET: NotificationsController
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult GetListNotification()
		{
			var user = userService.GetUserByContext(HttpContext);
			if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
			var notifications = notificationService.GetAllNotifications(user.Id);
			return PartialView("ListNotification", notifications);
		}

		[HttpPost]
		public IActionResult SetRead(int id)
		{
			var user = userService.GetUserByContext(HttpContext);
			if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
			var result = notificationService.SetRead(id, user.Id);
			if (result > 0)
				return Ok();
			else return NotFound();
		}

		// GET: NotificationsController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: NotificationsController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: NotificationsController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: NotificationsController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: NotificationsController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: NotificationsController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: NotificationsController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
