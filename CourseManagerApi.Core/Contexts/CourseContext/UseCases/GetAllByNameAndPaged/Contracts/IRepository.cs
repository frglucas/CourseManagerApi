using CourseManagerApi.Core.Contexts.CourseContext.Entities;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.GetAllByNameAndPaged.Contracts;

public interface IRepository
{
    Task<IEnumerable<Course>> GetAllByNameAsync(string term, CancellationToken cancellationToken);
}