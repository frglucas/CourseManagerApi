using CourseManagerApi.Core.Contexts.ClassContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.RemoveClient.Contracts;

public interface IRepository
{
    Task DeleteAsync(Contract contract, CancellationToken cancellationToken);
    Task<Contract?> FindContractByIdAsync(string contractId, CancellationToken cancellationToken);
}