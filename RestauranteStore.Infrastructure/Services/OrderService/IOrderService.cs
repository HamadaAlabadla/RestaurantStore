using Microsoft.AspNetCore.Http;
using RestauranteStore.Core.Dtos;
using RestauranteStore.EF.Models;
using RestaurantStore.Core.Dtos;
using static RestauranteStore.Core.Enums.Enums;

namespace RestauranteStore.Infrastructure.Services.OrderService
{
	public interface IOrderService
	{
		Order? GetOrder(int id , string userId);
		int UpdateStatus(int orderId, string userId, StatusOrder status );
		object? GetOrderDetails(int id , string userId);
		object? GetRestaurantDetails(int id , string userId);
		object? GetSupplierDetails(int id, string userId);
		object? GetPaymentDetails(int id, string userId);
		object? GetOrderItems(int id, string userId);
		object? GetAllSupplierOrders( HttpRequest request, string userId);
		object? GetAllRestaurantOrders( HttpRequest request, string userId);
		List<Order>? CreateOrder(OrderDto orderDto , string selectedProductIds, string quantities);
		Order? UpdateOrder(OrderDto orderDto , string userId);
		Order? DeleteOrder(int orderId , string userId);
		object? UpdateOrderDetails(OrderDetailsDto orderDetailsDto, string userId);
		object? UpdatePaymentDetails(EditPaymentDetailsDto editPaymentDetailsDto, string userId);
	}
}
