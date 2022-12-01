using System.Transactions;
using Common.Exceptions;
using Data;
using Microsoft.EntityFrameworkCore;
using Service.Abstract;
using Service.Abstract.Base;
using static Service.Utils.EntityValidationUtils;
using static Common.Exceptions.ExceptionMessages.ValidationExceptionMessages;
using Transaction = Domain.Transaction;

namespace Service;

public class TransactionService : BaseEntityService<Transaction>, ITransactionService
{
    public TransactionService(EfDbContext db) : base(db) { }

    public async Task<Transaction> AddAsync(Transaction dto, CancellationToken cancellationToken)
    {
        await _db.Transactions.AddAsync(dto, cancellationToken);

        await _db.SaveChangesAsync(true, cancellationToken);

        return dto;
    }

    public async Task<Transaction> EditAsync(Transaction dto, CancellationToken cancellationToken)
    {
        var entity =
            await _db.Transactions.FirstOrDefaultAsync(e => e.OrderId == dto.OrderId, cancellationToken)
                ?? throw new EntityValidationException(EntityCannotBeModifiedBecause<Transaction>($"with orderId: {dto.OrderId} of Id : {dto.Id} does not exist"));

        entity.TransactionState = dto.TransactionState;
        entity.TransactionType = dto.TransactionType;
        entity.OrderId = dto.OrderId;

        _db.Transactions.Entry(entity).State = EntityState.Modified;
        
        await _db.SaveChangesAsync(true, cancellationToken);

        return entity;
    }

    public async Task RemoveAsync(long orderId, Guid version, CancellationToken cancellationToken)
    {
        var entity = await _db.Transactions.FirstOrDefaultAsync(e => e.OrderId == orderId, cancellationToken)
            ?? throw new EntityValidationException(EntityCannotBeDeletedBecause<Transaction>($"on {orderId} does not exist"));

        ValidateRowVersionEqualityThrowDbConcurrencyExceptionIfNot(entity.Version,version);

        _db.Remove(entity);
    }
}