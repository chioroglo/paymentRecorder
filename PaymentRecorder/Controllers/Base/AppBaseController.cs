using AutoMapper;
using Common.Models.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PaymentRecorder.Controllers.Base;

[Authorize(Roles = "Accountant,System Administrator,Manager")]
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