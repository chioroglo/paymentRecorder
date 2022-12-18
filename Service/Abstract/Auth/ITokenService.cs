using System.IdentityModel.Tokens.Jwt;
using Common.Jwt;
using Domain;

namespace Service.Abstract.Auth;

public interface ITokenService
{
    Task<JwtSecurityToken> CreateAccessToken(ApplicationUser user, CancellationToken cancellationToken);

    Task<RefreshToken> CreateUniqueRefreshTokenAsync(CancellationToken cancellationToken);

    Task<(RefreshToken,AccessToken)> ExchangeRefreshTokenAndGetNewAccessToken(string oldRefreshToken, CancellationToken cancellationToken);

    Task<AccessToken> GetAccessToken(string refreshToken, CancellationToken cancellationToken);
}