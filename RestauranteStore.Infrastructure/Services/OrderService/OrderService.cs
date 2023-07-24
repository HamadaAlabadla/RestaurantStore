using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestauranteStore.Core.Dtos;
using RestauranteStore.EF.Data;
using RestauranteStore.EF.Models;
using RestauranteStore.Infrastructure.Services.OrderItemsService;
using RestauranteStore.Infrastructure.Services.OrderService;
using RestauranteStore.Infrastructure.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RestauranteStore.Core.Enums.Enums;

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

		public Order? CreateOrder(OrderDto orderDto, string selectedProductIds, string quantities)
		{
			if(orderDto == null) return null;
			var order = mapper.Map<Order>(orderDto);
			if(order == null) return null;
			order.DateCreate = DateTime.UtcNow;
			dbContext.Orders.Add(order);
			dbContext.SaveChanges();
			List<int> selectedIds = selectedProductIds.Split(',').Select(int.Parse).ToList();

			// Convert the JSON string to a Dictionary<int, int> for quantities
			Dictionary<int, double> quantityDictionary = JsonConvert.DeserializeObject<Dictionary<int, double>>(quantities)?? new Dictionary<int, double>();
			foreach (var item in quantityDictionary)
			{
				var orderItemDto = new OrderItemDto()
				{
					isDelete = false,
					OrderId = order.Id,
					ProductId = item.Key,
					QTY = item.Value,
					Price = (productService.GetProduct(item.Key)??new Product() { Price = 0.0}).Price,
				};
				orderItemService.CreateOrderItem(orderItemDto);
				order.TotalPrice += orderItemDto.Price * orderItemDto.QTYRequierd;
			}
			UpdateOrder(order);
			
			
			return order;
		}

		public Order? DeleteOrder(int orderId)
		{
			var order = GetOrder(orderId);
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

		public List<Order>? GetAllOrders(string userId)
		{
			return dbContext.Orders
				.Include(x=>x.Supplier).Include(x=> x.Restaurant).Include(x=>x.Restaurant.User)
				.Where(x => x.SupplierId.Equals(userId)?true:x.RestaurantId.Equals(userId)?true:false)
				.ToList();
		}

		public Order? GetOrder(int id)
		{
			return dbContext.Orders.Include(x => x.Restaurant).Include(x => x.Supplier).Include(x => x.OrderItems).FirstOrDefault(x => x.Id == id);
		}

		public Order? UpdateOrder(OrderDto orderDto)
		{
			throw new NotImplementedException();
		}
		public Order? UpdateOrder(Order order)
		{
			if (order == null) return null;
			var orderNew = GetOrder(order.Id);
			if (orderNew == null) return null;
			orderNew.isDelete = order.isDelete;
			orderNew.PaymentMethod = order.PaymentMethod;
			orderNew.ShippingAddress = order.ShippingAddress;
			orderNew.ShippingCity = order.ShippingCity;
			orderNew.StatusOrder = order.StatusOrder;
			orderNew.TotalPrice = order.TotalPrice;
			dbContext.Orders.Update(orderNew);
			dbContext.SaveChanges();
			return orderNew;
		}
	}
}
