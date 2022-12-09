namespace Common.Exceptions;

public class AuthenticationException : System.Exception
{
    public AuthenticationException(string message) : base(message)
    {
        
    }
}