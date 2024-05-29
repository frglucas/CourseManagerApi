
using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Create.Contracts;

public interface IRepository
{
    Task<bool> AnyByDocumentAsync(string hash, CancellationToken cancellationToken);
    Task<bool> AnyByEmailAsync(string email, CancellationToken cancellationToken);
    Task<Occupation?> FindOccupationByIdAsync(string occupationId, CancellationToken cancellationToken);
    Task<Tenant?> FindTenantByIdAsync(string tenantId, CancellationToken cancellationToken);
    Task SaveAsync(Client client, CancellationToken cancellationToken);
}