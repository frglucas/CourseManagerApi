using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.Create.Contracts;

public interface IRepository
{
    Task<bool> AnyByNameAsync(string name, CancellationToken cancellationToken);
    Task<Tenant?> FindTenantById(string tenantId, CancellationToken cancellationToken);
    Task SaveAsync(Course course, CancellationToken cancellationToken);
}