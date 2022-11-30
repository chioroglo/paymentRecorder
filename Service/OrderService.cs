using Common.Dto;
using Common.Exceptions;
using Data;
using Domain;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Service.Abstract;
using Service.Abstract.Base;
using static Common.Exceptions.ExceptionMessages.ValidationExceptionMessages;

namespace Service;

public class OrderService : BaseEntityService<Order>, IOrderService
{
    public OrderService(EfDbContext db) : base(db)
    {
    }

    public async Task<Order> Add(OrderDto dto, CancellationToken cancellationToken)
    {
        var queryWithAgents = _db.Accounts
            .Include(e => e.Agent);

        var issuerAccount = await queryWithAgents.FirstOrDefaultAsync(e => e.AccountCode == dto.IssuerAccountCode && e.Agent.FiscalCode == dto.IssuerFiscalCode,cancellationToken)
            ?? throw new EntityValidationException(EntityCannotBeCreatedBecause<Account>($"with code : {dto.IssuerAccountCode} and fiscal code {dto.IssuerFiscalCode} was not found"));

        var beneficiaryAccount = await queryWithAgents.FirstOrDefaultAsync(e => e.AccountCode == dto.BeneficiaryAccountCode && e.Agent.FiscalCode == dto.IssuerFiscalCode,cancellationToken)
                                 ?? throw new EntityValidationException(EntityCannotBeCreatedBecause<Account>($"with code : {dto.BeneficiaryAccountCode} and fiscal code {dto.BeneficiaryFiscalCode} was not found"));


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

        await _db.SaveChangesAsync(true,cancellationToken);

        var transaction = new Transaction
        {
            Version = Guid.NewGuid(),
            OrderId = newOrder.Id,
            TransactionType = dto.TransactionType,
            TransactionState = dto.TransactionState
        };

        await _db.Transactions.AddAsync(transaction, cancellationToken);

        await _db.SaveChangesAsync(true, cancellationToken);

        var ordersWithAccountsAgentsBanksTransactions = _db.Orders
            .Include(e => e.BeneficiaryAccount)
            .ThenInclude(e => e.Agent)
            .Include(e => e.BeneficiaryAccount)
            .ThenInclude(e => e.Bank)
            .Include(e => e.IssuerAccount)
            .ThenInclude(e => e.Agent)
            .Include(e => e.IssuerAccount)
            .ThenInclude(e => e.Bank)
            .Include(e => e.Transaction);

        return await ordersWithAccountsAgentsBanksTransactions.FirstOrDefaultAsync(e => e.Id == newOrder.Id, cancellationToken) ?? newOrder;
    }

    public Task RemoveAsync(long id, Guid version, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Order> UpdateAsync(OrderDto entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    

    public async Task<Order> GetByNumber(long orderNumber, CancellationToken cancellationToken)
    {
        var result = await GetWhereWithIncludeAsync(e => e.Number == orderNumber, cancellationToken,e => e.Transaction,e => e.BeneficiaryAccount,e => e.IssuerAccount);

        return result.FirstOrDefault() ?? throw new EntityValidationException(EntityWasNotFoundBecause<Order>($"of number {orderNumber} does not exist"));
    }
}