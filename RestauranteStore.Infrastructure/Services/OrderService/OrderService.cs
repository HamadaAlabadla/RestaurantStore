using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using RestauranteStore.Core.Dtos;
using RestauranteStore.EF.Data;
using RestauranteStore.EF.Models;
using RestauranteStore.Infrastructure.Services.OrderItemsService;
using RestauranteStore.Infrastructure.Services.OrderService;
using RestauranteStore.Infrastructure.Services.ProductService;
using RestaurantStore.Core.Dtos;
using RestaurantStore.Core.ModelViewModels;
using RestaurantStore.EF.Models;
using RestaurantStore.Infrastructure.Hubs;
using RestaurantStore.Infrastructure.Services.EmailService;
using RestaurantStore.Infrastructure.Services.NotificationService;
using System.Linq.Dynamic.Core;
using static RestauranteStore.Core.Enums.Enums;

namespace RestaurantStore.Infrastructure.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IBackgroundJobClient backgroundJob;
        private readonly IHubContext<NotificationHub> hubContext;
        private readonly IOrderItemService orderItemService;
        private readonly INotificationService notificationService;
        private readonly IMapper mapper;
        private readonly IProductService productService;
        private readonly IEmailService emailSender;

        public OrderService(ApplicationDbContext dbContext,
            IOrderItemService orderItemService,
            IMapper mapper,
            IProductService productService,
            INotificationService notificationService,
            IHubContext<NotificationHub> hubContext,
            IEmailService emailSender,
            IBackgroundJobClient backgroundJob
            )
        {
            this.dbContext = dbContext;
            this.orderItemService = orderItemService;
            this.mapper = mapper;
            this.productService = productService;
            this.notificationService = notificationService;
            this.hubContext = hubContext;
            this.emailSender = emailSender;
            this.backgroundJob = backgroundJob;

        }

        public async Task<bool> CreateOrderAsync(OrderDto orderDto, string supplierIds, string quantities)
        {

            if (orderDto == null) return false;
            List<Order> orders = new List<Order>();
            Dictionary<int, string> suppliersDectionary = JsonConvert.DeserializeObject<Dictionary<int, string>>(supplierIds) ?? new Dictionary<int, string>();

            // Convert the JSON string to a Dictionary<int, int> for quantities
            Dictionary<int, double> quantityDictionary = JsonConvert.DeserializeObject<Dictionary<int, double>>(quantities) ?? new Dictionary<int, double>();
            //List<OrderItemDto> orderItemDtos = new List<OrderItemDto>();
            //foreach (var item in quantityDictionary)
            //{
            //	var orderItemDto = new OrderItemDto()
            //	{
            //		SupplierId = suppliersDectionary.GetValueOrDefault(item.Key) ?? "",
            //		isDelete = false,
            //		Price = (productService.GetProduct(item.Key) ?? new Product() { Price = 0.0 }).Price,
            //		ProductId = item.Key,
            //		QTYRequierd = item.Value,
            //	};
            //	orderItemDtos.Add(orderItemDto);
            //}
            var orderItemDtos = quantityDictionary.Select(item => new OrderItemDto
            {
                SupplierId = suppliersDectionary.TryGetValue(item.Key, out var supplierId) ? supplierId : "",
                isDelete = false,
                Price = productService.GetProduct(item.Key)?.Price ?? 0.0,
                ProductId = item.Key,
                QTYRequierd = item.Value,
            }).ToList();
            try
            {
                var groups = orderItemDtos.GroupBy(x => x.SupplierId);
                foreach (var group in groups)
                {
                    await CreateOrderAsync(group.ToList(), orderDto);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task CreateOrderAsync(List<OrderItemDto> group, OrderDto orderDto)
        {
            var order = mapper.Map<Order>(orderDto);
            order.StatusOrder = orderDto.IsDraft ? StatusOrder.Draft : StatusOrder.Pending;
            order.TotalPrice = group.Sum(x => x.Price * x.QTYRequierd);
            order.SupplierId = group[0].SupplierId; // Since all items in the group belong to the same supplier
            order.DateCreate = DateTime.UtcNow;
            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();

            //await emailSender.SendEmailAsync("alkhoulyibraheem@gmail.com", "hello", html);
            foreach (var item in group)
            {
                item.OrderId = order.Id;
                await orderItemService.CreateOrderItemAsync(item);
            }
            order = await GetOrder(order.Id, order.RestaurantId);
            if (order != null)
            {
                backgroundJob.Enqueue(() => SendEmail("sadeg.magde024@gmail.com", order));
                backgroundJob.Enqueue(() => SendNotifi(order));
            }
        }
        public async Task SendNotifi(Order order)
        {
            if (order.StatusOrder != StatusOrder.Draft)
            {
                var notifi = new Notification()
                {
                    OrderId = order.Id,
                    FromUserId = order.RestaurantId,
                    ToUserId = order.SupplierId,
                    DateAdded = DateTime.UtcNow,
                    DateReady = null,
                    Header = "New Order Received",
                    Body = $"Order ID: {order.Id}\r\nRestaurant: {((order.Restaurant ?? new Restaurant()).User ?? new User() { Name = "undefined" }).Name}\r\nTotal Amount: {order.TotalPrice}\r\n",
                    isRead = false,
                    URL = $"/Orders/Details/{order.Id}"

                };
                await notificationService.Create(notifi);
            }
        }

        public async Task SendEmail(string Email, Order order)
        {
            await emailSender.SendDistinctiveStyleEmail(Email, order);
        }

        public async Task<Order?> DeleteOrder(int orderId, string userId)
        {
            var order = await GetOrder(orderId, userId);
            if (order == null) return null;
            foreach (var item in order.OrderItems)
            {
                item.isDelete = true;
                orderItemService.UpdateOrderItem(item);
            }
            order.isDelete = true;
            dbContext.Orders.Update(order);
            dbContext.SaveChanges();
            return order;
        }

        public IQueryable<Order> GetAllOrders(string search, string filter, string userId)
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
                        && (
                         x.RestaurantId.Equals(userId) ? true : x.StatusOrder != StatusOrder.Draft
                        )
                        && (
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



        public async Task<int> UpdateStatus(int orderId, string userId, StatusOrder status)
        {
            var order = await GetOrder(orderId, userId);
            if (order != null)
            {
                order.StatusOrder = status;
                order.DateModified = DateTime.UtcNow;
                dbContext.Orders.Update(order);
                dbContext.SaveChanges();
                //var notifi = new Notification()
                //{
                //	Body = $"change status to {status.ToString()}",
                //	DateAdded = DateTime.UtcNow,
                //	DateReady = null,
                //	FromUserId = userId,
                //	ToUserId = order.SupplierId.Equals(userId) ? order.RestaurantId : order.SupplierId,
                //	Header = $"change status to {status.ToString()}",
                //	isRead = false,
                //	OrderId = orderId,
                //	URL = order.SupplierId.Equals(userId)? $"/Orders/DetailsForRestaurant/{order.Id}": $"/Orders/Details/{order.Id}"

                //};
                //notificationService.Create(notifi);
                //notifi = notificationService.GetNotification(notifi.Id);
                //if (notifi == null) return orderId;
                //var notifiViewModel = mapper.Map<NotificationViewModel>(notifi);
                //await hubContext.Clients.Group(notifi.ToUserId).SendAsync("ReceiveNotification", notifiViewModel);
                return orderId;
            }
            return -1;
        }
        public async Task<Order?> GetOrder(int id, string userId)
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
            if (order.SupplierId.Equals(userId) && order.StatusOrder == StatusOrder.Pending)
            {
                order.StatusOrder = StatusOrder.Processing;
                order.DateModified = DateTime.UtcNow;
                dbContext.Orders.Update(order);
                dbContext.SaveChanges();
                var notifi = new Notification()
                {
                    Body = $"Your request is being processed",
                    DateAdded = DateTime.UtcNow,
                    DateReady = null,
                    FromUserId = userId,
                    ToUserId = order.SupplierId.Equals(userId) ? order.RestaurantId : order.SupplierId,
                    Header = $"Your request is being processed",
                    isRead = false,
                    OrderId = order.Id,
                    URL = order.SupplierId.Equals(userId) ? $"/Orders/DetailsForRestaurant/{order.Id}" : $"/Orders/Details/{order.Id}"

                };
                await notificationService.Create(notifi);
            }
            return order;
        }

        public Order? UpdateOrder(OrderDto orderDto, string userId)
        {
            throw new NotImplementedException();
        }
        public async Task<Order?> UpdateOrder(Order order, string userId)
        {
            if (order == null) return null;
            var orderNew = await GetOrder(order.Id, userId);
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

        public async Task<object?> GetOrderDetails(int id, string userId)
        {
            var order = await GetOrder(id, userId);
            if (order == null) return null;
            var orderDetailsViewModel = mapper.Map<OrderDetailsViewModel>(order);
            return new { data = orderDetailsViewModel };
        }

        public async Task<object?> UpdateOrderDetails(OrderDetailsDto orderDetailsDto, string userId)
        {
            if (orderDetailsDto == null) return -1;
            var order = await GetOrder(orderDetailsDto.Id, userId);
            if (order == null || (order.StatusOrder != StatusOrder.Draft
                && order.StatusOrder != StatusOrder.Pending
                && order.StatusOrder != StatusOrder.Processing))
                return -1;
            if (order.StatusOrder == StatusOrder.Draft)
            {
                if (!orderDetailsDto.IsDraft)
                {
                    await UpdateStatus(order.Id, userId, StatusOrder.Pending);
                    var notifi = new Notification()
                    {
                        OrderId = order.Id,
                        FromUserId = order.RestaurantId,
                        ToUserId = order.SupplierId,
                        DateAdded = DateTime.UtcNow,
                        DateReady = null,
                        Header = "New Order Received",
                        Body = $"Order ID: {order.Id}\r\nRestaurant: {((order.Restaurant ?? new Restaurant()).User ?? new User() { Name = "undefined" }).Name}\r\nTotal Amount: {order.TotalPrice}\r\n",
                        isRead = false,
                        URL = $"/Orders/Details/{order.Id}"

                    };
                    await notificationService.Create(notifi);
                }
                order.OrderDate = orderDetailsDto.OrderDate;
                order.PaymentMethod = orderDetailsDto.PaymentMethod;
                dbContext.Orders.Update(order);
                dbContext.SaveChanges();
            }
            else
            {
                order.OrderDate = orderDetailsDto.OrderDate;
                order.PaymentMethod = orderDetailsDto.PaymentMethod;
                dbContext.Orders.Update(order);
                dbContext.SaveChanges();
                var notifi = new Notification()
                {
                    Body = $"The request information has been updated  #{order.Id}",
                    DateAdded = DateTime.UtcNow,
                    DateReady = null,
                    FromUserId = userId,
                    ToUserId = order.RestaurantId,
                    Header = $"The request information has been updated from Restaurant",
                    isRead = false,
                    OrderId = order.Id,
                    URL = $"/Orders/Details/{order.Id}"

                };
                await notificationService.Create(notifi);
            }

            var orderDetailsViewModel = mapper.Map<OrderDetailsViewModel>(order);
            var jsondata = new { data = orderDetailsViewModel };
            return jsondata;
        }

        public async Task<object?> GetRestaurantDetails(int id, string userId)
        {
            var order = await GetOrder(id, userId);
            if (order == null) return null;
            var restaurantDetailsViewModel = mapper.Map<UserDetailsViewModel>(order.Restaurant.User);
            return new { data = restaurantDetailsViewModel };
        }
        public async Task<object?> GetSupplierDetails(int id, string userId)
        {
            var order = await GetOrder(id, userId);
            if (order == null) return null;
            var supplierDetailsViewModel = mapper.Map<UserDetailsViewModel>(order.Supplier);
            return new { data = supplierDetailsViewModel };
        }

        public async Task<object?> GetPaymentDetails(int id, string userId)
        {
            var order = await GetOrder(id, userId);
            if (order == null) return null;
            var paymentDetailsViewModel = mapper.Map<PaymentDetailsViewModel>(order);
            return new { data = paymentDetailsViewModel };
        }

        public async Task<object?> UpdatePaymentDetails(EditPaymentDetailsDto editPaymentDetailsDto, string userId)
        {
            if (editPaymentDetailsDto == null) return null;
            var order = await GetOrder(editPaymentDetailsDto.Id, userId);
            if (order == null || (order.StatusOrder != StatusOrder.Draft
                && order.StatusOrder != StatusOrder.Pending
                && order.StatusOrder != StatusOrder.Processing))
                return null;
            order.ShippingAddress = editPaymentDetailsDto.ShippingAddress;
            order.ShippingCity = order.ShippingCity;
            dbContext.Orders.Update(order);
            dbContext.SaveChanges();
            var notifi = new Notification()
            {
                OrderId = order.Id,
                FromUserId = order.RestaurantId,
                ToUserId = order.SupplierId,
                DateAdded = DateTime.UtcNow,
                DateReady = null,
                Header = $"Payment information has been updated #{order.Id}",
                Body = $"Payment information has been updated #{order.Id}",
                isRead = false,
                URL = $"/Orders/Details/{order.Id}"

            };
            await notificationService.Create(notifi);
            return new { data = order };
        }

        public object? GetOrderItems(int id, string userId)
        {
            var orderItems = orderItemService.GetAllOrderItems(id);
            if (orderItems == null || orderItems.Count() < 1) return null;
            var orderItemsViewModel = mapper.Map<IEnumerable<OrderItemViewModel>>(orderItems);
            if (orderItemsViewModel == null || orderItemsViewModel.Count() < 1) return null;
            else return new { data = orderItemsViewModel };

        }


        public async Task<object?> UpdateOrderItems(int orderId, string quantities, string userId)
        {
            var order = await GetOrder(orderId, userId);
            if (order == null || (order.StatusOrder != StatusOrder.Draft
                && order.StatusOrder != StatusOrder.Pending
                && order.StatusOrder != StatusOrder.Processing))
                return null;
            var orderItems = order.OrderItems.Where(x => !x.isDelete);
            // Convert the JSON string to a Dictionary<int, int> for quantities
            Dictionary<int, double> quantityDictionary = JsonConvert.DeserializeObject<Dictionary<int, double>>(quantities) ?? new Dictionary<int, double>();
            List<OrderItemDto> orderItemDtos = new List<OrderItemDto>();
            foreach (var item in quantityDictionary)
            {
                var orderItemDto = new OrderItemDto()
                {
                    OrderId = orderId,
                    SupplierId = order.SupplierId,
                    isDelete = false,
                    Price = (productService.GetProduct(item.Key) ?? new Product() { Price = 0.0 }).Price,
                    ProductId = item.Key,
                    QTYRequierd = item.Value,
                };
                if (orderItemDto.SupplierId.Equals(order.SupplierId))
                    orderItemDtos.Add(orderItemDto);
            }
            foreach (var item in orderItems)
            {
                var orderItemDto = orderItemDtos.FirstOrDefault(x => x.ProductId == item.ProductId);
                if (orderItemDto == null)
                {
                    orderItemService.DeleteOrderItem(orderId, item.ProductId);
                }
                else if (orderItemDto.QTYRequierd != item.QTY)
                {
                    item.QTY = orderItemDto.QTYRequierd;
                    orderItemService.UpdateOrderItem(item);
                }
            }
            foreach (var item in orderItemDtos)
            {
                var orderItemDto = orderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                if (orderItemDto == null)
                {
                    await orderItemService.CreateOrderItemAsync(item);
                }
            }
            orderItems = order.OrderItems.Where(x => !x.isDelete);
            order.TotalPrice = order.OrderItems.Sum(x => x.Price * x.QTY);
            dbContext.Orders.Update(order);
            dbContext.SaveChanges();
            var notifi = new Notification()
            {
                OrderId = order.Id,
                FromUserId = order.RestaurantId,
                ToUserId = order.SupplierId,
                DateAdded = DateTime.UtcNow,
                DateReady = null,
                Header = $"Order items and ordered quantities have been updated #{order.Id}",
                Body = $"Order items and ordered quantities have been updated",
                isRead = false,
                URL = $"/Orders/Details/{order.Id}"

            };
            await notificationService.Create(notifi);
            if (orderItems == null || orderItems.Count() < 1) return null;
            var orderItemsViewModel = mapper.Map<IEnumerable<OrderItemViewModel>>(orderItems);
            if (orderItemsViewModel == null || orderItemsViewModel.Count() < 1) return null;
            else return new { data = orderItemsViewModel };

        }

        public async Task<Order?> Cancel(int orderId, string userId)
        {
            var order = await GetOrder(orderId, userId);
            if (order == null || !order.RestaurantId.Equals(userId) || (order.StatusOrder != StatusOrder.Draft
                && order.StatusOrder != StatusOrder.Pending
                && order.StatusOrder != StatusOrder.Processing))
                return null;
            var result = await UpdateStatus(orderId, userId, StatusOrder.Cancelled);
            var notifi = new Notification()
            {
                OrderId = order.Id,
                FromUserId = order.RestaurantId,
                ToUserId = order.SupplierId,
                DateAdded = DateTime.UtcNow,
                DateReady = null,
                Header = $"The order #{order.Id} was canceled by the restaurant",
                Body = $"The order was canceled by the restaurant",
                isRead = false,
                URL = $"/Orders/Details/{order.Id}"

            };
            await notificationService.Create(notifi);
            if (result > 0)
                return order;
            else return null;

        }
        public async Task<Order?> Denied(int orderId, string userId)
        {
            var order = await GetOrder(orderId, userId);
            if (order == null || !order.SupplierId.Equals(userId) || (order.StatusOrder != StatusOrder.Draft
                && order.StatusOrder != StatusOrder.Pending
                && order.StatusOrder != StatusOrder.Delivering
                && order.StatusOrder != StatusOrder.Processing))
                return null;


            var resultFailed = 0;
            var resultDenied = 0;
            if (order.StatusOrder == StatusOrder.Delivering)
                resultFailed = await UpdateStatus(orderId, userId, StatusOrder.Failed);
            else
                resultDenied = await UpdateStatus(orderId, userId, StatusOrder.Denied);

            var notifi = new Notification()
            {
                Body = (resultDenied > 0) ? $"The order#{order.Id} was rejected by the seller" : "Failed to deliver the order",
                DateAdded = DateTime.UtcNow,
                DateReady = null,
                FromUserId = userId,
                ToUserId = order.SupplierId.Equals(userId) ? order.RestaurantId : order.SupplierId,
                Header = (resultDenied > 0) ? $"The order was rejected by the seller" : $"Failed to deliver the order #{order.Id}",
                isRead = false,
                OrderId = order.Id,
                URL = $"/Orders/DetailsForRestaurant/{order.Id}",

            };
            await notificationService.Create(notifi);
            if (resultFailed > 0 || resultDenied > 0)
                return order;
            else return null;

        }


        public async Task<Order?> Delivered(int orderId, string userId)
        {
            var order = await GetOrder(orderId, userId);
            if (order == null || !order.RestaurantId.Equals(userId) || (order.StatusOrder != StatusOrder.Delivering))
                return null;


            var result = await UpdateStatus(orderId, userId, StatusOrder.Delivered);

            var notifi = new Notification()
            {
                Body = $"The order #{order.Id} has been delivered to the customer",
                DateAdded = DateTime.UtcNow,
                DateReady = null,
                FromUserId = userId,
                ToUserId = order.SupplierId.Equals(userId) ? order.RestaurantId : order.SupplierId,
                Header = $"The order #{order.Id} has been delivered to the customer",
                isRead = false,
                OrderId = order.Id,
                URL = $"/Orders/Details/{order.Id}",

            };
            await notificationService.Create(notifi);
            if (result > 0)
                return order;
            else return null;

        }
        public async Task<Order?> DeliverMoney(int orderId, string userId)
        {
            var order = await GetOrder(orderId, userId);
            if (order == null || !order.SupplierId.Equals(userId) || (order.StatusOrder != StatusOrder.Delivered))
                return null;


            var result = await UpdateStatus(orderId, userId, StatusOrder.Completed);

            var notifi = new Notification()
            {
                Body = $"The order #{order.Id} funds have been received and completed",
                DateAdded = DateTime.UtcNow,
                DateReady = null,
                FromUserId = userId,
                ToUserId = order.SupplierId.Equals(userId) ? order.RestaurantId : order.SupplierId,
                Header = $"The order #{order.Id} funds have been received and completed",
                isRead = false,
                OrderId = order.Id,
                URL = $"/Orders/DetailsForRestaurant/{order.Id}",

            };
            await notificationService.Create(notifi);
            if (result > 0)
                return order;
            else return null;

        }

        public async Task<Order?> Delivering(int orderId, string userId)
        {
            var order = await GetOrder(orderId, userId);
            if (order == null || !order.SupplierId.Equals(userId)
                || (order.StatusOrder != StatusOrder.Pending
                && order.StatusOrder != StatusOrder.Processing))
                return null;


            var result = await UpdateStatus(orderId, userId, StatusOrder.Delivering);

            var notifi = new Notification()
            {
                Body = $"The order #{order.Id} has been delivering to the customer",
                DateAdded = DateTime.UtcNow,
                DateReady = null,
                FromUserId = userId,
                ToUserId = order.SupplierId.Equals(userId) ? order.RestaurantId : order.SupplierId,
                Header = $"The order #{order.Id} has been delivering to the customer",
                isRead = false,
                OrderId = order.Id,
                URL = $"/Orders/DetailsForRestaurant/{order.Id}",

            };
            await notificationService.Create(notifi);
            if (result > 0)
                return order;
            else return null;
        }
    }

}