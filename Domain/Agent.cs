using Domain.Abstract;
using Domain.Enum;

namespace Domain;

public class Agent : BaseEntity
{
    public string Name { get; set; }

    public LegalAgentType Type { get; set; }

    public long FiscalCode { get; set; }
}