namespace Common.Exceptions.ExceptionMessages;

public static class IdentityExceptionMessages
{
    public static string RegistrationFailedBecause(string reason)
    {
        return $"Registration failed, because {reason}";
    }

    public static string AuthenticationFailedMessage()
    {
        return "Authentication failed, check username or password";
    }

    public static string InvalidTokenMessage()
    {
        return $"Token processing has failed, because token was invalid";
    }
}