using Microsoft.AspNetCore.Identity;

namespace Domain;

public class ApplicationUser : IdentityUser
{
    public string Firstname { get; set; }

    public string Lastname { get; set; }
}