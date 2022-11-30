using AutoMapper;
using Common.Models;
using Domain;
using Microsoft.AspNetCore.Mvc;
using PaymentRecorder.Controllers.Base;
using Service.Abstract;

namespace PaymentRecorder.Controllers
{
    [Route($"api/{nameof(Order)}")]
    public class OrderController : AppBaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IMapper mapper, IOrderService orderService) : base(mapper)
        {
            _orderService = orderService;
        }

        [HttpGet("{orderNumber:long}")]
        public async Task<ActionResult<OrderModel>> GetByNumberAsync(long orderNumber, CancellationToken cancellationToken)
        {
            var order =  await _orderService.GetByNumber(orderNumber, cancellationToken);

            Response.Headers.IfMatch = order.Version.ToString();
            return Mapper.Map<OrderModel>(order);
        }
    }
}
