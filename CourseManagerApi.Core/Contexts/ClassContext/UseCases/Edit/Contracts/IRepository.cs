using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.CourseContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.Edit.Contracts;

public interface IRepository
{
    Task<Class?> FindClassByIdAsync(string classId, CancellationToken cancellationToken);
    Task<Course?> FindCourseByIdAsync(string courseId, CancellationToken cancellationToken);
    Task<User?> FindUserByIdAsync(string ministerId, string tenantId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}