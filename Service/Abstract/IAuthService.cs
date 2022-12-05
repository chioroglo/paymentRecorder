using Common.Dto.Auth;
using Common.Models.Auth;

namespace Service.Abstract;

public interface IAuthService
{
    Task<AuthenticationModel> RegisterAsync(RegistrationDto dto, CancellationToken cancellationToken);

    Task<AuthenticationModel> LoginAsync(LoginDto dto, CancellationToken cancellationToken);
}