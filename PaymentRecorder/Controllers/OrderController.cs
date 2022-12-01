using AutoMapper;
using Common.Dto.Order;
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
        public async Task<ActionResult<OrderModel>> GetByNumberAsync(long orderNumber,
            CancellationToken cancellationToken)
        {
            var order = await _orderService.GetByNumber(orderNumber, cancellationToken);

            Response.Headers.ETag = order.Version.ToString();
            return Mapper.Map<OrderModel>(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderModel>> AddOrder([FromBody] OrderDto createOrderDto,
            CancellationToken cancellationToken)
        {
            var entity = await _orderService.Add(createOrderDto, cancellationToken);

            Response.Headers.ETag = entity.Version.ToString();
            return Mapper.Map<OrderModel>(entity);
        }

        [HttpPut("{orderNumber:long}")]
        public async Task<ActionResult<OrderModel>> EditOrderByNumberAsync([FromRoute] long orderNumber,
            [FromBody] OrderDto orderDto,
            CancellationToken cancellationToken)
        {
            orderDto.Version = Guid.Parse(Request.Headers.IfMatch);
            var entity = await _orderService.UpdateByNumberAsync(orderNumber, orderDto, cancellationToken);

            Response.Headers.ETag = entity.Version.ToString();
            return Mapper.Map<OrderModel>(entity);
        }
    }
}