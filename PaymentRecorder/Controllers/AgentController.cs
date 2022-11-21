using Domain;
using Microsoft.AspNetCore.Mvc;
using PaymentRecorder.Controllers.Abstract;
using Service.Abstract;

namespace PaymentRecorder.Controllers
{
    [Microsoft.AspNetCore.Components.Route("/agents/")]
    public class AgentController : AppBaseController
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpGet("{id:long}")]
        public async Task<Agent> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _agentService.GetByIdAsync(id,cancellationToken);
        }
    }
}
