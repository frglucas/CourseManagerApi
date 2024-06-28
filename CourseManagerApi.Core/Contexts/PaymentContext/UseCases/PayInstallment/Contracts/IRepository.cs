using CourseManagerApi.Core.Contexts.PaymentContext.Entities;

namespace CourseManagerApi.Core.Contexts.PaymentContext.UseCases.PayInstallment.Contracts;

public interface IRepository
{
    Task<Installment?> FindInstallmentByIdAsync(string installmentId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}