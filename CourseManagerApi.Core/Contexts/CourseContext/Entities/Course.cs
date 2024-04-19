using CourseManagerApi.Core.Contexts.CourseContext.ValueObjects;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Shared.Contexts.SharedContext.Entities;

namespace CourseManagerApi.Core.Contexts.CourseContext.Entities;

public class Course : Entity
{
    protected Course() { }
    public Course(Name name, Description description)
    {
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public Name Name { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    public bool IsActive { get; private set; } = true;
    public Tenant Tenant { get; private set; } = null!;

    public void SetTenant(Tenant tenant)
    {
        if (Tenant == null)
            Tenant = tenant;
    }
}