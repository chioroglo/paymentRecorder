using AutoMapper;
using Common.Dto.Account;
using Common.Models;
using Domain;

namespace Common.MappingProfiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountDto, Account>()
            .ForMember(dest => dest.AccountCode, opt => opt.MapFrom(src => src.AccountCode))
            .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.AgentId))
            .ForMember(dest => dest.BankId, opt => opt.MapFrom(src => src.BankId));

        CreateMap<Account, AccountModel>()
            .ForMember(dest => dest.AccountCode, opt => opt.MapFrom(src => src.AccountCode))
            .ForMember(dest => dest.AgentFiscalCode, opt => opt.MapFrom(src => src.Agent.FiscalCode))
            .ForMember(dest => dest.AmountOfIncomingOrders, opt => opt.MapFrom(src => src.IncomingOrders.Count))
            .ForMember(dest => dest.AmountOfOutcomingOrders, opt => opt.MapFrom(src => src.OutcomingOrders.Count))
            .ForMember(dest => dest.BankCode, opt => opt.MapFrom(src => src.Bank.Code))
            .ForMember(dest => dest.BankName, opt => opt.MapFrom(src => src.Bank.Name))
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Agent.Name));
    }
}