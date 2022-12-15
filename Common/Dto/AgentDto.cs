using System.ComponentModel.DataAnnotations;
using Domain.Enum;
using static Common.Validation.Constraints.CommonValidationConstraints;

namespace Common.Dto;

public class AgentDto
{
    [MaxLength(NameMaxLength)]
    [MinLength(NameMinLength)]
    public string Name { get; set; }

    [EnumDataType(typeof(LegalAgentType))] public LegalAgentType Type { get; set; }

    [RegularExpression("^\\d{13}$")] public long FiscalCode { get; set; }
}