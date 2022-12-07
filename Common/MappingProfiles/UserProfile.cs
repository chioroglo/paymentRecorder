using System.Runtime.InteropServices;
using AutoMapper;
using Common.Dto.Auth;
using Common.Models.Auth;

namespace Common.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AuthenticationServiceResponseDto, AuthenticationResponseModel>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles))
            .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.AccessToken))
            .ForMember(dest => dest.AccessTokenExpirationDate,
                opt => opt.MapFrom(src => src.AccessTokenExpirationDate));
    }
}