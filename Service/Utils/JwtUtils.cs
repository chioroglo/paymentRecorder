using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.Jwt;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Service.Extensions;

public class JwtUtils
{
    public static async Task<JwtSecurityToken> CreateJwtTokenConformAppsettings<TUser>(UserManager<TUser> userManager,TUser user, JWTConfigurationFromAppsettingsJson configuration) where TUser : IdentityUser
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();

        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles",role));
        }

        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim(JwtTokenClaimNames.UserId,user.Id)
        }
            .Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Key));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration.Issuer,
            audience: configuration.Audience, 
            claims: claims, 
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddDays(configuration.DurationInDays));

        return token;
    }
}