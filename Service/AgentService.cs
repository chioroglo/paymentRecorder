using Common.Exceptions;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Service.Abstract;
using Service.Abstract.Base;
using Service.Extensions;
using static Common.Exceptions.ExceptionMessages.ValidationExceptionMessages;
using static Service.Extensions.EntityValidationExtensions;

namespace Service;

public class AgentService : BaseEntityService<Agent>, IAgentService
{
    private readonly IIncludableQueryable<Agent, ICollection<Account>> _agentsWithAccounts;

    public AgentService(EfDbContext db) : base(db)
    {
        _agentsWithAccounts = _db.Agents.Include(e => e.Accounts);
    }

    public async Task<Agent> GetByFiscalCodeWithAccountsAsync(long fiscalCode, CancellationToken cancellationToken)
    {
        var entityInDatabase = await _agentsWithAccounts.Include(e => e.Accounts).FirstOrDefaultAsync(e => e.FiscalCode == fiscalCode, cancellationToken) ??
                               throw new EntityValidationException(EntityWasNotFoundBecause<Agent>($"with FiscalCode: {fiscalCode} does not exist"));

        return entityInDatabase;
    }

    public override async Task<Agent> Add(Agent entity, CancellationToken cancellationToken)
    {
        if (await (_db.Agents.FirstOrDefaultAsync(e => e.FiscalCode == entity.FiscalCode, cancellationToken)) != null)
        {
            throw new EntityValidationException(EntityCannotBeCreatedBecause<Agent>($"has fiscal code, that is already assigned to another {nameof(Agent)}"));
        }

        await _db.AddAsync(entity,cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);

        return entity;
    }


    public override async Task RemoveAsync(long id, Guid version, CancellationToken cancellationToken)
    {
        var entity = await _db.Agents.Include(e => e.Accounts).FirstOrDefaultAsync(e => e.Id == id, cancellationToken) ??
                     throw new EntityValidationException(EntityWasNotFoundBecause<Agent>($"of ID:{id} does not exist"));
        ValidateRowVersionEqualityThrowDbConcurrencyExceptionIfNot(entity.Version, version);

        if (entity.Accounts.Count > 0)
        {
            throw new EntityValidationException(EntityCannotBeDeletedBecause<Bank>($"of name {entity.Name} has associated bank accounts"));
        }

        
        _db.Remove(entity);

        await _db.SaveChangesAsync(cancellationToken);
    }


    public override async Task<Agent> UpdateAsync(Agent entity, CancellationToken cancellationToken)
    {
        var entityInDatabase = await _db.Agents.FirstOrDefaultAsync(e => e.Id == entity.Id, cancellationToken) ??
                               throw new EntityValidationException(EntityWasNotFoundBecause<Agent>($"of ID:{entity.Id} does not exist"));
        ValidateRowVersionEqualityThrowDbConcurrencyExceptionIfNot(entityInDatabase.Version,entity.Version);

        entityInDatabase.Name = entity.Name;
        entityInDatabase.FiscalCode = entity.FiscalCode;
        entityInDatabase.TypeId = entity.TypeId;
        entityInDatabase.Version = Guid.NewGuid();
        
        
        _db.Entry(entityInDatabase).State = EntityState.Modified;
        
        await _db.SaveChangesAsync(cancellationToken);


        return entityInDatabase;
    }
}