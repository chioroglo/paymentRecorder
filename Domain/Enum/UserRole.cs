using System.ComponentModel;

namespace Domain.Enum;

public enum UserRole
{
    [Description("Accountant")] Accountant = 0,
    [Description("System Administrator")] SystemAdministrator,
    [Description("Manager")] Manager
}