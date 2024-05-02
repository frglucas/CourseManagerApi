using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Shared.Contexts.SharedContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClassContext.Entities;

public class Class : Entity
{
    protected Class() { }
    public Class(Course course, User minister, DateTime scheduledDate, bool isOnline)
    {
        Course = course;
        Minister = minister;
        ScheduledDate = scheduledDate;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsOnline = isOnline;
    }

    public Course Course { get; private set; } = null!;
    public User Minister { get; private set; } = null!;
    public DateTime ScheduledDate { get; private set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    public bool IsOnline { get; private set; } = false;
    public Tenant Tenant { get; private set; } = null!;

    public void SetTenant(Tenant tenant) => Tenant = tenant;
}