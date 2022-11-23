using Domain.Abstract;

namespace Common.Models;

public class TransactionModel : BaseEntity
{

    public long OrderNumber { get; set; }
    
    public string TransactionType { get; set; }
    
    public string TransactionState { get; set; }
}