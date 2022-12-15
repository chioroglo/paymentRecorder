using Common.Dto.Auth;
using Common.Models.Auth;

namespace Service.Abstract.Auth;

public interface IAuthService
{
    Task<AuthenticationServiceResponseDto> RegisterAsync(RegistrationDto dto, CancellationToken cancellationToken);

    Task<AuthenticationServiceResponseDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken);

    Task<ApplicationUserModel> GetByUserId(string userId, CancellationToken cancellationToken);

    Task Logout(string refreshToken, CancellationToken cancellationToken);
}