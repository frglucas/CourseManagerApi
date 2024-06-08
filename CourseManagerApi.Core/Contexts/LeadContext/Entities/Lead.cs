using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.LeadContext.ValueObjects;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Shared.Contexts.SharedContext.Entities;

namespace CourseManagerApi.Core.Contexts.LeadContext.Entities;

public class Lead : Entity
{
    protected Lead() {}

    public Lead(Name name, Email email, PhoneNumber phoneNumber, string observation)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Observation = observation;
    }

    public Name Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    public string Observation { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    public User Creator { get; private set; } = null!;
    public Guid CreatorId { get; private set; } = Guid.Empty;
    public Tenant Tenant { get; private set; } = null!;
    public bool IsAdhered { get; private set; } = false;

    public void SetName(Name name) => Name = name;
    public void SetEmail(Email email) => Email = email;
    public void SetPhoneNumber(PhoneNumber phoneNumber) => PhoneNumber = phoneNumber;
    public void SetObservation(string observation) => Observation = observation;
    public void SetIsAdhered(bool isAdhered) => IsAdhered = isAdhered;
    public void SetNewUpdateAt() => UpdatedAt = DateTime.UtcNow;
    public void SetCreator(User creator)
    {
        if (Creator == null)
        {
            Creator = creator;
            CreatorId = creator.Id;
        }
    }

    public void SetTenant(Tenant tenant)
    {
        if (Tenant == null)
            Tenant = tenant;
    }
}