using System.Net.Mime;
using Common.Dto.Auth;
using Common.Jwt;
using Common.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using ContentType = Azure.Core.ContentType;

namespace PaymentRecorder.Controllers.Auth;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;

    public AuthController(IAuthService authService, ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("register")]
    public async Task<ActionResult<AuthenticationModel>> RegisterAsync([FromBody] RegistrationDto dto,CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterAsync(dto,cancellationToken);

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationModel>> LoginAsync([FromBody] LoginDto dto,CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(dto,cancellationToken);

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("get-access-token")]
    public async Task<ActionResult<AccessToken>> GetAccessToken([FromQuery] string refreshToken,
        CancellationToken cancellationToken)
    {
        var accessToken = await _tokenService.GetAccessToken(refreshToken, cancellationToken);
        
        return Ok(accessToken);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("exchange-refresh-token")]
    public async Task<ActionResult<RefreshToken>> GetRefreshToken([FromQuery] string refreshToken,
        CancellationToken cancellationToken)
    {
        // retrieve refresh token from httponly cookie
        var newRefreshToken = await _tokenService.ExchangeRefreshToken(refreshToken, cancellationToken);

        return newRefreshToken;
    }

}