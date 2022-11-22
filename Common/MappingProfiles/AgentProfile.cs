using AutoMapper;
using Common.Dto;
using Common.Models;
using Domain;

namespace Common.MappingProfiles;

public class AgentProfile : Profile
{
    public AgentProfile() : base()
    {
        CreateMap<AgentDto, Agent>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.FiscalCode, opt => opt.MapFrom(src => src.FiscalCode))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

        CreateMap<Agent, AgentModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.FiscalCode, opt => opt.MapFrom(src => src.FiscalCode))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.NumberOfAccounts, opt => opt.MapFrom(src => src.Accounts.Count));
    }
}