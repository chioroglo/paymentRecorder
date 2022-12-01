using System.ComponentModel;

namespace Domain.Enum;

public enum LegalAgentType
{
    [Description("Physical person")] Physical = 0,

    [Description("Juridical person")] Juridical = 1
}