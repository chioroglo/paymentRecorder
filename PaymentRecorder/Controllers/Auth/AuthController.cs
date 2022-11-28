using Common.Dto.Auth;
using Common.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace PaymentRecorder.Controllers.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationModel>> RegisterAsync(RegistrationDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
