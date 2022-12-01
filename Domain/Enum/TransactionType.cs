using System.ComponentModel;

namespace Domain.Enum;

public enum TransactionType
{
    [Description("Regular")] Regular = 0,
    [Description("Urgent")] Urgent = 1
}