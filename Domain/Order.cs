using Domain.Abstract;
using Domain.Enum;

namespace Domain;

public class Order : BaseEntity
{
    public long Number { get; set; }

    public decimal Amount { get; set; }

    public CurrencyCode CurrencyCode { get; set; }

    public string Destination { get; set; }

    public Account IssuerAccount { get; set; }

    public long IssuerAccountId { get; set; }

    public Account BeneficiaryAccount { get; set; }

    public long BeneficiaryAccountId { get; set; }

    public DateTime EmissionDate { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime? ExecutionDate { get; set; }
    
    public string Timezone { get; set; }

    public Transaction Transaction { get; set; }

}