using CourseManagerApi.Core.Contexts.PaymentContext.Enums;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Shared.Contexts.SharedContext.Entities;

namespace CourseManagerApi.Core.Contexts.PaymentContext.Entities;

public class Installment : Entity
{
    public Installment() { }
    public Installment(decimal money, DateTime dueDate, Payment payment)
    {
        Money = money;
        DueDate = dueDate;
        PaymentStatus = EPaymentStatus.NotPaid;
        Payment = payment;
    }

    public decimal Money { get; private set; } = Decimal.Zero;
    public DateTime DueDate { get; private set; } = DateTime.UtcNow;
    public EPaymentStatus PaymentStatus { get; private set; } = EPaymentStatus.NotPaid;
    public Payment Payment { get; private set; } = null!;
    public Tenant Tenant { get; private set; } = null!;
}