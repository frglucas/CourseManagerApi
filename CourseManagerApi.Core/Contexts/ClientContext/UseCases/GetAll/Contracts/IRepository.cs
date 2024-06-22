using CourseManagerApi.Core.Contexts.ClientContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAll.Contracts;

public interface IRepository
{
    Task<IEnumerable<Client>> GetAllClientsAsync(CancellationToken cancellationToken);
}