using System.Data;
using FluentValidation;
using FluentValidation.Validators;

namespace Common.Dto.Auth;

public class LoginDto
{

    public string Email { get; set; }

    public string Password { get; set; }
}

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(dto => dto.Email).EmailAddress(EmailValidationMode.AspNetCoreCompatible);
        RuleFor(dto => dto.Password)
            .MinimumLength(Common.ValidationConstraints.ApplicationUserValidationConstraints.PasswordMinLength);
    }
}