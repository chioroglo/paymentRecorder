using Microsoft.AspNetCore.Mvc;

namespace PaymentRecorder.Controllers.Abstract
{
    [Route("api/")]
    [ApiController]
    public class AppBaseController : ControllerBase
    {

        public AppBaseController()
        {
            
        }

        [HttpGet("test")]
        public IActionResult Index()
        {
            return Ok("controller test");
        }
    }
}
