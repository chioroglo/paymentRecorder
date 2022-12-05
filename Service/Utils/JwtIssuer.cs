using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Common.Jwt;
using Common.Validation.ValidationConstraints;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Service.Utils;

public static class JwtIssuer
{
    public static async Task<JwtSecurityToken> CreateAccessTokenConformAppsettings<TUser>(
        UserManager<TUser> userManager,
        TUser user, JWTConfigurationFromAppsettingsJson configuration) where TUser : IdentityUser
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);

        var roleClaims = roles.Select(role => new Claim("roles", role)).ToList();

        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        }.Union(userClaims).Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Key));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            configuration.Issuer,
            configuration.Audience,
            claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddMinutes(configuration.AccessTokenLifetimeMinutes));

        return token;
    }

    public static async Task<RefreshToken> CreateUniqueRefreshToken(UserManager<ApplicationUser> userManager,
        ApplicationUser user, JWTConfigurationFromAppsettingsJson configuration, CancellationToken cancellationToken)
    {
        var randomNumber = new byte[ApplicationUserValidationConstraints.RefreshTokenLengthFixed];

        using (var randomNumberGenerator = RandomNumberGenerator.Create())
        {
            randomNumberGenerator.GetBytes(randomNumber);
            var refreshToken = Convert.ToBase64String(randomNumber);

            if (await userManager.Users.FirstOrDefaultAsync(e => e.RefreshToken == refreshToken, cancellationToken) !=
                null)
            {
                return await CreateUniqueRefreshToken(userManager, user, configuration, cancellationToken);
            }

            return new RefreshToken
            {
                Token = refreshToken,
                ExpirationDate = DateTime.UtcNow.AddDays(configuration.RefreshTokenLifetimeInDays)
            };
        }
    }
}