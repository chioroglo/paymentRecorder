using Common.Dto.Auth;
using Common.Models.Auth;
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationModel>> RegisterAsync([FromBody] RegistrationDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            return Ok(result);
        }
    }
}
