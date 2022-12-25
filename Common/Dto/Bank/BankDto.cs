using System.ComponentModel.DataAnnotations;
using static Common.Validation.Constraints.BankValidationConstraints;
using static Common.Validation.Constraints.CommonValidationConstraints;

namespace Common.Dto.Bank;

public class BankDto
{
    [MinLength(NameMinLength)]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; }

    [RegularExpression(BankCodeRegex)]
    [StringLength(BankCodeLengthFixed, MinimumLength = BankCodeLengthFixed)]
    public string Code { get; set; }
}