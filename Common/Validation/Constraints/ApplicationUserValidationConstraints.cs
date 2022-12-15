namespace Common.Validation.Constraints;

public static class ApplicationUserValidationConstraints
{
    public const int FirstnameLastnameMaxLength = 20;

    public const int PasswordMinLength = 6;

    public const int RefreshTokenLengthFixed = 128;
}