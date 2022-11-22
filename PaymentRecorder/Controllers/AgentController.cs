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
        public async Task<AgentModel> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            var entity = await _agentService.GetByIdAsync(id,cancellationToken);

            return Mapper.Map<AgentModel>(entity);
        }
        
        [HttpPost]
        public async Task<AgentModel> AddNewAgent([FromBody] AgentDto agent, CancellationToken cancellationToken)
        {
            var dto = Mapper.Map<Agent>(agent);
            

            var createdEntity = await _agentService.Add(dto,cancellationToken);

            return Mapper.Map<AgentModel>(createdEntity);
        }
    }
}
