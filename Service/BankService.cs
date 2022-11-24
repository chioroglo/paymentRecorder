using Common.Exceptions;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Service.Abstract;
using Service.Abstract.Base;

using static Common.Exceptions.ExceptionMessages.ValidationExceptionMessages;

namespace Service;

public class BankService : BaseEntityService<Bank>, IBankService
{
    public BankService(EfDbContext db) : base(db)
    {

    }


    public async Task<Bank> GetByCodeAsync(string bankCode, CancellationToken cancellationToken)
    {
        var entity = await _db.Banks.FirstOrDefaultAsync(e => e.Code == bankCode, cancellationToken) ??
                     throw new EntityValidationException(EntityWasNotFoundBecause<Bank>($"with code:{bankCode} does not exist"));

        return entity;
    }

    public override async Task<Bank> Add(Bank entity, CancellationToken cancellationToken)
    {
        await _db.AddAsync(entity, cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public override async Task RemoveAsync(long id, Guid version, CancellationToken cancellationToken)
    {
        var entity = await _db.Banks.FirstOrDefaultAsync(e => e.Id == id, cancellationToken) ??
                     throw new EntityValidationException(EntityWasNotFoundBecause<Bank>($"of ID:{id} does not exist"));


        _db.Remove(entity);

        await _db.SaveChangesAsync(cancellationToken);
    }

    public override async Task<Bank> UpdateAsync(Bank entity, CancellationToken cancellationToken)
    {
        var entityInDatabase = await _db.Banks.FirstOrDefaultAsync(e => entity.Id == e.Id, cancellationToken) ??
                     throw new EntityValidationException(EntityWasNotFoundBecause<Bank>($"of ID:{entity.Id} does not exist"));


        if (entityInDatabase.Version != entity.Version)
        {
            throw new DbUpdateConcurrencyException("Concurrency conflict, please update your entity!");
        }

        entityInDatabase.Name = entity.Name;
        entityInDatabase.Code = entity.Code;
        entityInDatabase.Version = Guid.NewGuid();

        _db.Entry(entityInDatabase).State = EntityState.Modified;

        await _db.SaveChangesAsync(cancellationToken);

        return entityInDatabase;
    }
    
}