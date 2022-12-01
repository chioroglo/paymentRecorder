using Common.Models.Base;
using Domain;
using Domain.Enum;

namespace Common.Models;

public class OrderModel : BaseModel
{
    public long Number { get; set; }

    public DateTime EmissionDate { get; set; }

    public decimal Amount { get; set; }

    public CurrencyCode CurrencyCode { get; set; }

    public string CurrencyName { get; set; }

    public string IssuerAccountCode { get; set; }

    public string IssuerFiscalCode { get; set; }

    public string IssuerAgentName { get; set; }

    public string IssuerBankName { get; set; }

    public string IssuerBankCode { get; set; }

    public string BeneficiaryAccountCode { get; set; }

    public string BeneficiaryFiscalCode { get; set; }

    public string BeneficiaryAgentName { get; set; }

    public string BeneficiaryBankName { get; set; }

    public string BeneficiaryBankCode { get; set; }

    public string Destination { get; set; }

    public string TransactionType { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime? ExecutionDate { get; set; }

    public string Timezone { get; set; }

    public string TransactionState { get; set; }
}