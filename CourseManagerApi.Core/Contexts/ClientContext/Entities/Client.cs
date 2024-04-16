using CourseManagerApi.Core.Contexts.ClientContext.ValueObjects;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Shared.Contexts.SharedContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClientContext.Entities;

public class Client : Entity
{
    protected Client() { }
    public Client(Email email, AccountContext.ValueObjects.Name name, Document document, Gender gender, DateTime birthDate, Occupation occupation, string observation, bool isSmoker)
    {
        Email = email;
        Name = name;
        Document = document;
        Gender = gender;
        BirthDate = birthDate;
        Occupation = occupation;
        Observation = observation;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsSmoker = isSmoker;
        IsActive = true;
    }

    public Email Email { get; private set; } = null!;
    public AccountContext.ValueObjects.Name Name { get; private set; } = null!;
    public Document Document { get; private set; } = null!;
    public Gender Gender { get; private set; } = null!;
    public DateTime BirthDate { get; private set; } = DateTime.UtcNow;
    public Occupation Occupation { get; private set; } = null!;
    public string Observation { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    public bool IsSmoker { get; private set; } = false;
    public bool IsActive { get; private set; } = true;
    public Tenant Tenant { get; private set; } = null!;
    public List<Address> Addresses { get; } = new();
    public List<PhoneNumber> PhoneNumbers { get; } = new();

    public void AddAddress(Address address) => Addresses.Add(address);
    public void AddPhoneNumber(PhoneNumber phoneNumber) => PhoneNumbers.Add(phoneNumber);
    public void SetTenant(Tenant tenant)
    {
        if (Tenant == null)
            Tenant = tenant;
    }
}