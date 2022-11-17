using Domain.Abstract;

namespace Domain;

public class Account : BaseEntity
{
    public string Code { get; set; }

    public Agent Agent { get; set; }

    public long AgentId { get; set; }

    public Bank Bank { get; set; }

    public long BankId { get; set; }
}