using CourseManagerApi.Core.Contexts.CourseContext.Entities;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.Edit.Contracts;

public interface IRepository
{
    Task<Course?> FindCourseByIdAsync(string id, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}