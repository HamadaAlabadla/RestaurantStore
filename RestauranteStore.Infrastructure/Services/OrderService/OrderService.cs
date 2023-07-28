using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Core.ModelViewModels;
using RestauranteStore.EF.Data;
using RestauranteStore.EF.Models;
using RestauranteStore.Infrastructure.Services.OrderItemsService;
using RestauranteStore.Infrastructure.Services.OrderService;
using RestauranteStore.Infrastructure.Services.ProductService;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RestauranteStore.Core.Enums.Enums;
using RestaurantStore.Core.ModelViewModels;
using RestaurantStore.Core.Dtos;

namespace RestaurantStore.Infrastructure.Services.OrderService
{
	public class OrderService : IOrderService
	{
		private readonly ApplicationDbContext dbContext;
		private readonly IOrderItemService orderItemService;
		private readonly IMapper mapper;
		private readonly IProductService productService;

		public OrderService(ApplicationDbContext dbContext,
			IOrderItemService orderItemService,
			IMapper mapper,
			IProductService productService)
		{
			this.dbContext = dbContext;
			this.orderItemService = orderItemService;
			this.mapper = mapper;
			this.productService = productService;
		}

		public List<Order>? CreateOrder(OrderDto orderDto, string supplierIds, string quantities)
		{
			if(orderDto == null) return null;
			List<Order> orders = new List<Order>();
			Dictionary<int, string> suppliersDectionary = JsonConvert.DeserializeObject<Dictionary<int, string>>(supplierIds) ?? new Dictionary<int, string>();

			// Convert the JSON string to a Dictionary<int, int> for quantities
			Dictionary<int, double> quantityDictionary = JsonConvert.DeserializeObject<Dictionary<int, double>>(quantities)?? new Dictionary<int, double>();
			List<OrderItemDto> orderItemDtos = new List<OrderItemDto>();
			foreach (var item in quantityDictionary)
			{
				var orderItemDto = new OrderItemDto()
				{
					SupplierId = suppliersDectionary.GetValueOrDefault(item.Key)??"",
					isDelete = false,
					Price = (productService.GetProduct(item.Key) ?? new Product() { Price = 0.0 }).Price,
					ProductId = item.Key,
					QTYRequierd = item.Value,
				};
			}

			var groups = orderItemDtos.GroupBy(x => x.SupplierId);
			foreach (var group in groups)
			{
				var order = mapper.Map<Order>(orderDto);
				if (orderDto.IsDraft)
					order.StatusOrder = StatusOrder.Draft;
				else
					order.StatusOrder = StatusOrder.Pending;
				order.TotalPrice = group.Sum(x => x.Price*x.QTYRequierd);
				dbContext.Orders.Add(order);
				dbContext.SaveChanges();
				foreach (var item in group)
				{
					item.OrderId = order.Id;
					orderItemService.CreateOrderItem(item);
				}
				orders.Add(order);
			}			
			
			return orders;
		}

		public Order? DeleteOrder(int orderId , string userId)
		{
			var order = GetOrder(orderId , userId);
			if(order == null) return null;
			foreach(var item in order.OrderItems)
			{
				item.isDelete = true;
				orderItemService.UpdateOrderItem(item);
			}
			order.isDelete = true;
			dbContext.Orders.Update(order);
			dbContext.SaveChanges();
			return order;
		}

		public IQueryable<Order> GetAllOrders(string search , string filter , string userId)
		{
			StatusOrder? filterEnum = null;
			if (!string.IsNullOrEmpty(filter))
			{
				try
				{
					filterEnum = (StatusOrder)Enum.Parse(typeof(StatusOrder), filter, true);
				}
				catch
				{
					filterEnum = null;
				}
			}
			return dbContext.Orders
				.Where(x => !x.isDelete 
						&& (
						 x.SupplierId.Equals(userId) 
						 ||
						 x.RestaurantId.Equals(userId)
						)
						&&(
						 x.RestaurantId.Equals(userId)?true:x.StatusOrder != StatusOrder.Draft
						)
						&&(
							filterEnum == null || x.StatusOrder == filterEnum
						)
				);
		}
		public object? GetAllSupplierOrders(HttpRequest request, string userId)
		{
			var pageLength = int.Parse((request.Form["length"].ToString()) ?? "");
			var skiped = int.Parse((request.Form["start"].ToString()) ?? "");
			var searchData = request.Form["search[value]"];
			var sortColumn = request.Form[string.Concat("columns[", request.Form["order[0][column]"], "][name]")];
			var sortDir = request.Form["order[0][dir]"];
			var filter = request.Form["filter"];
			if (string.IsNullOrEmpty(filter))
				filter = new StringValues("") { };
			var orders = GetAllOrders(searchData[0] ?? "", filter[0] ?? "", userId);
			var recordsTotal = orders.Count();
			
			if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDir))
				orders = orders.OrderBy(string.Concat(sortColumn, " ", sortDir));


