using Common.Models.Base;

namespace Common.Models;

public class OrderModel : BaseModel
{
    public long Number { get; set; }

    public decimal Amount { get; set; }

    public CurrencyCode CurrencyCode { get; set; }

    public string Destination { get; set; }

    public AccountModel IssuerAccount { get; set; }

    public AccountModel BeneficiaryAccount { get; set; }

    public DateTime EmissionDate { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime? ExecutionDate { get; set; }

    public string Timezone { get; set; }

    public string TransactionState { get; set; }
    
    public string TransactionType { get; set; }
}