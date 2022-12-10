namespace Common.Exceptions.ExceptionMessages;

public static class AuthenticationExceptionMessages
{
    public static string InvalidTokenMessage()
    {
        return $"Token processing has failed, because token was invalid";
    }
}