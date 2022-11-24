using Common.Models.Base;
using Domain.Enum;

namespace Common.Models;

public class AgentModel : BaseModel
{
    public string Name { get; set; }

    public LegalAgentType TypeId { get; set; }
    
    public string Type { get; set; }

    public long FiscalCode { get; set; }

    public long NumberOfAccounts { get; set; }
}