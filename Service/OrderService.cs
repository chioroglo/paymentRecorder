using Common.Dto.Order;
using Common.Exceptions;
using Data;
using Domain;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Service.Abstract;
using Service.Abstract.Base;
using static Common.Exceptions.ExceptionMessages.ValidationExceptionMessages;
using static Service.Utils.EntityValidationMethods;

namespace Service;

public class OrderService : BaseEntityService<Order>, IOrderService
{
    private readonly ITransactionService _transactionService;

    public OrderService(EfDbContext db, ITransactionService transactionService) : base(db)
    {
        _transactionService = transactionService;
    }

    private IIncludableQueryable<Order, Transaction> GetQueryWithAllInclusionsForOrderModel()
    {
        return _db.Orders
            .Include(e => e.BeneficiaryAccount)
            .ThenInclude(e => e.Agent)
            .Include(e => e.BeneficiaryAccount)
            .ThenInclude(e => e.Bank)
            .Include(e => e.IssuerAccount)
            .ThenInclude(e => e.Agent)
            .Include(e => e.IssuerAccount)
            .ThenInclude(e => e.Bank)
            .Include(e => e.Transaction);
    }

    public async Task<Order> Add(OrderDto dto, CancellationToken cancellationToken)
    {
        var queryWithAgents = _db.Accounts
            .Include(e => e.Agent);

        var issuerAccount = await queryWithAgents.FirstOrDefaultAsync(
                                e => e.AccountCode == dto.IssuerAccountCode &&
                                     e.Agent.FiscalCode == dto.IssuerFiscalCode, cancellationToken)
                            ?? throw new EntityValidationException(EntityCannotBeCreatedBecause<Account>(
                                $"with code : {dto.IssuerAccountCode} and fiscal code {dto.IssuerFiscalCode} was not found"));

        var beneficiaryAccount = await queryWithAgents.FirstOrDefaultAsync(
                                     e => e.AccountCode == dto.BeneficiaryAccountCode &&
                                          e.Agent.FiscalCode == dto.BeneficiaryFiscalCode, cancellationToken)
                                 ?? throw new EntityValidationException(
                                     EntityCannotBeCreatedBecause<Account>(
                                         $"with code : {dto.BeneficiaryAccountCode} and fiscal code {dto.BeneficiaryFiscalCode} was not found"));

        if (issuerAccount.AccountCode == beneficiaryAccount.AccountCode)
        {
            throw new EntityValidationException(
                EntityCannotBeCreatedBecause<Account>("of beneficiary and issuer are unique"));
        }

        if (_db.Orders.Count(e => e.Number == dto.Number) > 0)
        {
            throw new EntityValidationException(EntityCannotBeCreatedBecause<Order>("with this number is occupied"));
        }

        if (dto.TransactionState == TransactionState.Completed && dto.ExecutionDate == null)
        {
            throw new EntityValidationException(
                EntityCannotBeCreatedBecause<Order>("is executed and execution date is not provided"));
        }

        if (dto.IssueDate > dto.ExecutionDate)
        {
            throw new EntityValidationException(
                EntityCannotBeCreatedBecause<Order>("information has invalid dates of execution and/or issuing"));
        }


        var newOrder = new Order
        {
            Version = Guid.NewGuid(),
            Number = dto.Number,
            Amount = dto.Amount,
            CurrencyCode = dto.CurrencyCode,
            Destination = dto.Destination,
            IssuerAccount = issuerAccount,
            IssuerAccountId = issuerAccount.Id,
            BeneficiaryAccount = beneficiaryAccount,
            BeneficiaryAccountId = beneficiaryAccount.Id,
            EmissionDate = dto.EmissionDate,
            IssueDate = dto.IssueDate,
            ExecutionDate = dto.TransactionState == TransactionState.Completed ? dto.ExecutionDate : null,
            Timezone = dto.Timezone
        };

        await _db.Orders.AddAsync(newOrder, cancellationToken);
        await _db.SaveChangesAsync(true, cancellationToken);

        await _transactionService.AddAsync(
            new Transaction
            {
                OrderId = newOrder.Id,
                TransactionType = dto.TransactionType,
                TransactionState = dto.TransactionState
            }, cancellationToken);

        return await GetQueryWithAllInclusionsForOrderModel()
            .FirstOrDefaultAsync(e => e.Id == newOrder.Id, cancellationToken) ?? newOrder;
    }

