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

        [HttpGet("{id:long}")]
        public async Task<ActionResult<AgentModel>> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            var entity = await _agentService.GetByIdAsync(id,cancellationToken);

            return Ok(Mapper.Map<AgentModel>(entity));
        }
        
        [HttpPost]
        public async Task<ActionResult<AgentModel>> AddNewAgent([FromBody] AgentDto agent, CancellationToken cancellationToken)
        {
            var dto = Mapper.Map<Agent>(agent);
                

            var createdEntity = await _agentService.Add(dto,cancellationToken);

            UriBuilder.Path = $"{createdEntity.Id}";
            return CreatedAtAction("",Mapper.Map<AgentModel>(createdEntity));
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<AgentModel>> EditExistingAgentById([FromBody] AgentDto agent,long id, CancellationToken cancellationToken)
        {
            var dto = Mapper.Map<Agent>(agent);

            dto.Id = id;
            

            var updatedEntity = await _agentService.UpdateAsync(dto,cancellationToken);

            return StatusCode((int)HttpStatusCode.OK, Mapper.Map<AgentModel>(updatedEntity));
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult> DeleteAgentById(long id, CancellationToken cancellationToken)
        {
            await _agentService.RemoveAsync(id, cancellationToken);

            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}
