using System.Web.Http;
using AutoMapper;
using Common.Models.Error;
using Microsoft.AspNetCore.Mvc;

namespace PaymentRecorder.Controllers.Abstract
{
    [RoutePrefix("api/")]
    [ApiController]
    [ProducesErrorResponseType(typeof(ErrorDetails))]
    public abstract class AppBaseController : ControllerBase
    {
        protected readonly IMapper Mapper;
        
        protected AppBaseController(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
