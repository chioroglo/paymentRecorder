namespace Common.Exceptions.ExceptionMessages;

public static class TokenExceptionMessages
{
    public static string InvalidTokenMessage()
    {
        return $"Token processing has failed, because token was invalid";
    }

    public static string TokenExpiredMessage()
    {
        return $"This session is expired. Please log in";
    }
}