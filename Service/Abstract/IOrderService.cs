using Common.Dto;
using Domain;
using Service.Abstract.Base;

namespace Service.Abstract;

public interface IOrderService : IBaseEntityService<Order>
{
    Task<Order> Add(OrderDto dto, CancellationToken cancellationToken);

    Task RemoveAsync(long id, Guid version, CancellationToken cancellationToken);

    Task<Order> UpdateAsync(OrderDto entity, CancellationToken cancellationToken);

    Task<Order> GetByNumber(long orderNumber, CancellationToken cancellationToken);
}