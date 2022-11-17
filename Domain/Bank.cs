using Domain.Abstract;

namespace Domain;

public class Bank : BaseEntity
{
    public string Name { get; set; }

    public string Code { get; set; }
}