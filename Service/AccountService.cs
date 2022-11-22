using Common.Exceptions;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Service.Abstract;
using Service.Abstract.Base;
using static Common.Exceptions.ExceptionMessages.ValidationExceptionMessages;

namespace Service;

public class AccountService : BaseEntityService<Account>, IAccountService
{
    public AccountService(EfDbContext db) : base(db)
    {

    }

    public override async Task<Account> Add(Account entity, CancellationToken cancellationToken)
    {
        await _db.Accounts.AddAsync(entity, cancellationToken);

        await _db.SaveChangesAsync(true,cancellationToken);

        return entity;
    }


    public override async Task RemoveAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _db.Accounts.FirstOrDefaultAsync(e => e.Id == id,cancellationToken) ?? 
                     throw new EntityValidationException(EntityWasNotFoundBecause<Account>($"of ID:{id} does not exist"));

        _db.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public override async Task<Account> UpdateAsync(Account entity, CancellationToken cancellationToken)
    {
        //var entityInDatabase = await _db.Accounts.FirstOrDefaultAsync(e => e.Id == entity.Id, cancellationToken) ?? 
        //                       throw new EntityValidationException(EntityWasNotFoundBecause<Account>($"of ID:{entity.Id} does not exist"));
        

        throw new NotImplementedException();
    }
}