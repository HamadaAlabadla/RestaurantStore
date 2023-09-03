using Microsoft.AspNetCore.Http;
using RestaurantStore.Core.Dtos;
using RestaurantStore.EF.Models;
using static RestaurantStore.Core.Enums.Enums;

namespace RestaurantStore.Infrastructure.Services.OrderService
{
    public interface IOrderService
    {
        Task<Order?> GetOrder(int id, string userId);
        Task<int> UpdateStatus(int orderId, string userId, StatusOrder status);
        Task<object?> GetOrderDetails(int id, string userId);
        Task<object?> GetRestaurantDetails(int id, string userId);
        Task<object?> GetSupplierDetails(int id, string userId);
        Task<object?> GetPaymentDetails(int id, string userId);
        object? GetOrderItems(int id, string userId);
        object? GetAllSupplierOrders(HttpRequest request, string userId);
        object? GetAllRestaurantOrders(HttpRequest request, string userId);
        Task<bool> CreateOrderAsync(OrderDto orderDto, string selectedProductIds, string quantities);
        Order? UpdateOrder(OrderDto orderDto, string userId);
        Task<Order?> DeleteOrder(int orderId, string userId);
        Task<object?> UpdateOrderDetails(OrderDetailsDto orderDetailsDto, string userId);
        Task<object?> UpdatePaymentDetails(EditPaymentDetailsDto editPaymentDetailsDto, string userId);
        Task<object?> UpdateOrderItems(int orderId, string quantities, string userId);

        Task<Order?> Cancel(int orderId, string userId);
        Task<Order?> Denied(int orderId, string userId);
        Task<Order?> Delivered(int orderId, string userId);
        Task<Order?> Delivering(int orderId, string userId);
        Task<Order?> DeliverMoney(int orderId, string userId);
    }
}
