using Common.Dto.Auth;

namespace Service.Abstract;

public interface IAuthService
{
    Task<AuthenticationServiceResponseDto> RegisterAsync(RegistrationDto dto, CancellationToken cancellationToken);

    Task<AuthenticationServiceResponseDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken);
}