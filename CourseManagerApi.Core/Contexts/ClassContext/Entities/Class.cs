using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.ValueObjects;
using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Shared.Contexts.SharedContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClassContext.Entities;

public class Class : Entity
{
    protected Class() { }
    public Class(Course course, User minister, Name name, string addressOrLink, DateTime scheduledDate, bool isOnline)
    {
        Course = course;
        Minister = minister;
        Name = name;
        AddressOrLink = addressOrLink;
        ScheduledDate = scheduledDate;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsOnline = isOnline;
    }

    public Course Course { get; private set; } = null!;
    public User Minister { get; private set; } = null!;
    public Name Name { get; private set; } = null!;
    public string AddressOrLink { get; private set; } = string.Empty;
    public DateTime ScheduledDate { get; private set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    public bool IsOnline { get; private set; } = false;
    public Tenant Tenant { get; private set; } = null!;

    public void SetCourse(Course course) => Course = course;
    public void SetMinister(User minister) => Minister = minister;
    public void SetName(Name name) => Name = name;
    public void SetAddressOrLink(string addressOrLink) => AddressOrLink = addressOrLink;
    public void SetScheduledDate(DateTime scheduledDate) => ScheduledDate = scheduledDate;
    public void SetIsOnline(bool isOnline) => IsOnline = isOnline;
    public void SetTenant(Tenant tenant) => Tenant = tenant;
    public void SetNewUpdateAt() => UpdatedAt = DateTime.UtcNow;
}