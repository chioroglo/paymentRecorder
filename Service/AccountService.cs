using Common.Exceptions;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Service.Abstract;
using Service.Abstract.Base;
using static Common.Exceptions.ExceptionMessages.ValidationExceptionMessages;
using static Service.Utils.EntityValidationMethods;

namespace Service;

public class AccountService : BaseEntityService<Account>, IAccountService
{
    public AccountService(EfDbContext db) : base(db)
    {
    }

    public async Task<IEnumerable<Account>> GetByAgentFiscalCodeAsync(long agentFiscalCode,
        CancellationToken cancellationToken)
    {
        return await _db.Accounts.Include(e => e.Agent).Include(e => e.Bank)
            .Where(e => e.Agent.FiscalCode == agentFiscalCode).ToListAsync(cancellationToken);
    }

    public async Task<Account> Add(Account entity, CancellationToken cancellationToken)
    {
        _ = await _db.Banks.FirstOrDefaultAsync(e => e.Id == entity.BankId, cancellationToken) ??
            throw new EntityValidationException(
                EntityCannotBeCreatedBecause<Bank>($"of ID:{entity.BankId} does not exist"));

        _ = await _db.Agents.FirstOrDefaultAsync(e => e.Id == entity.AgentId, cancellationToken) ??
            throw new EntityValidationException(
                EntityCannotBeCreatedBecause<Agent>($"of ID:{entity.AgentId} does not exist"));

        await _db.Accounts.AddAsync(entity, cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);

        return entity;
    }


    public async Task RemoveAsync(long id, Guid version, CancellationToken cancellationToken)
    {
        var entity = await _db.Accounts.FirstOrDefaultAsync(e => e.Id == id, cancellationToken) ??
                     throw new EntityValidationException(
                         EntityWasNotFoundBecause<Account>($"of ID:{id} does not exist"));

        ValidateRowVersionEqualityThrowDbConcurrencyExceptionIfNot(entity.Version, version);

        _db.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<Account> UpdateAsync(Account entity, CancellationToken cancellationToken)
    {
        var entityInDatabase = await _db.Accounts.FirstOrDefaultAsync(e => e.Id == entity.Id, cancellationToken) ??
                               throw new EntityValidationException(
                                   EntityCannotBeModifiedBecause<Account>($"of ID:{entity.Id} does not exist"));

        ValidateRowVersionEqualityThrowDbConcurrencyExceptionIfNot(entityInDatabase.Version, entity.Version);

        _ = await _db.Banks.FirstOrDefaultAsync(e => e.Id == entity.BankId, cancellationToken) ??
            throw new EntityValidationException(
                EntityCannotBeModifiedBecause<Bank>($"of ID:{entity.BankId} does not exist"));

        _ = await _db.Agents.FirstOrDefaultAsync(e => e.Id == entity.AgentId, cancellationToken) ??
            throw new EntityValidationException(
                EntityCannotBeModifiedBecause<Agent>($"of ID:{entity.AgentId} does not exist"));

        entityInDatabase.AccountCode = entity.AccountCode;
        entityInDatabase.AgentId = entity.AgentId;
        entityInDatabase.BankId = entity.BankId;
        entityInDatabase.Version = Guid.NewGuid();

        _db.Entry(entityInDatabase).State = EntityState.Modified;

        await _db.SaveChangesAsync(cancellationToken);

        return entityInDatabase;
    }
}