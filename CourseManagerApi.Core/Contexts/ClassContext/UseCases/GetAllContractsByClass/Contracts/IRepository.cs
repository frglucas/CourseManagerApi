using CourseManagerApi.Core.Contexts.ClassContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetAllContractsByClass.Contracts;

public interface IRepository
{
    Task<List<Contract>> GetContractsByClassIdAsync(string id, CancellationToken cancellationToken);
}