using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.PaymentContext.Entities;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;

namespace CourseManagerApi.Core.Contexts.PaymentContext.UseCases.AddInstallment.Contracts;

public interface IRepository
{
    Task<Contract?> FindContractByIdAsync(string contractId, CancellationToken cancellationToken);
    Task<Tenant?> FindTenantByIdAsync(string tenantId, CancellationToken cancellationToken);
    Task SaveAsync(Payment payment, CancellationToken cancellationToken);
    Task SaveAsync(Installment installment, CancellationToken cancellationToken);
}