using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.AddClient.Contracts;

public interface IRepository
{
    Task<Client?> FindClientByIdAsync(string clientId, CancellationToken cancellationToken);
    Task<Class?> FindClassByIdAsync(string classId, CancellationToken cancellationToken);
    Task<Tenant?> FindTenantByIdAsync(string tenantId, CancellationToken cancellationToken);
    Task SaveAsync(Contract contract, CancellationToken cancellationToken);
    Task<bool> AnyContractBetweenClientAndClassAsync(Guid clientId, Guid classId, CancellationToken cancellationToken);
}