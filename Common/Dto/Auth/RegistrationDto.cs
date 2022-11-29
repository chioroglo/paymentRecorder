using System.ComponentModel.DataAnnotations;
using FluentValidation;
using static Common.ValidationConstraints.ApplicationUserValidationConstraints;
using static Common.ValidationConstraints.CommonValidationConstraints;

namespace Common.Dto.Auth;

public class RegistrationDto
{
    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }
    
    public string Password { get; set; }
}

public class RegistrationDtoValidator : AbstractValidator<RegistrationDto>
{
    public RegistrationDtoValidator()
    {
        RuleFor(e => e.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull();

        RuleFor(e => e.Firstname)
            .NotNull()
            .NotEmpty()
            .MaximumLength(FirstnameLastnameMaxLength);

        RuleFor(e => e.Lastname)
            .NotNull()
            .NotEmpty()
            .MaximumLength(FirstnameLastnameMaxLength);

        RuleFor(e => e.Username)
            .MinimumLength(NameMinLength)
            .MaximumLength(NameMaxLength)
            .NotNull();

        RuleFor(e => e.Password)
            .NotNull()
            .MinimumLength(PasswordMinLength);

    }
}