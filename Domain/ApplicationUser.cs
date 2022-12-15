using Domain.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class ApplicationUser : IdentityUser, IBaseEntity
{
    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpirationDate { get; set; }
}