using AutoMapper;
using Common.Dto.Account;
using Common.Models;
using Domain;
using Microsoft.AspNetCore.Mvc;
using PaymentRecorder.Controllers.Base;
using Service.Abstract;


namespace PaymentRecorder.Controllers
{
    [Route($"api/{nameof(Account)}")]
    [ApiController]
    public class AccountController : AppBaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IMapper mapper, IAccountService accountService) : base(mapper)
        {
            _accountService = accountService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("get-by-agent-fiscal-code/{agentId:long}")]
        public async Task<ActionResult<IEnumerable<AccountModel>>> GetByAgentFiscalCode(long agentId,
            CancellationToken cancellationToken)
        {
            var accounts = await _accountService.GetByAgentFiscalCodeAsync(agentId, cancellationToken);

            return Ok(accounts.Select(e => Mapper.Map<AccountModel>(e)));
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountModel>>> GetAll(CancellationToken cancellationToken)
        {
            var accounts = await _accountService.GetAllWithIncludeAsNoTrackingAsync(cancellationToken, e => e.Bank,
                e => e.Agent, e => e.IncomingOrders, e => e.OutcomingOrders);

            return Ok(accounts.Select(e => Mapper.Map<AccountModel>(e)));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id:long}")]
        public async Task<ActionResult<AccountModel>> GetById([FromRoute] long id, CancellationToken cancellationToken)
        {
            var entity = await _accountService.GetByIdWithIncludeAsNoTrackingAsync(id, cancellationToken, e => e.Bank,
                e => e.Agent,
                e => e.IncomingOrders, e => e.OutcomingOrders);

            Response.Headers.ETag = entity.Version.ToString();
            return Mapper.Map<AccountModel>(entity);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<AccountModel>> AddNewAccount([FromBody] AccountDto dto,
            CancellationToken cancellationToken)
        {
            var request = Mapper.Map<Account>(dto);

            var newlyCreatedEntity = await _accountService.Add(request, cancellationToken);

            Response.Headers.ETag = newlyCreatedEntity.Version.ToString();
            return Mapper.Map<AccountModel>(newlyCreatedEntity);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:long}")]
        public async Task<ActionResult<AccountModel>> Put([FromRoute] long id, [FromBody] AccountDto dto,
            CancellationToken cancellationToken)
        {
            var request = Mapper.Map<Account>(dto);
            request.Version = Guid.Parse(HttpContext.Request.Headers.IfMatch);
            request.Id = id;

            var updatedEntity = await _accountService.UpdateAsync(request, cancellationToken);

            Response.Headers.ETag = updatedEntity.Version.ToString();
            return Mapper.Map<AccountModel>(updatedEntity);
        }


        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:long}")]
        public async Task<ActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
        {
            await _accountService.RemoveAsync(id, Guid.Parse(HttpContext.Request.Headers.IfMatch), cancellationToken);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}