using Common.Validation.ValidationConstraints;
using Common.Validation.Validators;
using Domain.Enum;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Common.Dto;

public class OrderDto
{
    public long Number { get; set; }
    
    public DateTime EmissionDate { get; set; }

    public decimal Amount { get; set; }

    public CurrencyCode CurrencyCode { get; set; }

    public string IssuerAccountCode { get; set; }

    public long IssuerFiscalCode { get; set; }
    
    public string BeneficiaryAccountCode { get; set; }

    public long BeneficiaryFiscalCode { get; set; }

    public string Destination { get; set; }

    public TransactionType TransactionType { get; set; }
    
    public TransactionState TransactionState { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime? ExecutionDate { get; set; }
    
    public string Timezone { get; set; }
}

public class OrderDtoValidator : AbstractValidator<OrderDto>
{
    public OrderDtoValidator()
    {
        RuleFor(e => e.CurrencyCode).IsInEnum();
        RuleFor(e => e.TransactionState).IsInEnum();
        RuleFor(e => e.TransactionType).IsInEnum();
        RuleFor(e => e.Destination).MaximumLength(OrderValidationConstraints.DestinationMaxLength);
        RuleFor(e => e.IssuerAccountCode).Length(AccountValidationConstraints.CodeLengthFixed);
        RuleFor(e => e.BeneficiaryAccountCode).Length(AccountValidationConstraints.CodeLengthFixed);

        RuleFor(e => e.Timezone).IsValidWindowsTimezone();
    }
}