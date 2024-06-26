using CourseManagerApi.Core.Contexts.PaymentContext.Enums;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Shared.Contexts.SharedContext.Entities;

namespace CourseManagerApi.Core.Contexts.PaymentContext.Entities;

public class Installment : Entity
{
    public Installment() { }
    public Installment(decimal money, DateTime dueDate, EPaymentMethod paymentMethod)
    {
        Money = money;
        DueDate = dueDate;
        PaymentMethod = paymentMethod;
        PaymentStatus = EPaymentStatus.NotPaid;
    }

    public decimal Money { get; private set; } = Decimal.Zero;
    public DateTime DueDate { get; private set; } = DateTime.UtcNow;
    public EPaymentStatus PaymentStatus { get; private set; } = EPaymentStatus.NotPaid;
    public EPaymentMethod PaymentMethod { get; private set; } = EPaymentMethod.Money;
    public Payment Payment { get; private set; } = null!;
    public Tenant? Tenant { get; private set; } = null!;

    public void SetPayment(Payment payment) => Payment = payment;

    public void SetTenant(Tenant tenant) => Tenant = tenant; 

    public void SetPaymentStatus(EPaymentStatus paymentStatus) => PaymentStatus = paymentStatus; 
}