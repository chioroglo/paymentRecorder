using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace PaymentRecorder.Controllers.Abstract
{
    [Route("api/")]
    [ApiController]
    public abstract class AppBaseController : ControllerBase
    {
        protected readonly IMapper Mapper;

        protected AppBaseController(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
