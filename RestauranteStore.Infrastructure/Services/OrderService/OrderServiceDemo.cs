using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using NToastNotify;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Core.ModelViewModels;
using RestauranteStore.EF.Data;
using RestauranteStore.EF.Models;
using RestauranteStore.Infrastructure.Services.FileService;
using System.Linq.Dynamic.Core;
using System.Security.Policy;
using static RestauranteStore.Core.Enums.Enums;

namespace RestauranteStore.Infrastructure.Services.OrderService
{
    public class OrderServiceDemo : IOrderService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IFileService fileService;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;
        public OrderServiceDemo(ApplicationDbContext dbContext,
            IMapper mapper,
            IFileService fileService,
            IToastNotification toastNotification)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.fileService = fileService;
            this.toastNotification = toastNotification;
        }

        // Implement the necessary methods for handling orders and sending notifications
        // Use the existing OrderService.cs as a reference
        
        public async Task<int> CreateOrder(OrderDto orderDto)
        {
            var order = mapper.Map<Order>(orderDto);
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();
            return order.Id;
        }
        
        public async Task<OrderDto> UpdateOrder(int id, OrderDto orderDto)
        {
            var order = await dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return null;
            }
            mapper.Map(orderDto, order);
            await dbContext.SaveChangesAsync();
            return mapper.Map<OrderDto>(order);
        }
        
        public async Task<bool> DeleteOrder(int id)
        {
            var order = await dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }
            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync();
            return true;
        }
        
        public async Task<OrderDto> GetOrder(int id)
        {
            var order = await dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return null;
            }
            return mapper.Map<OrderDto>(order);
        }
        
        public async Task SendNotification(string message)
        {
            toastNotification.AddInfoToastMessage(message);
        }
    }
}