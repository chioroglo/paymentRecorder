using Domain.Abstract;
using Domain.Enum;

namespace Domain;

public class Transaction : BaseEntity
{
    public Order Order { get; set; }

    public long OrderId { get; set; }

    public TransactionType TransactionType { get; set; }

    public TransactionState TransactionState { get; set; }
}