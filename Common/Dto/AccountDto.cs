using System.ComponentModel.DataAnnotations;
using static Common.Validation.ValidationConstraints.AccountValidationConstraints;

namespace Common.Dto;

public class AccountDto
{
    [StringLength(CodeLengthFixed,MinimumLength = CodeLengthFixed)]
    public string AccountCode { get; set; }

    [Range(0,long.MaxValue)]
    public long AgentId { get; set; }

    [Range(0, long.MaxValue)]
    public long BankId { get; set; }
}