    public async Task RemoveByOrderNumberAsync(long orderNumber, Guid version, CancellationToken cancellationToken)
    {
        var entity = await _db.Orders.FirstOrDefaultAsync(e => e.Number == orderNumber, cancellationToken)
                     ?? throw new EntityValidationException(
                         EntityCannotBeDeletedBecause<Transaction>($"of ID {orderNumber} does not exist"));

        ValidateRowVersionEqualityThrowDbConcurrencyExceptionIfNot(entity.Version, version);

        _db.Orders.Remove(entity);

        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<Order> UpdateByNumberAsync(long orderNumber, OrderDto dto, CancellationToken cancellationToken)
    {
        var entityFromDb = await _db.Orders.FirstOrDefaultAsync(e => e.Number == orderNumber, cancellationToken)
                           ?? throw new EntityValidationException(
                               EntityCannotBeModifiedBecause<Order>($"with number {orderNumber} does not exist"));


        ValidateRowVersionEqualityThrowDbConcurrencyExceptionIfNot(dto.Version, entityFromDb.Version);

        var queryWithAgents = _db.Accounts
            .Include(e => e.Agent);

        var issuerAccount = await queryWithAgents.FirstOrDefaultAsync(
                                e => e.AccountCode == dto.IssuerAccountCode &&
                                     e.Agent.FiscalCode == dto.IssuerFiscalCode, cancellationToken)
                            ?? throw new EntityValidationException(EntityCannotBeModifiedBecause<Account>(
                                $"with code : {dto.IssuerAccountCode} and fiscal code {dto.IssuerFiscalCode} was not found"));

        var beneficiaryAccount = await queryWithAgents.FirstOrDefaultAsync(
                                     e => e.AccountCode == dto.BeneficiaryAccountCode &&
                                          e.Agent.FiscalCode == dto.BeneficiaryFiscalCode, cancellationToken)
                                 ?? throw new EntityValidationException(
                                     EntityCannotBeModifiedBecause<Account>(
                                         $"with code : {dto.BeneficiaryAccountCode} and fiscal code {dto.BeneficiaryFiscalCode} was not found"));

        if (issuerAccount.AccountCode == beneficiaryAccount.AccountCode)
        {
            throw new EntityValidationException(
                EntityCannotBeCreatedBecause<Account>("of beneficiary and issuer are unique"));
        }

        // change to any
        if (await _db.Orders.CountAsync(e => e.Number == dto.Number && e.Id != entityFromDb.Id, cancellationToken) > 0)
        {
            throw new EntityValidationException(
                EntityCannotBeCreatedBecause<Order>("with specified number is occupied"));
        }

        if (dto.TransactionState == TransactionState.Completed && dto.ExecutionDate == null)
        {
            throw new EntityValidationException(
                EntityCannotBeCreatedBecause<Order>($"is executed and execution date is not provided"));
        }

        if (dto.IssueDate > dto.ExecutionDate)
        {
            throw new EntityValidationException(
                EntityCannotBeCreatedBecause<Order>("information has invalid dates of execution and issuing"));
        }


        entityFromDb.Version = Guid.NewGuid();
        entityFromDb.Number = orderNumber;
        entityFromDb.Amount = dto.Amount;
        entityFromDb.CurrencyCode = dto.CurrencyCode;
        entityFromDb.Destination = dto.Destination;
        entityFromDb.IssuerAccount = issuerAccount;
        entityFromDb.IssuerAccountId = issuerAccount.Id;
        entityFromDb.BeneficiaryAccount = beneficiaryAccount;
        entityFromDb.BeneficiaryAccountId = beneficiaryAccount.Id;
        entityFromDb.EmissionDate = dto.EmissionDate;
        entityFromDb.IssueDate = dto.IssueDate;
        entityFromDb.ExecutionDate = dto.TransactionState == TransactionState.Completed ? dto.ExecutionDate : null;
        entityFromDb.Timezone = dto.Timezone;


        await _db.SaveChangesAsync(cancellationToken);

        var transactionDto = new Transaction
        {
            Version = Guid.NewGuid(),
            OrderId = entityFromDb.Id,
            TransactionType = dto.TransactionType,
            TransactionState = dto.TransactionState
        };

        await _transactionService.EditAsync(transactionDto, cancellationToken);


        return await GetQueryWithAllInclusionsForOrderModel()
            .FirstOrDefaultAsync(e => e.Id == entityFromDb.Id, cancellationToken) ?? entityFromDb;
    }


    public async Task<Order> GetByNumber(long orderNumber, CancellationToken cancellationToken)
    {
        return await GetQueryWithAllInclusionsForOrderModel()
                   .FirstOrDefaultAsync(e => e.Number == orderNumber, cancellationToken)
               ?? throw new EntityValidationException(
                   EntityWasNotFoundBecause<Order>($"of number {orderNumber} does not exist"));
    }

    public async Task<IEnumerable<Order>> GetAllIssuedInPeriod(DateTime periodStart, DateTime periodEnd,int limit, CancellationToken cancellationToken)
    {
        if (limit <= 0)
        {
            throw new EntityValidationException("Negative page limit introduced");
        }

        if (periodStart >= periodEnd)
        {
            throw new EntityValidationException("Invalid dates provided. Starting period is later or the same as ending period"); 
        }

        return await GetQueryWithAllInclusionsForOrderModel()
            .Where(e => e.IssueDate >= periodStart && e.IssueDate <= periodEnd).OrderByDescending(e => e.IssueDate).Take(limit).ToListAsync(cancellationToken);
    }
}