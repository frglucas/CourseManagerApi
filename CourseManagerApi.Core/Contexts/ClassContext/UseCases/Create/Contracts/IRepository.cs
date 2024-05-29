using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.Create.Contracts;

public interface IRepository
{
    Task<Course?> FindCourseByIdAsync(string id, CancellationToken cancellationToken);
    Task<Tenant?> FindTenantByIdAsync(string id, CancellationToken cancellationToken);
    Task<User?> FindUserByIdAsync(string userId, CancellationToken cancellationToken);
    Task SaveAsync(Class entity, CancellationToken cancellationToken);
}