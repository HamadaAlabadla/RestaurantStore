using AutoMapper;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Core.ModelViewModels;
using RestauranteStore.EF.Models;

namespace RestauranteStore.Core.AutoMapper
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<User, UserViewModel>()
				.ForMember(dist => dist.Role, src => src.Ignore())
				.ForMember(dist => dist.DateCreateText, src => src.MapFrom(src => src.DateCreate.ToShortDateString()));



			CreateMap<UserDto, User>()
				.ForMember(dist => dist.Id, src => src.Ignore())
				.ForMember(dist => dist.NormalizedEmail, src => src.MapFrom(src => src.Email!.ToUpper()))
				.ForMember(dist => dist.NormalizedUserName, src => src.MapFrom(src => src.UserName!.ToUpper()))
				.ForMember(dist => dist.PhoneNumber, src => src.MapFrom(src => src.PhoneNumber));
			
			CreateMap<RestoranteDto, User>()
				.ForMember(dist => dist.Id, src => src.Ignore())
				.ForMember(dist => dist.NormalizedEmail, src => src.MapFrom(src => src.Email!.ToUpper()))
				.ForMember(dist => dist.NormalizedUserName, src => src.MapFrom(src => src.UserName!.ToUpper()))
				.ForMember(dist => dist.PhoneNumber, src => src.MapFrom(src => src.PhoneNumber));

			CreateMap<User, UserDto>()
				.ForMember(dist => dist.Logo, src => src.Ignore());


			CreateMap<RestoranteDto,Restorante>()
				.ForMember(dist => dist.UserId, src => src.Ignore())
				.ForMember(dist => dist.User, src => src.Ignore());
			CreateMap<Product, ProductDto>()
				.ForMember(dist => dist.Image, src => src.Ignore());

			CreateMap<ProductDto, Product>()
				.ForMember(dist => dist.Image, src => src.Ignore());

			CreateMap<Product, ProductViewModel>()
				.ForMember(dist => dist.NameShortenQuantityUnit, src => src.MapFrom(src => src.QuantityUnit.shortenQuantity))
				.ForMember(dist => dist.NameCategory, src => src.MapFrom(src => src.Category.Name))
				.ForMember(dist => dist.DateCreate, src => src.MapFrom(src => src.DateCreate.ToShortDateString()))
				.ForMember(dist => dist.NameShortenUnitPrice, src => src.MapFrom(src => src.UnitPrice.ShortenName))
				.ForMember(dist => dist.NameSupplier, src => src.MapFrom(src => src.User.Name));

		}
	}
}
