using CourseManagerApi.Core.Contexts.PaymentContext.Entities;

namespace CourseManagerApi.Core.Contexts.PaymentContext.UseCases.RemoveInstallment.Contracts;

public interface IRepository
{
    Task<Installment?> FindInstallmentByIdAsync(string installmentId, CancellationToken cancellationToken);
    Task SaveChangesAsync(Installment installment, CancellationToken cancellationToken);
}