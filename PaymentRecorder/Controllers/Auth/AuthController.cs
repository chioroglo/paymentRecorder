using System.IdentityModel.Tokens.Jwt;
using System.Net;
using AutoMapper;
using Common.Dto.Auth;
using Common.Exceptions;
using Common.Exceptions.ExceptionMessages;
using Common.Jwt;
using Common.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaymentRecorder.Factories;
using Service.Abstract.Auth;

namespace PaymentRecorder.Controllers.Auth;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;

    public AuthController(IAuthService authService, ITokenService tokenService, IMapper mapper)
    {
        _authService = authService;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    [Authorize(Roles = "System Administrator,Manager")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("register")]
    public async Task<ActionResult<AuthenticationResponseModel>> AddNewUserAsync([FromBody] RegistrationDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterAsync(dto, cancellationToken);

        HttpContext.Response.Cookies.Append(
            JwtCookieClaims.RefreshToken,
            result.RefreshToken,
            CookieOptionsFactory.CreateOptionsForTokenCookie(result.RefreshTokenExpirationDate));

        return Ok(_mapper.Map<AuthenticationResponseModel>(result));
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponseModel>> LoginAsync([FromBody] LoginDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(dto, cancellationToken);

        HttpContext.Response.Cookies.Append(
            JwtCookieClaims.RefreshToken,
            result.RefreshToken,
            CookieOptionsFactory.CreateOptionsForTokenCookie(result.RefreshTokenExpirationDate));

        return Ok(_mapper.Map<AuthenticationResponseModel>(result));
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("get-access-token")]
    public async Task<ActionResult<AuthenticationResponseModel>> GetUserAndAccessTokenByRefreshToken(CancellationToken cancellationToken)
        {
        var refreshToken = HttpContext.Request.Cookies[JwtCookieClaims.RefreshToken] ??
                           throw new AuthenticationException(TokenExceptionMessages.InvalidTokenMessage());

        var userWithAccessToken = await _authService.GetUserAndAccessTokenByRefreshToken(refreshToken, cancellationToken);

        return Ok(_mapper.Map<AuthenticationResponseModel>(userWithAccessToken));
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("exchange-refresh-token")]
    public async Task<ActionResult<AccessToken>> ExchangeRefreshToken(CancellationToken cancellationToken)
    {
        var oldRefreshToken = HttpContext.Request.Cookies[JwtCookieClaims.RefreshToken] ??
                              throw new AuthenticationException(TokenExceptionMessages.InvalidTokenMessage());

        var (newRefreshToken,newAccessToken) = await _tokenService.ExchangeRefreshTokenAndGetNewAccessToken(oldRefreshToken, cancellationToken);

        HttpContext.Response.Cookies.Append(
            JwtCookieClaims.RefreshToken,
            newRefreshToken.Token
            , CookieOptionsFactory.CreateOptionsForTokenCookie(newRefreshToken.ExpirationDate));

        return Ok(newAccessToken);
    }
    

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPost("logout")]
    public async Task<ActionResult> LogoutToken(CancellationToken cancellationToken)
    {
        var refreshToken = HttpContext.Request.Cookies[JwtCookieClaims.RefreshToken] ??
                           throw new AuthenticationException(TokenExceptionMessages.InvalidTokenMessage());

        await _authService.Logout(refreshToken, cancellationToken);

        HttpContext.Response.Cookies.Append(JwtCookieClaims.RefreshToken, "",
            CookieOptionsFactory.CreateOptionsForTokenCookie(DateTime.UtcNow.AddDays(-1)));

        return NoContent();
    }
}