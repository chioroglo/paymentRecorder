using System.Security.Cryptography;
using System.Text;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Service.Utils;

public class PasswordHasherHmacSha512 : IPasswordHasher<ApplicationUser>
{
    public string HashPassword(ApplicationUser user, string password)
    {
        var passwordBytesUtf8 = Encoding.UTF8.GetBytes(password);

        using (var algorithm = SHA512.Create())
        {
            var hash = algorithm.ComputeHash(passwordBytesUtf8);
            return Convert.ToBase64String(hash);
        }
    }

    public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword,
        string providedPassword)
    {
        return HashPassword(user, providedPassword) == hashedPassword
            ? PasswordVerificationResult.Success
            : PasswordVerificationResult.Failed;
    }
}