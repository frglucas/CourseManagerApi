using CourseManagerApi.Core.Contexts.CourseContext.Entities;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.Delete.Contracts;

public interface IRepository
{
    Task<Course?> FindCourseByIdAsync(string id, CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
}