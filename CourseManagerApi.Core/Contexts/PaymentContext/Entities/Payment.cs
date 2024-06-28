using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.PaymentContext.Enums;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Shared.Contexts.SharedContext.Entities;

namespace CourseManagerApi.Core.Contexts.PaymentContext.Entities;

public class Payment : Entity
{
    protected Payment() { }
    public Payment(decimal totalPrice, int numberOfInstallments, EPaymentStatus paymentStatus)
    {
        TotalPrice = totalPrice;
        NumberOfInstallments = numberOfInstallments;
        PaymentStatus = paymentStatus;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public decimal TotalPrice { get; private set; } = Decimal.Zero;
    public int NumberOfInstallments { get; private set; } = 0;
    public EPaymentStatus PaymentStatus { get; private set; } = EPaymentStatus.NotPaid;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    public Contract Contract { get; private set; } = null!;
    public Guid ContractId { get; private set; } = Guid.Empty;
    public Tenant? Tenant { get; private set; } = null!;
    public List<Installment> Installments { get; private set; } = new();

    public void SetContract(Contract contract)
    {
        Contract = contract;
        ContractId = contract.Id;
    }

    public void SetTenant(Tenant tenant) => Tenant = tenant;

    public void SumTotalPrince(decimal Money) => TotalPrice += Money;
}