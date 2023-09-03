using RestaurantStore.Core.Dtos;
using RestaurantStore.EF.Models;

namespace RestaurantStore.Infrastructure.Services.OrderItemsService
{
    public interface IOrderItemService
    {
        OrderItem? GetOrderItem(int orderId, int productId);
        List<OrderItem>? GetAllOrderItems(int orderId);
        Task<OrderItem?> CreateOrderItemAsync(OrderItemDto orderItemDto);
        OrderItem? UpdateOrderItem(OrderItemDto orderItemDto);
        OrderItem? UpdateOrderItem(OrderItem orderItem);
        OrderItem? DeleteOrderItem(int orderId, int productId);
    }
}
