using Common.Exceptions;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Service.Abstract;
using Service.Abstract.Base;
using static Common.Exceptions.ExceptionMessages.ValidationExceptionMessages;

namespace Service;

public class AgentService : BaseEntityService<Agent>, IAgentService
{

    public AgentService(EfDbContext db) : base(db)
    {

    }

    public override async Task<Agent> Add(Agent entity, CancellationToken cancellationToken)
    {
        await _db.AddAsync(entity,cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public override async Task RemoveAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _db.Agents.FirstOrDefaultAsync(e => e.Id == id,cancellationToken) ??
                     throw new EntityValidationException(EntityWasNotFoundBecause<Agent>($"of ID:{id} does not exist"));
    }

    public override async Task<Agent> UpdateAsync(Agent entity, CancellationToken cancellationToken)
    {
        var entityInDatabase = await _db.Agents.FirstOrDefaultAsync(e => e.Id == entity.Id, cancellationToken) ??
                               throw new EntityValidationException(EntityWasNotFoundBecause<Agent>($"of ID:{entity.Id} does not exist"));

        entityInDatabase.Name = entity.Name;
        entityInDatabase.FiscalCode = entity.FiscalCode;
        entityInDatabase.Type = entity.Type;

        _db.Entry(entityInDatabase).State = EntityState.Modified;

        await _db.SaveChangesAsync(cancellationToken);

        return entityInDatabase;
    }
}