namespace Common.Exceptions.ExceptionMessages;

public static class IdentityExceptionMessages
{
    public static string RegistrationFailedBecause(string reason) => $"Registration failed, because {reason}";

    public static string AuthenticationFailedMessage() => "Authentication failed, check username or password";
}