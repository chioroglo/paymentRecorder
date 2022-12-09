namespace Common.Exceptions.ExceptionMessages;

public static class AuthorizationExceptionMessages
{
    public static string InvalidTokenMessage()
    {
        return $"Token processing has failed, because token was invalid";
    }
}