using Common.Dto.Order;
using Domain;
using Service.Abstract.Base;

namespace Service.Abstract;

public interface IOrderService : IBaseEntityService<Order>
{
    Task<Order> Add(OrderDto dto, CancellationToken cancellationToken);

    Task RemoveByOrderNumberAsync(long orderNumber, Guid version, CancellationToken cancellationToken);

    Task<Order> UpdateByNumberAsync(long orderNumber,OrderDto entity, CancellationToken cancellationToken);

    Task<Order> GetByNumber(long orderNumber, CancellationToken cancellationToken);
}