using Domain.Abstract;

namespace Domain;

public class Bank : BaseEntity
{
    public string Name { get; set; }

    public string Code { get; set; }

    public ICollection<Account> Accounts { get; set; }
}