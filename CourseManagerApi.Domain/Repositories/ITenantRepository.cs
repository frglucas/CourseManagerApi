using CourseManagerApi.Domain.Entities;

namespace CourseManagerApi.Domain.Repositories;

public interface ITenantRepository
{
    Task<Tenant> FindByIdAsync(int tenantId);
}