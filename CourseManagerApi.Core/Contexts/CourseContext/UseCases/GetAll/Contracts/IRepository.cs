using CourseManagerApi.Core.Contexts.CourseContext.Entities;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.GetAll.Contracts;

public interface IRepository
{
    Task<IEnumerable<Course>> GetAll(CancellationToken cancellationToken);
}