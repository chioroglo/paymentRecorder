using FluentValidation;

namespace Common.Dto.Auth;

public class LoginDto
{

    public string EmailOrUsername { get; set; }

    public string Password { get; set; }
}

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(dto => dto.Password)
            .MinimumLength(ValidationConstraints.ApplicationUserValidationConstraints.PasswordMinLength);
    }
}