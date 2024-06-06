using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.LeadContext.Entities;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;

namespace CourseManagerApi.Core.Contexts.LeadContext.UseCases.Create.Contracts;

public interface IRepository
{
    Task<User?> FindCreatorByIdAsync(string id, CancellationToken cancellationToken);
    Task<Tenant?> FindTenantByIdAsync(string tenantId, CancellationToken cancellationToken);
    Task SaveAsync(Lead lead, CancellationToken cancellationToken);
}