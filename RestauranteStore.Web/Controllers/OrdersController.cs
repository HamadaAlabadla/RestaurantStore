using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestauranteStore.Core.Dtos;
using RestauranteStore.EF.Models;
using RestauranteStore.Infrastructure.Services.OrderService;
using RestauranteStore.Infrastructure.Services.UserService;
using RestaurantStore.Core.ModelViewModels;
using static RestauranteStore.Core.Enums.Enums;

namespace RestaurantStore.Web.Controllers
{
	public class OrdersController : Controller
	{
		private readonly IOrderService orderService;
		private readonly IUserService userService;
		private readonly IMapper mapper;

		public OrdersController(IOrderService orderService,
			IUserService userService,
			IMapper mapper)
		{
			this.orderService = orderService;
			this.userService = userService;
			this.mapper = mapper;
		}


		// GET: OrdersController
		public IActionResult Index()
		{
			var user = userService.GetUserByContext(HttpContext);
			if (user == null || user.UserType == UserType.admin) return NotFound();

			var orders = orderService.GetAllOrders(user.Id);
			var ordersRestaurantViewModel = mapper.Map<IEnumerable<OrderListRestaurantViewModel>>(orders);
			return View(ordersRestaurantViewModel);
		}

		// GET: OrdersController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: OrdersController/Create
		public ActionResult Add_Order()
		{
			var order = new OrderDto();
			
			ViewData["suppliers"] =  userService.GetAllSuppliersAsync();
			ViewData["status"] = Enum.GetValues(typeof(StatusOrder)).Cast<StatusOrder>()
								.Select(v => new SelectListItem
								{
									Text = v.ToString(),
									Value = ((int)v).ToString()
								}).ToList();
			return View(order);
		}

		// POST: OrdersController/Create
		[HttpPost]
		public IActionResult Create(OrderDto orderDto , string selectedProductIds, string quantities)
		{
			if (ModelState.IsValid)
			{
				orderService.CreateOrder(orderDto , selectedProductIds ,quantities );
				return Ok();
			}
			return NotFound();
		}
		
		// GET: OrdersController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: OrdersController/Edit/5
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

		// GET: OrdersController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: OrdersController/Delete/5
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
