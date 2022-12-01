using FluentValidation;

namespace Common.Validation.Validators;

public static class ValidTimezoneRuleBuilderExtension
{
    public static IRuleBuilder<T, string> IsValidWindowsTimezone<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        // Timezones are stored as Windows timezone
        // Check here for list https://learn.microsoft.com/en-us/windows-hardware/manufacture/desktop/default-time-zones?view=windows-11
        // or call TimeZoneInfo.GetSystemTimeZones();

        return ruleBuilder.Must(timezoneInDto =>
                TimeZoneInfo.GetSystemTimeZones().Count(timezone => timezone.Id == timezoneInDto) > 0)
            .WithMessage("Invalid timezone ( Please specify Windows format)");
    }
}