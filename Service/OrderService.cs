using Data;
using Domain;
using Service.Abstract;
using Service.Abstract.Base;

namespace Service;

public class OrderService : BaseEntityService<Order>, IOrderService
{
    public OrderService(EfDbContext db) : base(db)
    {
    }

    public override Task<Order> Add(Order entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override Task RemoveAsync(long id, Guid version, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override Task<Order> UpdateAsync(Order entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}