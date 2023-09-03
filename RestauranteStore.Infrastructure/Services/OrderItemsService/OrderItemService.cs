using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantStore.Core.Dtos;
using RestaurantStore.EF.Data;
using RestaurantStore.EF.Models;

namespace RestaurantStore.Infrastructure.Services.OrderItemsService
{
    public class OrderItemService : IOrderItemService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public OrderItemService(ApplicationDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<OrderItem?> CreateOrderItemAsync(OrderItemDto orderItemDto)
        {
            if (orderItemDto == null) return null;
            var orderItem = GetOrderItemWithDeleted(orderItemDto.OrderId, orderItemDto.ProductId);
            if (orderItem != null)
            {
                orderItem.isDelete = false;
                orderItem.Price = orderItemDto.Price;
                orderItem.QTY = orderItemDto.QTYRequierd;
                UpdateOrderItem(orderItem);
                return orderItem;
            }
            orderItem = mapper.Map<OrderItem>(orderItemDto);
            await dbContext.OrderItems.AddAsync(orderItem);
            await dbContext.SaveChangesAsync();
            return orderItem;
        }

        public OrderItem? DeleteOrderItem(int orderId, int productId)
        {
            var orderItem = GetOrderItem(orderId, productId);
            if (orderItem == null) return null;
            orderItem.isDelete = true;
            dbContext.OrderItems.Update(orderItem);
            return orderItem;
        }

        public List<OrderItem>? GetAllOrderItems(int orderId)
        {
            return dbContext.OrderItems.Where(x => !x.isDelete && x.OrderId == orderId).Include(x => x.Order).Include(x => x.Product).ToList();
        }
        private OrderItem? GetOrderItemWithDeleted(int orderId, int productId)
        {
            return dbContext.OrderItems.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
        }
        public OrderItem? GetOrderItem(int orderId, int productId)
        {
            return dbContext.OrderItems.Where(x => !x.isDelete).Include(x => x.Order).Include(x => x.Product).FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
        }

        public OrderItem? UpdateOrderItem(OrderItemDto orderItemDto)
        {
            if (orderItemDto == null) return null;
            var orderItem = mapper.Map<OrderItem>(orderItemDto);
            if (orderItem == null) return null;
            UpdateOrderItem(orderItem);
            return orderItem;
        }

        public OrderItem? UpdateOrderItem(OrderItem orderItem)
        {
            var orderItemNew = GetOrderItem(orderItem.OrderId, orderItem.ProductId);
            if (orderItemNew == null) return null;
            orderItemNew.isDelete = orderItem.isDelete;
            orderItemNew.Price = orderItem.Price;
            orderItemNew.QTY = orderItem.QTY;
            dbContext.OrderItems.Update(orderItemNew);
            dbContext.SaveChanges();
            return orderItemNew;
        }
    }
}
