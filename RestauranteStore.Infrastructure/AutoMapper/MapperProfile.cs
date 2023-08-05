using AutoMapper;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Core.ModelViewModels;
using RestauranteStore.EF.Models;
using RestaurantStore.Core.Dtos;
using RestaurantStore.Core.ModelViewModels;
using RestaurantStore.EF.Models;
using static RestauranteStore.Core.Enums.Enums;

namespace RestaurantStore.Infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(dist => dist.Role, src => src.Ignore())
                .ForMember(dist => dist.DateCreateText, src => src.MapFrom(src => src.DateCreate.ToShortDateString()));



            CreateMap<UserDto, User>()
                .ForMember(dist => dist.Id, src => src.MapFrom(src => src.Id))
                .ForMember(dist => dist.Name, src => src.MapFrom(src => (!string.IsNullOrEmpty(src.Name)) ? src.Name : $"{src.FirstName} {src.LastName}"))
                .ForMember(dist => dist.NormalizedEmail, src => src.MapFrom(src => src.Email!.ToUpper()))
                .ForMember(dist => dist.NormalizedUserName, src => src.MapFrom(src => src.UserName!.ToUpper()))
                .ForMember(dist => dist.PhoneNumber, src => src.MapFrom(src => src.PhoneNumber));

            CreateMap<RestaurantDto, User>()
                .ForMember(dist => dist.Id, src => src.Ignore())
                .ForMember(dist => dist.NormalizedEmail, src => src.MapFrom(src => src.Email!.ToUpper()))
                .ForMember(dist => dist.NormalizedUserName, src => src.MapFrom(src => src.UserName!.ToUpper()))
                .ForMember(dist => dist.PhoneNumber, src => src.MapFrom(src => src.PhoneNumber));

            CreateMap<User, UserDto>()
                .ForMember(dist => dist.Logo, src => src.Ignore())
                .ForMember(dist => dist.FirstName, src => src.MapFrom(src => SplitName(src.Name ?? "")[0]))
                .ForMember(dist => dist.LastName, src => src.MapFrom(src => SplitName(src.Name ?? "")[1]))
                .ForMember(dist => dist.image, src => src.MapFrom(src => src.Logo));

            CreateMap<User, EditEmailDto>()
                .ForMember(dist => dist.Password, src => src.Ignore());


            CreateMap<RestaurantDto, Restaurant>()
                .ForMember(dist => dist.UserId, src => src.Ignore())
                .ForMember(dist => dist.User, src => src.Ignore());

            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(dist => dist.UserName, src => src.MapFrom(src => src.User!.UserName))
                .ForMember(dist => dist.PhoneNumber, src => src.MapFrom(src => src.User!.PhoneNumber))
                .ForMember(dist => dist.Name, src => src.MapFrom(src => src.User!.Name))
                .ForMember(dist => dist.Email, src => src.MapFrom(src => src.User!.Email))
                .ForMember(dist => dist.image, src => src.MapFrom(src => src.User!.Logo));

            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.Image, src => src.Ignore())
                .ForMember(dist => dist.ImageTitle, src => src.MapFrom(src => src.Image))
                ;

            CreateMap<ProductDto, Product>()
                .ForMember(dist => dist.Image, src => src.Ignore());

            CreateMap<Product, ProductViewModel>()
                .ForMember(dist => dist.NameShortenQuantityUnit, src => src.MapFrom(src => src.QuantityUnit.shortenQuantity))
                .ForMember(dist => dist.NameCategory, src => src.MapFrom(src => src.Category.Name))
                .ForMember(dist => dist.DateCreate, src => src.MapFrom(src => src.DateCreate.ToShortDateString()))
                .ForMember(dist => dist.NameShortenUnitPrice, src => src.MapFrom(src => src.UnitPrice.ShortenName))
                .ForMember(dist => dist.NameSupplier, src => src.MapFrom(src => src.User.Name));


            CreateMap<OrderItemDto, OrderItem>()
                .ForMember(dist => dist.QTY, src => src.MapFrom(src => src.QTYRequierd))
                .ReverseMap()
                .ForMember(dist => dist.QTYRequierd, src => src.MapFrom(src => src.QTY));

            CreateMap<OrderDto, Order>();
            CreateMap<Product, OrderItemDto>()
                .ForMember(dist => dist.ProductId, src => src.MapFrom(src => src.ProductNumber))
                .ForMember(dist => dist.SupplierId, src => src.MapFrom(src => src.UserId))
                .ForMember(dist => dist.SupplierName, src => src.MapFrom(src => src.User.Name))
                .ForMember(dist => dist.ProductName, src => src.MapFrom(src => src.Name))
                .ForMember(dist => dist.ProductImage, src => src.MapFrom(src => src.Image))
                .ForMember(dist => dist.Image, src => src.MapFrom(src => src.Image))
                .ForMember(dist => dist.Price, src => src.MapFrom(src => src.Price))
                .ForMember(dist => dist.isDelete, src => src.MapFrom(src => src.isDelete))
                .ForMember(dist => dist.QTY, src => src.MapFrom(src => src.QTY))
                .ForMember(dist => dist.QTYRequierd, src => src.Ignore())
                .ForMember(dist => dist.OrderId, src => src.Ignore());

            CreateMap<Order, OrderListSupplierViewModel>()
                .ForMember(dist => dist.DateModified, src => src.MapFrom(src => src.DateModified.ToShortDateString()))
                .ForMember(dist => dist.DateCreate, src => src.MapFrom(src => src.DateCreate.ToShortDateString()))
                .ForMember(dist => dist.StatusOrder, src => src.MapFrom(src => src.StatusOrder.ToString()))
                .ForMember(dist => dist.RestaurantImage, src => src.MapFrom(src => (src.Restaurant.User ?? new User() { Logo = "" }).Logo))
                .ForMember(dist => dist.RestaurantName, src => src.MapFrom(src => (src.Restaurant.User ?? new User() { Name = "undefined" }).Name));

            CreateMap<Order, OrderListRestaurantViewModel>()
                .ForMember(dist => dist.DateModified, src => src.MapFrom(src => src.DateModified.ToShortDateString()))
                .ForMember(dist => dist.DateCreate, src => src.MapFrom(src => src.DateCreate.ToShortDateString()))
                .ForMember(dist => dist.StatusOrder, src => src.MapFrom(src => src.StatusOrder.ToString()))
                .ForMember(dist => dist.SupplierName, src => src.MapFrom(src => (src.Supplier ?? new User() { Name = "" }).Name))
                .ForMember(dist => dist.SupplierImage, src => src.MapFrom(src => (src.Supplier ?? new User() { Logo = "undefined" }).Logo));


            CreateMap<Order, OrderViewModel>()
                .ForMember(dist => dist.DateModified, src => src.MapFrom(src => src.DateModified.ToShortDateString()))
                .ForMember(dist => dist.DateCreate, src => src.MapFrom(src => src.DateCreate.ToShortDateString()))
                .ForMember(dist => dist.OrderDate, src => src.MapFrom(src => src.OrderDate.ToShortDateString()))
                .ForMember(dist => dist.SupplierImage, src => src.MapFrom(src => src.Supplier.Logo))
                .ForMember(dist => dist.SupplierName, src => src.MapFrom(src => src.Supplier.Name))
                .ForMember(dist => dist.SupplierEmail, src => src.MapFrom(src => src.Supplier.Email))
                .ForMember(dist => dist.SupplierPhoneNumber, src => src.MapFrom(src => src.Supplier.PhoneNumber))
                .ForMember(dist => dist.RestaurantEmail, src => src.MapFrom(src => (src.Restaurant.User ?? new User() { Email = "undefined" }).Email))
                .ForMember(dist => dist.RestaurantImage, src => src.MapFrom(src => (src.Restaurant.User ?? new User() { Logo = "undefined" }).Logo))
                .ForMember(dist => dist.RestaurantPhoneNumber, src => src.MapFrom(src => (src.Restaurant.User ?? new User() { PhoneNumber = "undefined" }).PhoneNumber))
                .ForMember(dist => dist.RestaurantName, src => src.MapFrom(src => (src.Restaurant.User ?? new User() { Name = "undefined" }).Name));

            CreateMap<Order, OrderDetailsViewModel>()
                .ForMember(dist => dist.PaymentMethod, src => src.MapFrom(src => src.PaymentMethod.ToString()))
                .ForMember(dist => dist.StatusOrder, src => src.MapFrom(src => src.StatusOrder.ToString()))
                .ForMember(dist => dist.DateAdded, src => src.MapFrom(src => src.OrderDate.ToShortDateString()));

            CreateMap<Order, OrderDetailsDto>()
                .ForMember(dist => dist.IsDraft, src => src.MapFrom(src => (src.StatusOrder == StatusOrder.Draft) ? true : false));

            CreateMap<User, UserDetailsViewModel>()
                .ForMember(dist => dist.Phone, src => src.MapFrom(src => src.PhoneNumber))
                .ForMember(dist => dist.Image, src => src.MapFrom(src => src.Logo ?? "undefined"));

            CreateMap<Order, PaymentDetailsViewModel>();
            CreateMap<Order, EditPaymentDetailsDto>();
            CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(dist => dist.ProductImage, src => src.MapFrom(src => src.Product.Image))
                .ForMember(dist => dist.ProductName, src => src.MapFrom(src => src.Product.Name))
                .ForMember(dist => dist.ProductId, src => src.MapFrom(src => src.Product.ProductNumber))
                .ForMember(dist => dist.QTYRequierd, src => src.MapFrom(src => src.QTY))
                .ForMember(dist => dist.DateModified, src => src.MapFrom(src => src.Order.DateModified.ToShortDateString()))
                .ForMember(dist => dist.OrderDate, src => src.MapFrom(src => src.Order.OrderDate.ToShortDateString()))
                .ForMember(dist => dist.DateCreate, src => src.MapFrom(src => src.Order.DateCreate.ToShortDateString()))
                .ForMember(dist => dist.Price, src => src.MapFrom(src => src.Price));


            CreateMap<Notification, NotificationViewModel>()
                .ForMember(dist => dist.FromUserImage, src => src.MapFrom(src => src.FromUser.Logo))
                .ForMember(dist => dist.FromUserName, src => src.MapFrom(src => src.FromUser.Name))
                .ForMember(dist => dist.DateAddedAgo, src => src.MapFrom(src => GetDateAgo(src.DateAdded)))
                .ForMember(dist => dist.DateReady, src => src.MapFrom(src => (src.DateReady ?? DateTime.UtcNow).ToShortDateString()));

        }



        private string[] SplitName(string name = "")
        {
            var splitArray = new string[2] { "", "" };
            var result = name.Split(' ');
            switch (result.Length)
            {
                case 1:
                    splitArray[0] = result[0]; break;
                case 2:
                    splitArray[0] = result[0];
                    splitArray[1] = result[1]; break;
            }
            return splitArray;

        }
        private string GetDateAgo(DateTime Date)
        {
            var dateAgo = "";
            var diffTime = (DateTime.UtcNow - Date);
            int second = (int)diffTime.TotalSeconds;
            int mins = (int)diffTime.TotalMinutes;
            int hr = (int)diffTime.TotalHours;
            int days = (int)diffTime.TotalDays;
            if (second < 60)
                dateAgo = $"{second} sec ago";
            else if (mins < 60)
                dateAgo = $"{mins} mins ago";
            else if (hr < 24)
                dateAgo = $"{hr} hr ago";
            else
                dateAgo = $"{days} day ago";
            return dateAgo;
        }
    }
}
