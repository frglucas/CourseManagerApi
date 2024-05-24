using CourseManagerApi.Core.Contexts.ClientContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllByNameOrEmailAndPaged.Contracts;

public interface IRepository
{
    Task<IEnumerable<Client>> GetAllByNameOrEmailAsync(string term, CancellationToken cancellationToken);
}