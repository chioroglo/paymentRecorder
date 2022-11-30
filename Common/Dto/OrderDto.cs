using Domain.Enum;

namespace Common.Dto;

public class OrderDto
{
    public long Number { get; set; }
    
    public DateTime EmissionDate { get; set; }

    public decimal Amount { get; set; }

    public CurrencyCode CurrencyCode { get; set; }

    public string IssuerAccountCode { get; set; }

    public long IssuerFiscalCode { get; set; }
    
    public string BeneficiaryAccountCode { get; set; }

    public long BeneficiaryFiscalCode { get; set; }

    public string Destination { get; set; }

    public TransactionType TransactionType { get; set; }
    
    public TransactionState TransactionState { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime? ExecutionDate { get; set; }
    
    public string Timezone { get; set; }
}