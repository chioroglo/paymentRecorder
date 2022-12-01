namespace Common.Validation.ValidationConstraints;

public static class BankValidationConstraints
{
    public const int BankCodeLengthFixed = 8;

    public const string BankCodeRegex = @"[A-Z]{6}[A-Z0-9]{2}";
}