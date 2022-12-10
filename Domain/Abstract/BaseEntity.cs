using System.ComponentModel.DataAnnotations;

namespace Domain.Abstract;

public abstract class BaseEntity : IBaseEntity
{
    public long Id { get; set; }

    [ConcurrencyCheck] public Guid Version { get; set; }
}