			var data = orders.Include(x => x.Restaurant).ThenInclude(x => x.User)
				.Skip(skiped).Take(pageLength).ToList();
			var orderListSupplierViewModel = mapper.Map<IEnumerable<OrderListSupplierViewModel>>(data);
			var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data = orderListSupplierViewModel };
			return jsonData;
		}

		public object? GetAllRestaurantOrders(HttpRequest request, string userId)
		{
			var pageLength = int.Parse((request.Form["length"].ToString()) ?? "");
			var skiped = int.Parse((request.Form["start"].ToString()) ?? "");
			var searchData = request.Form["search[value]"];
			var sortColumn = request.Form[string.Concat("columns[", request.Form["order[0][column]"], "][name]")];
			var sortDir = request.Form["order[0][dir]"];
			var filter = request.Form["filter"];
			if (string.IsNullOrEmpty(filter))
				filter = new StringValues("") { };
			var orders = GetAllOrders(searchData[0] ?? "", filter[0] ?? "", userId);
			var recordsTotal = orders.Count();

			if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDir))
				orders = orders.OrderBy(string.Concat(sortColumn, " ", sortDir));


			var data = orders.Include(x => x.Supplier)
				.Skip(skiped).Take(pageLength).ToList();
			var orderListRestaurantViewModel = mapper.Map<IEnumerable<OrderListRestaurantViewModel>>(data);
			var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data = orderListRestaurantViewModel };
			return jsonData;
		}



		public int UpdateStatus(int orderId ,string userId, StatusOrder status)
		{
			var order = GetOrder(orderId , userId);
			if(order != null)
			{
				order.StatusOrder = status;
				order.DateModified = DateTime.UtcNow;
				dbContext.Orders.Update(order);
				dbContext.SaveChanges();
				return orderId;
			}
			return -1;
		}
        public Order? GetOrder(int id , string userId)
		{
			var order = dbContext.Orders.Where(x => !x.isDelete && x.Id == id
				&& (
						x.SupplierId.Equals(userId)
						||
						x.RestaurantId.Equals(userId)
					)
				&& (
					x.RestaurantId.Equals(userId) ? true : x.StatusOrder != StatusOrder.Draft
				))
				.Include(x => x.Supplier).Include(x => x.Restaurant).ThenInclude(x => x.User).Include(x => x.OrderItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.Id == id);
			if (order == null) return null;
			if ( order.SupplierId.Equals(userId) && order.StatusOrder == StatusOrder.Pending)
			{
				order.StatusOrder = StatusOrder.Processing;
				order.DateModified = DateTime.UtcNow;
				dbContext.Orders.Update(order);
				dbContext.SaveChanges();
			}
			return order;
		}

		public Order? UpdateOrder(OrderDto orderDto , string userId)
		{
			throw new NotImplementedException();
		}
		public Order? UpdateOrder(Order order , string userId)
		{
			if (order == null) return null;
			var orderNew = GetOrder(order.Id , userId);
			if (orderNew == null) return null;
			orderNew.isDelete = order.isDelete;
			orderNew.PaymentMethod = order.PaymentMethod;
			orderNew.ShippingAddress = order.ShippingAddress;
			orderNew.ShippingCity = order.ShippingCity;
			orderNew.StatusOrder = order.StatusOrder;
			orderNew.TotalPrice = order.TotalPrice;
			orderNew.DateModified = DateTime.UtcNow;
			dbContext.Orders.Update(orderNew);
			dbContext.SaveChanges();
			return orderNew;
		}

		public object? GetOrderDetails(int id, string userId)
		{
			var order = GetOrder(id, userId);
			if (order == null) return null;
			var orderDetailsViewModel = mapper.Map<OrderDetailsViewModel>(order);
			return new {data =  orderDetailsViewModel};
		}

		public object? UpdateOrderDetails(OrderDetailsDto orderDetailsDto, string userId)
		{
			if(orderDetailsDto == null) return -1;
			var order = GetOrder(orderDetailsDto.Id, userId);
			if(order == null) return -1;
			if (order.StatusOrder == StatusOrder.Draft)
			{
				if (!orderDetailsDto.IsDraft)
					UpdateStatus(order.Id, userId, StatusOrder.Pending);
			}
			order.OrderDate = orderDetailsDto.OrderDate;
			order.PaymentMethod = orderDetailsDto.PaymentMethod;
			dbContext.Orders.Update(order);
			dbContext.SaveChanges();
			var orderDetailsViewModel = mapper.Map<OrderDetailsViewModel>(order);
			var jsondata = new { data =  orderDetailsViewModel };
			return jsondata;
		}

		public object? GetRestaurantDetails(int id, string userId)
		{
			var order = GetOrder(id, userId);
			if (order == null) return null;
			var restaurantDetailsViewModel = mapper.Map<UserDetailsViewModel>(order.Restaurant.User);
			return new { data = restaurantDetailsViewModel };
		}
		public object? GetSupplierDetails(int id, string userId)
		{
			var order = GetOrder(id, userId);
			if (order == null) return null;
			var supplierDetailsViewModel = mapper.Map<UserDetailsViewModel>(order.Supplier);
			return new { data = supplierDetailsViewModel };
		}

		public object? GetPaymentDetails(int id, string userId)
		{
			var order = GetOrder(id, userId);
			if (order == null) return null;
			var paymentDetailsViewModel = mapper.Map<PaymentDetailsViewModel>(order);
			return new { data = paymentDetailsViewModel };
		}

		public object? UpdatePaymentDetails(EditPaymentDetailsDto editPaymentDetailsDto, string userId)
		{
			if(editPaymentDetailsDto == null) return null;
			var order = GetOrder(editPaymentDetailsDto.Id, userId);
			if (order == null) return null;
			order.ShippingAddress = editPaymentDetailsDto.ShippingAddress;
			order.ShippingCity = order.ShippingCity;
			dbContext.Orders.Update(order);
			dbContext.SaveChanges();
			return new { data = order };
		}

		public object? GetOrderItems(int id, string userId)
		{
			var orderItems = orderItemService.GetAllOrderItems(id);	
			if(orderItems == null || orderItems.Count() <1) return null;
			var orderItemsViewModel = mapper.Map<IEnumerable<OrderItemViewModel>>(orderItems);
			if(orderItemsViewModel == null || orderItemsViewModel.Count() <1) return null;
			else return new { data = orderItemsViewModel };

		}
	}
}
