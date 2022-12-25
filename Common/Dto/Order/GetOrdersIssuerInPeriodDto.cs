using Common.Validation.Constraints;
using FluentValidation;

namespace Common.Dto.Order;

public class GetOrdersIssuerInPeriodDto
{
    public DateTime PeriodStart { get; set; }

    public DateTime PeriodEnd { get; set; }

    public int Limit { get; set; }
}

public class GetOrdersIssuerInPeriodDtoValidator : AbstractValidator<GetOrdersIssuerInPeriodDto>
{
    public GetOrdersIssuerInPeriodDtoValidator()
    {
        RuleFor(e => e.PeriodStart).NotEmpty();
        RuleFor(e => e.PeriodEnd).NotEmpty();
        RuleFor(e => e.Limit).NotEmpty().LessThanOrEqualTo(OrderValidationConstraints.OrdersPerPageFetchingLimit);
    }
}