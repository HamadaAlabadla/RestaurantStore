using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Infrastructure.Services.OrderService;
using RestauranteStore.Infrastructure.Services.UserService;
using RestaurantStore.Core.Dtos;
using RestaurantStore.Core.ModelViewModels;
using static RestauranteStore.Core.Enums.Enums;


namespace RestaurantStore.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IHtmlHelper _htmlHelper;

        public OrdersController(IOrderService orderService,
            IUserService userService,
            IMapper mapper,
            IHtmlHelper _htmlHelper)
        {
            this.orderService = orderService;
            this.userService = userService;
            this.mapper = mapper;
            this._htmlHelper = _htmlHelper;
        }

        [Authorize(Roles = "supplier")]
        // GET: OrdersController
        public IActionResult IndexSupplier()
        {
            return View();
        }
        [Authorize(Roles = "supplier")]
        [HttpPost]
        public IActionResult GetAllOrdersForSupplier()
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var jsonData = orderService.GetAllSupplierOrders(Request, user.Id);

            return Ok(jsonData);
        }

        [Authorize(Roles = "restaurant")]
        // GET: OrdersController
        public IActionResult IndexRestaurant()
        {
            return View();
        }
        [Authorize(Roles = "restaurant")]
        [HttpPost]
        public IActionResult GetAllOrdersForRestaurant()
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var jsonData = orderService.GetAllRestaurantOrders(Request, user.Id);

            return Ok(jsonData);
        }
        [Authorize(Roles = "supplier")]
        // GET: OrdersController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var order = await orderService.GetOrder(id, user.Id);
            var orderViewModel = mapper.Map<OrderViewModel>(order);
            if (orderViewModel == null)
            {
                if (user.UserType == UserType.supplier)
                    return RedirectToAction(nameof(IndexSupplier));
                else if (user.UserType == UserType.restaurant)
                    return RedirectToAction(nameof(IndexRestaurant));
                else
                    return NotFound();
            }
            return View(orderViewModel);
        }
        [Authorize(Roles = "restaurant")]
        [HttpGet]
        public IActionResult DetailsForRestaurant(int id)
        {
            return View(id);
        }
        [Authorize(Roles = "restaurant")]
        [HttpGet]
        public async Task<IActionResult> OrderDetails(int id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var orderDetails = await orderService.GetOrderDetails(id, user.Id);
            if (orderDetails == null)
                return NotFound();
            return Ok(orderDetails);
        }
        [Authorize(Roles = "supplier")]
        [HttpGet]
        public async Task<IActionResult> RestaurantDetails(int id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var restaurantDetails = await orderService.GetRestaurantDetails(id, user.Id);
            if (restaurantDetails == null)
                return NotFound();
            return Ok(restaurantDetails);
        }
        [Authorize(Roles = "restaurant")]
        [HttpGet]
        public async Task<IActionResult> SupplierDetails(int id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var supplierDetails = await orderService.GetSupplierDetails(id, user.Id);
            if (supplierDetails == null)
                return NotFound();
            return Ok(supplierDetails);
        }

        [Authorize(Roles = "restaurant")]
        [HttpGet]
        public async Task<IActionResult> PaymentDetails(int id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var Payment = await orderService.GetPaymentDetails(id, user.Id);
            if (Payment == null)
                return NotFound();
            return Ok(Payment);
        }
        [Authorize(Roles = "restaurant")]
        [HttpGet]
        public IActionResult OrderItems(int id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var orderItems = orderService.GetOrderItems(id, user.Id);
            if (orderItems == null)
                return NotFound();
            return Ok(orderItems);
        }
        [Authorize(Roles = "restaurant")]
        // GET: OrdersController/Create
        public ActionResult Add_Order()
        {
            var order = new OrderDto();

            ViewData["suppliers"] = userService.GetAllSuppliersAsync();
            ViewData["status"] = Enum.GetValues(typeof(StatusOrder)).Cast<StatusOrder>()
                                .Select(v => new SelectListItem
                                {
                                    Text = v.ToString(),
                                    Value = ((int)v).ToString()
                                }).ToList();
            return View(order);
        }
        [Authorize(Roles = "restaurant")]
        // POST: OrdersController/Create
        [HttpPost]
        public async Task<IActionResult> Create(OrderDto orderDto, string selectedProductIds, string quantities)
        {
            if (ModelState.IsValid)
            {
                var result = await orderService.CreateOrderAsync(orderDto, selectedProductIds, quantities);
                if (result)
                    return Ok();
                else
                    return NotFound();
            }
            return NotFound();
        }


        //      [HttpGet]
        //// GET: OrdersController/Edit/5
        //public async Task<IActionResult> EditStatus(int id)
        //{
        //	var user = userService.GetUserByContext(HttpContext);
        //	if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
        //	var statusorder = Enum.GetValues(typeof(StatusOrder)).Cast<StatusOrder>()
        //						.Select(v => new SelectListItem
        //						{
        //							Text = v.ToString(),
        //							Value = ((int)v).ToString()
        //						}).ToList();
        //	var order = await orderService.GetOrder(id, user.Id);
        //	return PartialView("EditStatus", new EditOrderStatusDto() { Id = order.Id, StatusOrder = order.StatusOrder, StatusOrders = statusorder });
        //}

        //// POST: OrdersController/Edit/5
        //[HttpPost]
        //public async Task<IActionResult> EditStatus(EditOrderStatusDto editOrderStatusDto)
        //{
        //	if (ModelState.IsValid)
        //	{
        //		var user = userService.GetUserByContext(HttpContext);
        //		if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
        //		var result = await orderService.UpdateStatus(editOrderStatusDto.Id, user.Id, editOrderStatusDto.StatusOrder);
        //		if (result > 0)
        //			return Ok();
        //		else
        //			return NotFound();
        //	}
        //	else
        //	{
        //		return NotFound();
        //	}
        //}

        [Authorize(Roles = "restaurant")]
        [HttpGet]
        public async Task<IActionResult> EditOrderDetails(int id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var order = await orderService.GetOrder(id, user.Id);
            if (order == null || (order.StatusOrder != StatusOrder.Draft
                && order.StatusOrder != StatusOrder.Pending
                && order.StatusOrder != StatusOrder.Processing))
                return NotFound();
            var orderDetailsDto = mapper.Map<OrderDetailsDto>(order);
            return PartialView("EditOrderDetails", orderDetailsDto);

        }
        [Authorize(Roles = "restaurant")]
        [HttpGet]
        public async Task<IActionResult> EditPaymentDetails(int id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var order = await orderService.GetOrder(id, user.Id);
            if (order == null || (order.StatusOrder != StatusOrder.Draft
                && order.StatusOrder != StatusOrder.Pending
                && order.StatusOrder != StatusOrder.Processing))
                return NotFound();
            var paymentDetailsDto = mapper.Map<EditPaymentDetailsDto>(order);
            return PartialView("EditPaymentDetails", paymentDetailsDto);

        }
        [Authorize(Roles = "restaurant")]
        [HttpGet]
        public async Task<IActionResult> EditOrderItems(int id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var order = await orderService.GetOrder(id, user.Id);
            //var paymentDetailsDto = mapper.Map<EditPaymentDetailsDto>(order);
            if (order == null || (order.StatusOrder != StatusOrder.Draft
                && order.StatusOrder != StatusOrder.Pending
                && order.StatusOrder != StatusOrder.Processing))
                return NotFound();
            return PartialView("EditOrderItems");

        }

        [Authorize(Roles = "restaurant")]
        [HttpPost]
        public async Task<IActionResult> EditOrderItems(int OrderId, string quantities)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var result = await orderService.UpdateOrderItems(OrderId, quantities, user.Id);
            if (result == null) return NotFound();
            else return Ok(result);
        }
        [Authorize(Roles = "restaurant")]
        [HttpPost]
        public async Task<IActionResult> EditOrderDetails(OrderDetailsDto orderDetailsDto)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var result = await orderService.UpdateOrderDetails(orderDetailsDto, user.Id);
            if (result != null) return Ok(result);
            else return NotFound();
        }
        [Authorize(Roles = "restaurant")]
        [HttpPost]
        public async Task<IActionResult> EditPaymentDetails(EditPaymentDetailsDto editPaymentDetailsDto)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var result = await orderService.UpdatePaymentDetails(editPaymentDetailsDto, user.Id);
            if (result != null) return Ok(result);
            else return NotFound();
        }

        [Authorize(Roles = "restaurant")]
        // POST: OrdersController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var result = await orderService.DeleteOrder(id, user.Id);
            if (result != null) return Ok(result);
            else return NotFound();
        }
        [Authorize(Roles = "restaurant")]
        // POST: OrdersController/Delete/5
        [HttpPost]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var result = await orderService.Cancel(id, user.Id);
            if (result != null) return Ok();
            else return NotFound();
        }

        [Authorize(Roles = "supplier")]
        // POST: OrdersController/Delete/5
        [HttpPost]
        public async Task<IActionResult> DeniedOrder(int id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var result = await orderService.Denied(id, user.Id);
            if (result != null) return Ok();
            else return NotFound();
        }
        [Authorize(Roles = "restaurant")]
        [HttpPost]
        public async Task<IActionResult> Delivered(int id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var result = await orderService.Delivered(id, user.Id);
            if (result != null) return Ok();
            else return NotFound();
        }
        [Authorize(Roles = "supplier")]
        [HttpPost]
        public async Task<IActionResult> Delivering(int id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var result = await orderService.Delivering(id, user.Id);
            if (result != null) return Ok();
            else return NotFound();
        }
        [Authorize(Roles = "supplier")]
        [HttpPost]
        public async Task<IActionResult> DeliverMoney(int id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            var result = await orderService.DeliverMoney(id, user.Id);
            if (result != null) return Ok();
            else return NotFound();
        }
    }
}