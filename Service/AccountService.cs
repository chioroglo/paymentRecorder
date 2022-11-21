using System.Linq.Expressions;
using Common.Exceptions;
using Data;
using Data.Extensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Service.Abstract;
using Service.Abstract.Base;
using static Common.ExceptionMessages.ValidationExceptionMessages;

namespace Service;

public class AccountService : BaseEntityService<Account>, IAccountService
{
    private readonly EfDbContext _db;

    public AccountService(EfDbContext db)
    {
        _db = db;
    }

    public override async Task<Account> Add(Account entity, CancellationToken cancellationToken)
    {
        await _db.Accounts.AddAsync(entity, cancellationToken);

        await _db.SaveChangesAsync(true,cancellationToken);

        return entity;
    }

    public override async Task<IEnumerable<Account>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Accounts.AsNoTracking().ToListAsync(cancellationToken);
    }

    public override async Task<Account> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _db.Accounts.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
               ?? throw new EntityValidationException(EntityWasNotFoundBecause<Account>($"of ID:{id} does not exist"));
    }

    public override async Task<Account> GetByIdWithIncludeAsync(int id, CancellationToken cancellationToken, params Expression<Func<Account, object>>[] includeProperties)
    {
        return await _db.IncludeProperties(includeProperties).AsNoTracking().FirstOrDefaultAsync(e => e.Id == id,cancellationToken) ??
               throw new EntityValidationException(EntityWasNotFoundBecause<Account>($"of ID:{id} does not exist"));
    }

    public override async Task RemoveAsync(int id, int issuerId, CancellationToken cancellationToken)
    {
        var entity = await _db.Accounts.FirstOrDefaultAsync(e => e.Id == id,cancellationToken);

        if (entity == null)
        {
            throw new EntityValidationException(EntityWasNotFoundBecause<Account>($"of ID:{id} does not exist"));
        }

        _db.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public override async Task<Account> UpdateAsync(Account entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}