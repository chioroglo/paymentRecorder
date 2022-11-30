using AutoMapper;
using Common.Extensions;
using Common.Models;
using Domain;

namespace Common.MappingProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderModel>()
            .ForMember(dest => dest.BeneficiaryAccount, opt => opt.MapFrom(src => src.BeneficiaryAccount))
            .ForMember(dest => dest.IssuerAccount, opt => opt.MapFrom(src => src.IssuerAccount))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(dest => dest.Number))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.CurrencyCode))
            .ForMember(dest => dest.CurrencyName, opt => opt.MapFrom(src => src.CurrencyCode.GetEnumDescription()))
            .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Destination))
            .ForMember(dest => dest.EmissionDate, opt => opt.MapFrom(src => src.EmissionDate))
            .ForMember(dest => dest.IssueDate, opt => opt.MapFrom(src => src.IssueDate))
            .ForMember(dest => dest.ExecutionDate, opt => opt.MapFrom(src => src.ExecutionDate))
            .ForMember(dest => dest.Timezone, opt => opt.MapFrom(src => src.Timezone))
            .ForMember(dest => dest.TransactionState,
                opt => opt.MapFrom(src => src.Transaction.TransactionState.GetEnumDescription()))
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.Transaction.TransactionType));
    }
}