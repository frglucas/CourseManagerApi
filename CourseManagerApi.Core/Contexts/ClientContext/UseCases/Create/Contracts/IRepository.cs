
using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.LeadContext.Entities;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Create.Contracts;

public interface IRepository
{
    Task<bool> AnyByDocumentAsync(string hash, CancellationToken cancellationToken);
    Task<bool> AnyByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> FindCaptivatorByIdAsync(string captivatorId, CancellationToken cancellationToken);
    Task<User?> FindCreatorByIdAsync(string creatorId, CancellationToken cancellationToken);
    Task<Client?> FindIndicatorByIdAsync(string indicatorId, CancellationToken cancellationToken);
    Task<Lead?> FindLeadByIdAsync(string leadId, CancellationToken cancellationToken);
    Task<Occupation?> FindOccupationByIdAsync(string occupationId, CancellationToken cancellationToken);
    Task<Tenant?> FindTenantByIdAsync(string tenantId, CancellationToken cancellationToken);
    Task SaveAsync(Client client, CancellationToken cancellationToken);
}