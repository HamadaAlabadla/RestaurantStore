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

			CreateMap<Admin, AdminViewModel>()
				.ForMember(dist => dist.Id, src => src.MapFrom(src => src.Id))
				.ForMember(dist => dist.DateCreate, src => src.MapFrom(src => src.DateCreate))
				.ForMember(dist => dist.Logo, src => src.MapFrom(src => src.Logo))
				.ForMember(dist => dist.UserTypeText, src => src.MapFrom(src => src.User!.UserType.ToString()))
				.ForMember(dist => dist.DateCreateText, src => src.MapFrom(src => src.DateCreate.ToShortDateString()))
				.ForMember(dist => dist.Email, src => src.MapFrom(src => src.User!.Email))
				.ForMember(dist => dist.UserName, src => src.MapFrom(src => src.User!.UserName))
				.ForMember(dist => dist.PhoneNumber, src => src.MapFrom(src => src.User!.PhoneNumber));

			CreateMap<AdminDto, Admin>()
				.ForMember(dist => dist.Id, src => src.MapFrom(src => src.Id));
			//.ForMember(dist => dist.Logo, src => src.Ignore())
			//.ForMember(dist => dist.UserId, src => src.Ignore());

			CreateMap<Admin, AdminDto>()
				.ForMember(dist => dist.Id, src => src.MapFrom(src => src.Id))
				.ForMember(dist => dist.Logo, src => src.Ignore())
				.ForMember(dist => dist.UserName, src => src.MapFrom(src => src.User!.UserName))
				.ForMember(dist => dist.UserType, src => src.MapFrom(src => src.User!.UserType))
				.ForMember(dist => dist.PhoneNumber, src => src.MapFrom(src => src.User!.PhoneNumber))
				.ForMember(dist => dist.Email, src => src.MapFrom(src => src.User!.Email));



			CreateMap<AdminDto, User>()
				.ForMember(dist => dist.Id, src => src.Ignore())
				.ForMember(dist => dist.Email, src => src.MapFrom(src => src.Email))
				.ForMember(dist => dist.NormalizedEmail, src => src.MapFrom(src => src.Email!.ToUpper()))
				.ForMember(dist => dist.UserName, src => src.MapFrom(src => src.UserName))
				.ForMember(dist => dist.UserType, src => src.MapFrom(src => src.UserType))
				.ForMember(dist => dist.NormalizedUserName, src => src.MapFrom(src => src.UserName!.ToUpper()))
				.ForMember(dist => dist.PhoneNumber, src => src.MapFrom(src => src.PhoneNumber));
			CreateMap<User, User>()
				.ForMember(dist => dist.Id, src => src.Ignore());
		}
	}
}
