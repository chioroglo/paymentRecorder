using Common.Models.Base;

namespace Common.Models;

public class AccountModel : BaseModel
{
    public string AccountCode { get; set; }

    public string OwnerName { get; set; }

    public long AgentFiscalCode { get; set; }

    public string BankName { get; set; }

    public string BankCode { get; set; }

    public long AmountOfOutcomingOrders { get; set; }

    public long AmountOfIncomingOrders { get; set; }
}