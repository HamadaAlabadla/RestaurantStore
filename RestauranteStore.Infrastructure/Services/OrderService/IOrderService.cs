using RestauranteStore.Core.Dtos;
using RestauranteStore.EF.Models;

namespace RestauranteStore.Infrastructure.Services.OrderService
{
	public interface IOrderService
	{
		Order? GetOrder(int id);
		List<Order>? GetAllOrders(string userId);
		Order? CreateOrder(OrderDto orderDto , string selectedProductIds, string quantities);
		Order? UpdateOrder(OrderDto orderDto);
		Order? DeleteOrder(int orderId);
	}
}
