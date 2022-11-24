using AutoMapper;
using Common.Dto;
using Common.Models;
using Domain;

namespace Common.MappingProfiles;

public class BankProfile : Profile
{
    public BankProfile()
    {
        CreateMap<BankDto, Bank>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code));

        CreateMap<Bank, BankModel>()
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}