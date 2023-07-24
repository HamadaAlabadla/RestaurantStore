using AutoMapper;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Core.ModelViewModels;
using RestauranteStore.EF.Models;
using RestaurantStore.Core.ModelViewModels;

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
				.ForMember(dist => dist.image, src => src.MapFrom(src => src.Logo));


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


			CreateMap<OrderItemDto, OrderItem>();

			CreateMap<OrderDto, Order>();
			CreateMap<Product, OrderItemDto>()
				.ForMember(dist => dist.ProductId, src => src.MapFrom(src => src.ProductNumber))
				.ForMember(dist => dist.ProductName, src => src.MapFrom(src => src.Name))
				.ForMember(dist => dist.Image, src => src.MapFrom(src => src.Image))
				.ForMember(dist => dist.Price, src => src.MapFrom(src => src.Price))
				.ForMember(dist => dist.isDelete, src => src.MapFrom(src => src.isDelete))
				.ForMember(dist => dist.QTY, src => src.MapFrom(src => src.QTY))
				.ForMember(dist => dist.QTYRequierd, src => src.Ignore())
				.ForMember(dist => dist.OrderId, src => src.Ignore());

			CreateMap<Order, OrderListRestaurantViewModel>()
				.ForMember(dist => dist.DateModified, src => src.MapFrom(src => src.DateModified.ToShortDateString()))
				.ForMember(dist => dist.DateCreate, src => src.MapFrom(src => src.DateCreate.ToShortDateString()))
				.ForMember(dist => dist.StatusOrder, src => src.MapFrom(src => src.StatusOrder.ToString()))
				.ForMember(dist => dist.RestaurantName, src => src.MapFrom(src => (src.Restaurant.User??new User() { Name = "undefined"}).Name));
		}
	}
}
