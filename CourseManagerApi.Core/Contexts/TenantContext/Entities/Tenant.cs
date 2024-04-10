using CourseManagerApi.Shared.Contexts.SharedContext.Entities;

namespace CourseManagerApi.Core.Contexts.TenantContext.Entities;

public class Tenant : Entity
{
    protected Tenant() { }

    public Tenant(string name) => Name = name;

    public string Name { get; private set; } = string.Empty;
}