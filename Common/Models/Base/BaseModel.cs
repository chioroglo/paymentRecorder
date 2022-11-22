namespace Common.Models.Base;

public abstract class BaseModel
{
    public long Id { get; set; }

    public Guid Version { get; set; }
}