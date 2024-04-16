using CourseManagerApi.Core.Contexts.ClientContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Delete.Contracts;

public interface IRepository
{
    Task<Client?> FindClientById(string id, CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
}