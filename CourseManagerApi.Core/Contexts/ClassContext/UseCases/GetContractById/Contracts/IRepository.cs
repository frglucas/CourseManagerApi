using CourseManagerApi.Core.Contexts.ClassContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetContractById.Contracts;

public interface IRepository
{
    Task<Contract?> GetContractByIdAsync(string id, CancellationToken cancellationToken);
}