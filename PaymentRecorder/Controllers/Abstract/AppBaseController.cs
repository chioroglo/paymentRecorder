using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace PaymentRecorder.Controllers.Abstract
{
    [Route("api/")]
    [ApiController]
    public abstract class AppBaseController : ControllerBase
    {
        protected readonly IMapper Mapper;
        //protected readonly UriBuilder UriBuilder;

        protected AppBaseController(IMapper mapper)
        {
            Mapper = mapper;
            //UriBuilder = new UriBuilder(Request.Scheme,Request.Host.Host,Request.Host.Port ?? 80);
        }
    }
}
