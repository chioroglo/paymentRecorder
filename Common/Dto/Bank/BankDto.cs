using System.ComponentModel.DataAnnotations;
using FluentValidation;
using static Common.Validation.Constraints.BankValidationConstraints;
using static Common.Validation.Constraints.CommonValidationConstraints;

namespace Common.Dto.Bank;

public class BankDto
{
    public string Name { get; set; }
    
    public string Code { get; set; }
}


public class BankDtoValidator : AbstractValidator<BankDto>
{
    public BankDtoValidator()
    {
        RuleFor(e => e.Name).MinimumLength(NameMinLength).MaximumLength(NameMaxLength);
        RuleFor(e => e.Code).Matches(BankCodeRegex).Length(BankCodeLengthFixed);
    }
}