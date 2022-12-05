using System.ComponentModel.DataAnnotations;

namespace Domain.Abstract;

public class BaseEntity
{
    public long Id { get; set; }

    [ConcurrencyCheck] public Guid Version { get; set; }
}