using System.IdentityModel.Tokens.Jwt;
using Common.Dto.Auth;
using Common.Exceptions;
using Common.Extensions;
using Common.Jwt;
using Common.Models.Auth;
using Data;
using Domain;
using Domain.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Service.Abstract;
using Service.Utils;
using static Common.Exceptions.ExceptionMessages.IdentityExceptionMessages;


namespace Service;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JWTConfigurationFromAppsettingsJson _jwtConfiguration;
    private readonly EfDbContext _db;

    public AuthService(UserManager<ApplicationUser> userManager, IOptions<JWTConfigurationFromAppsettingsJson> jwt, EfDbContext db)
    {
        _userManager = userManager;
        _db = db;
        _jwtConfiguration = jwt.Value;
    }

    public async Task<AuthenticationModel> RegisterAsync(RegistrationDto dto, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByEmailAsync(dto.Email) != null)
        {
            throw new IdentityException(RegistrationFailedBecause($"email {dto.Email} is occupied"));
        }

        if (await _userManager.FindByNameAsync(dto.Username) != null)
        {
            throw new IdentityException(RegistrationFailedBecause($"username {dto.Username} is occupied"));
        }

        var user = new ApplicationUser()
        {
            UserName = dto.Username,
            Email = dto.Email,
            Firstname = dto.Firstname,
            Lastname = dto.Lastname
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

        var token = await JwtIssuer.CreateAccessTokenConformAppsettings(_userManager, user, _jwtConfiguration);


        return new AuthenticationModel
        {
            Username = user.UserName,
            Email = user.Email,
            RefreshTokenExpirationDate = token.ValidTo,
            Roles = new List<string> { UserRole.Accountant.GetEnumDescription() },
            RefreshToken = ""
        };
    }

    public async Task<AuthenticationModel> LoginAsync(LoginDto dto, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(dto.EmailOrUsername) ??
                   await _userManager.FindByEmailAsync(dto.EmailOrUsername);

        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
        {
            throw new IdentityException(AuthenticationFailedMessage());
        }
        
        var refreshToken = await JwtIssuer.CreateUniqueRefreshToken(_userManager, user,_jwtConfiguration, cancellationToken);
        user.RefreshToken = refreshToken.Token;
        user.RefreshTokenExpirationDate = refreshToken.ExpirationDate;

        await _userManager.UpdateAsync(user);
            

        var accessToken = await JwtIssuer.CreateAccessTokenConformAppsettings(_userManager, user, _jwtConfiguration);
        var roles = await _userManager.GetRolesAsync(user);

        return new AuthenticationModel
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