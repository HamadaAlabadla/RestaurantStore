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

			CreateMap<User, UserDto>()
				.ForMember(dist => dist.Logo, src => src.Ignore());




		}
	}
}
