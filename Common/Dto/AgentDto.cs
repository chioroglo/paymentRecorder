using Domain.Enum;

namespace Common.Dto;

public class AgentDto
{
    public string Name { get; set; }

    public LegalAgentType Type { get; set; }

    public long FiscalCode { get; set; }
}