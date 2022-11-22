using AutoMapper;
using Common.Dto;
using Common.Models;
using Domain;

namespace Common.MappingProfiles;

public class AgentProfile : Profile
{
    public AgentProfile()
    {
        CreateMap<AgentDto, Agent>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.FiscalCode, opt => opt.MapFrom(src => src.FiscalCode))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.FromBase64String(src.Version)));

        CreateMap<Agent, AgentModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.FiscalCode, opt => opt.MapFrom(src => src.FiscalCode))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.NumberOfAccounts, opt => opt.MapFrom(src => src.Accounts.Count))
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version));
    }
}