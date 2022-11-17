using Domain.Abstract;

namespace Domain;

public class Order : BaseEntity
{
    public long Number { get; set; }

    public decimal Amount { get; set; }

    public string CurrencyCode { get; set; }

    public string Destination { get; set; }

    public Account RecipientAccount { get; set; }

    public long RecipientAccountId { get; set; }

    public Account BeneficiaryAccount { get; set; }

    public long BeneficiaryAccountId { get; set; }

    public DateTime EmissionDate { get; set; }

    public DateTime ReceiptDate { get; set; }

    public DateTime? ExecutionDate { get; set; }

}