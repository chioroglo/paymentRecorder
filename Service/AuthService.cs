using System.IdentityModel.Tokens.Jwt;
using Common.Dto.Auth;
using Common.Exceptions;
using Common.Extensions;
using Domain;
using Domain.Enum;
using Microsoft.AspNetCore.Identity;
using Service.Abstract;
using static Common.Exceptions.ExceptionMessages.IdentityExceptionMessages;


namespace Service;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;

    public AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<AuthenticationServiceResponseDto> RegisterAsync(RegistrationDto dto, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByEmailAsync(dto.Email) != null)
        {
            throw new IdentityException(RegistrationFailedBecause($"email {dto.Email} is occupied"));
        }

        if (await _userManager.FindByNameAsync(dto.Username) != null)
        {
            throw new IdentityException(RegistrationFailedBecause($"username {dto.Username} is occupied"));
        }


        var refreshToken = await _tokenService.CreateUniqueRefreshTokenAsync(cancellationToken);

        var user = new ApplicationUser()
        {
            UserName = dto.Username,
            Email = dto.Email,
            Firstname = dto.Firstname,
            Lastname = dto.Lastname,
            RefreshToken = refreshToken.Token,
            RefreshTokenExpirationDate = refreshToken.ExpirationDate
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Empty;

            foreach (var error in result.Errors)
            {
                errors += $"{error.Description}, ";
            }

            throw new IdentityException(RegistrationFailedBecause(errors));
        }

        await _userManager.AddToRoleAsync(user, UserRole.Accountant.GetEnumDescription());
        


        // check if adds refresh token automatically
        // uncomment if not
        await _userManager.UpdateAsync(user);

        var accessToken = await _tokenService.CreateAccessToken(user,cancellationToken);
        
        return new AuthenticationServiceResponseDto
        {
            Username = user.UserName,
            Email = user.Email,
            Roles = new List<string> { UserRole.Accountant.GetEnumDescription() },
            AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
            AccessTokenExpirationDate = accessToken.ValidTo,
            RefreshToken = user.RefreshToken,
            RefreshTokenExpirationDate = refreshToken.ExpirationDate
        };
    }

    public async Task<AuthenticationServiceResponseDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(dto.EmailOrUsername) ??
                   await _userManager.FindByEmailAsync(dto.EmailOrUsername);

        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
        {
            throw new IdentityException(AuthenticationFailedMessage());
        }

        if (user.RefreshTokenExpirationDate < DateTime.UtcNow)
        {
            var refreshToken = await _tokenService.CreateUniqueRefreshTokenAsync(cancellationToken);
            user.RefreshToken = refreshToken.Token;
            user.RefreshTokenExpirationDate = refreshToken.ExpirationDate;
        }

        await _userManager.UpdateAsync(user);
            

        var accessToken = await _tokenService.CreateAccessToken(user,cancellationToken);
        var roles = await _userManager.GetRolesAsync(user);

        return new AuthenticationServiceResponseDto
        {
            Username = user.UserName,
            Email = user.Email,
            Roles = roles,
            AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
            AccessTokenExpirationDate = accessToken.ValidTo,
            RefreshToken = user.RefreshToken,
            RefreshTokenExpirationDate = user.RefreshTokenExpirationDate
        };
    }
}