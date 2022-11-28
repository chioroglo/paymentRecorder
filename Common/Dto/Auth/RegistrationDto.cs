using System.ComponentModel.DataAnnotations;
using static Common.ValidationConstraints.ApplicationUserValidationConstraints;
using static Common.ValidationConstraints.CommonValidationConstraints;

namespace Common.Dto.Auth;

public class RegistrationDto
{
    [StringLength(FirstnameLastnameMaxLength)]
    public string Firstname { get; set; }

    [StringLength(FirstnameLastnameMaxLength)]
    public string Lastname { get; set; }

    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Username { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [StringLength(PasswordMaxLength,MinimumLength = PasswordMinLength)]
    public string Password { get; set; }
}