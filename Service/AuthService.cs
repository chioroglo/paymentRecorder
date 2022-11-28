using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using Azure.Identity;
using Common.Dto.Auth;
using Common.Exceptions;
using Common.Exceptions.ExceptionMessages;
using Common.Jwt;
using Common.Models.Auth;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using Service.Abstract;
using Service.Extensions;
using static Common.Exceptions.ExceptionMessages.IdentityExceptionMessages;


namespace Service;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JWTConfigurationFromAppsettingsJson _jwt;

    public AuthService(UserManager<ApplicationUser> userManager, IOptions<JWTConfigurationFromAppsettingsJson> jwt)
    {
        _userManager = userManager;
        _jwt = jwt.Value;
    }

    public async Task<AuthenticationModel> RegisterAsync(RegistrationDto dto,CancellationToken cancellationToken)
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
                errors += $" {error.Description},";
            }

            throw new IdentityException(RegistrationFailedBecause(errors));
        }

        await _userManager.AddToRoleAsync(user, "User");

        var token = await JwtUtils.CreateJwtTokenConformAppsettings(_userManager,user,_jwt);

        return new AuthenticationModel
        {
            Email = user.Email,
            ExpiresOn = token.ValidTo,
            Roles = new List<string>() { "User" },
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        };
    }
}