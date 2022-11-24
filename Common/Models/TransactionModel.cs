using Domain.Abstract;
using Domain.Enum;

namespace Common.Models;

public class TransactionModel : BaseEntity
{

    public long OrderNumber { get; set; }
    
    public TransactionType TransactionTypeId { get; set; }

    public string TransactionType { get; set; }
    
    public TransactionState TransactionStateId { get; set; }

    public string TransactionState { get; set; }
}