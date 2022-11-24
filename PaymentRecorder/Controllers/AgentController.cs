using System.Net;
using AutoMapper;
using Common.Dto;
using Common.Models;
using Domain;
using Microsoft.AspNetCore.Mvc;
using PaymentRecorder.Controllers.Abstract;
using Service.Abstract;

namespace PaymentRecorder.Controllers
{
    
    [Route("/agents/")]
    public class AgentController : AppBaseController
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService,IMapper mapper) : base(mapper)
        {
            _agentService = agentService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id:long}")]
        public async Task<ActionResult<AgentModel>> GetByIdIncludeAccountsNumberAsync(long id, CancellationToken cancellationToken)
        {
            var entity = await _agentService.GetByIdWithIncludeAsync(id,cancellationToken,e => e.Accounts);


            Response.Headers.ETag = entity.Version.ToString();
            return Ok(Mapper.Map<AgentModel>(entity));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("get-by-fiscal-code/{fiscalCode:long}")]
        public async Task<ActionResult<AgentModel>> GetByFiscalCodeAsync(long fiscalCode, CancellationToken cancellationToken)
        {
            var entity = await _agentService.GetByFiscalCodeWithAccountsAsync(fiscalCode,cancellationToken);

            Response.Headers.ETag = entity.Version.ToString();

            return Ok(Mapper.Map<AgentModel>(entity));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<AgentModel>> AddNewAgent([FromBody] AgentDto agent, CancellationToken cancellationToken)
        {
            var dto = Mapper.Map<Agent>(agent);

            var createdEntity = await _agentService.Add(dto,cancellationToken);

            Response.Headers.ETag = createdEntity.Version.ToString();
            return Ok(Mapper.Map<AgentModel>(createdEntity));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:long}")]
        public async Task<ActionResult<AgentModel>> EditExistingAgentById([FromBody] AgentDto agent, [FromRoute] long id, CancellationToken cancellationToken)
        {
            var dto = Mapper.Map<Agent>(agent);
            dto.Version = Guid.Parse(HttpContext.Request.Headers.IfMatch);
            dto.Id = id;
            

            var updatedEntity = await _agentService.UpdateAsync(dto,cancellationToken);


            Response.Headers.ETag = updatedEntity.Version.ToString();
            return StatusCode((int)HttpStatusCode.OK, Mapper.Map<AgentModel>(updatedEntity));
        }

        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id:long}")]
        public async Task<ActionResult> DeleteAgentById([FromRoute]long id, CancellationToken cancellationToken)
        {
            await _agentService.RemoveAsync(id, Guid.Parse(HttpContext.Request.Headers.IfMatch), cancellationToken);

            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}
