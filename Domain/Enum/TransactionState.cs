using System.ComponentModel;

namespace Domain.Enum;

public enum TransactionState
{
    [Description("Completed")]
    Completed = 0,
    [Description("Pending")]
    Pending = 1,
    [Description("Cancelled")]
    Cancelled = 2
}