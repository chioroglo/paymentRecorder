using Domain.Abstract;

namespace Domain;

public class Account : BaseEntity
{
    public string AccountCode { get; set; }

    public Agent Agent { get; set; }

    public long AgentId { get; set; }

    public Bank Bank { get; set; }

    public long BankId { get; set; }

    public ICollection<Order> IncomingOrders { get; set; }

    public ICollection<Order> OutcomingOrders { get; set; }

}