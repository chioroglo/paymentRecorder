using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Common.Exceptions;
using Common.Jwt;
using Common.Validation.ValidationConstraints;
using Data.Migrations;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Abstract;
using static Common.Exceptions.ExceptionMessages.IdentityExceptionMessages;
using static Common.Validation.ValidationConstraints.ApplicationUserValidationConstraints;

namespace Service;

public class TokenService : ITokenService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JWTConfigurationFromAppsettingsJson _jwtConfiguration;

    public TokenService(UserManager<ApplicationUser> userManager,
        IOptions<JWTConfigurationFromAppsettingsJson> jwtConfiguration)
    {
        _userManager = userManager;
        _jwtConfiguration = jwtConfiguration.Value;
    }

    public async Task<JwtSecurityToken> CreateAccessToken(ApplicationUser user, CancellationToken cancellationToken)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = roles.Select(role => new Claim("roles", role)).ToList();

        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        }.Union(userClaims).Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtConfiguration.Issuer,
            _jwtConfiguration.Audience,
            claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddMinutes(_jwtConfiguration.AccessTokenLifetimeMinutes));

        return token;
    }

    public async Task<RefreshToken> CreateUniqueRefreshTokenAsync(CancellationToken cancellationToken)
    {
        const string alphabatForToken = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_";
        var data = new byte[RefreshTokenLengthFixed];


        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(data);
        }

        var tokenBuilder = new StringBuilder(RefreshTokenLengthFixed);

        Array.ForEach(data, b => tokenBuilder.Append(alphabatForToken[b % alphabatForToken.Length]));

        var token = tokenBuilder.ToString();

        if (await _userManager.Users.FirstOrDefaultAsync(e => e.RefreshToken == token, cancellationToken) != null)
        {
            return await CreateUniqueRefreshTokenAsync(cancellationToken);
        }

        return new RefreshToken
        {
            Token = token,
            ExpirationDate = DateTime.UtcNow.AddDays(_jwtConfiguration.RefreshTokenLifetimeInDays)
        };
    }

    public async Task<RefreshToken> ExchangeRefreshToken(string oldRefreshToken, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(e => e.RefreshToken == oldRefreshToken,
            cancellationToken);

        if (user == null || user.RefreshTokenExpirationDate < DateTime.UtcNow)
        {
            throw new IdentityException(InvalidTokenMessage());
        }

        var newRefreshToken = await CreateUniqueRefreshTokenAsync(cancellationToken);

        user.RefreshToken = newRefreshToken.Token;
        user.RefreshTokenExpirationDate = newRefreshToken.ExpirationDate;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            throw new IdentityException("token issuing problem occurred");
        }

        return newRefreshToken;
    }

    public async Task<AccessToken> GetAccessToken(string refreshToken, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(e => e.RefreshToken == refreshToken, cancellationToken);

        if (user == null || user.RefreshTokenExpirationDate < DateTime.UtcNow)
        {
            throw new IdentityException(InvalidTokenMessage());
        }

        var token = await CreateAccessToken(user, cancellationToken);

        return new AccessToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpirationDate = token.ValidTo
        };
    }
}