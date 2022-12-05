namespace Common.Exceptions;

public class EntityValidationException : System.Exception
{
    public EntityValidationException(string message) : base(message)
    {
    }
}