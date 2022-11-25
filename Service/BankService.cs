using Common.Exceptions;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Service.Abstract;
using Service.Abstract.Base;
using static Common.Exceptions.ExceptionMessages.ValidationExceptionMessages;
using static Service.Extensions.EntityValidationExtensions;

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
        await ValidateForExistingBankCodeAsync(entity,cancellationToken);

        await _db.AddAsync(entity, cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public override async Task RemoveAsync(long id, Guid version, CancellationToken cancellationToken)
    {

        var entity = await _db.Banks.Include(e => e.Accounts).FirstOrDefaultAsync(e => e.Id == id, cancellationToken) ??
                     throw new EntityValidationException(EntityWasNotFoundBecause<Bank>($"of ID:{id} does not exist"));

        ValidateRowVersionEqualityThrowDbConcurrencyExceptionIfNot(entity.Version, version);


        if (entity.Accounts.Count > 0)
        {
            throw new EntityValidationException(EntityCannotBeDeletedBecause<Bank>($"of name {entity.Name} has associated bank accounts"));
        }

        _db.Remove(entity);

        await _db.SaveChangesAsync(cancellationToken);
    }

    public override async Task<Bank> UpdateAsync(Bank entity, CancellationToken cancellationToken)
    {
        var entityInDatabase = await _db.Banks.FirstOrDefaultAsync(e => entity.Id == e.Id, cancellationToken) ??
                     throw new EntityValidationException(EntityWasNotFoundBecause<Bank>($"of ID:{entity.Id} does not exist"));

        ValidateRowVersionEqualityThrowDbConcurrencyExceptionIfNot(entityInDatabase.Version,entity.Version);

        await ValidateForExistingBankCodeAsync(entity, cancellationToken);

        entityInDatabase.Name = entity.Name;
        entityInDatabase.Code = entity.Code;
        entityInDatabase.Version = Guid.NewGuid();

        _db.Entry(entityInDatabase).State = EntityState.Modified;

        await _db.SaveChangesAsync(cancellationToken);

        return entityInDatabase;
    }

    private async Task ValidateForExistingBankCodeAsync(Bank entity, CancellationToken cancellationToken)
    {
        if (await _db.Banks.FirstOrDefaultAsync(e => e.Code == entity.Code && e.Id != entity.Id, cancellationToken) != null)
        {
            throw new EntityValidationException(EntityCannotBeCreatedBecause<Bank>($"has code, that is already assigned to another {nameof(Bank)} in database"));
        }
    }
}