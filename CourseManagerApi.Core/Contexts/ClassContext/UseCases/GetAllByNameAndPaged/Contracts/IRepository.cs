using CourseManagerApi.Core.Contexts.ClassContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetAllByNameAndPaged.Contracts;

public interface IRepository
{
    Task<IEnumerable<Class>> GetAllByNameAsync(string term, CancellationToken cancellationToken);
}