using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.ValueObjects;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Shared.Contexts.SharedContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClientContext.Entities;

public class Client : Entity
{
    protected Client() { }
    public Client(Email email, Name name, Document document, Gender gender, DateTime birthDate, string observation, bool isSmoker, User creator, User captivator)
    {
        Email = email;
        Name = name;
        Document = document;
        Gender = gender;
        BirthDate = birthDate;
        Observation = observation;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsSmoker = isSmoker;
        IsActive = true;
        Creator = creator;
        Captivator = captivator;
    }

    public Email Email { get; private set; } = null!;
    public Name Name { get; private set; } = null!;
    public Document Document { get; private set; } = null!;
    public Gender Gender { get; private set; } = null!;
    public DateTime BirthDate { get; private set; } = DateTime.UtcNow;
    public Occupation? Occupation { get; private set; } = null!;
    public string Observation { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    public bool IsSmoker { get; private set; } = false;
    public bool IsActive { get; private set; } = true;
    public User Creator { get; private set; } = null!;
    public User Captivator { get; private set; } = null!;
    public Client? Indicator { get; private set; } = null!;
    public Tenant? Tenant { get; private set; } = null!;
    public List<Address> Addresses { get; } = new();
    public List<PhoneNumber> PhoneNumbers { get; } = new();

    public void AddAddress(Address address) => Addresses.Add(address);
    public void AddPhoneNumber(PhoneNumber phoneNumber) => PhoneNumbers.Add(phoneNumber);
    public void AddPhoneNumbers(List<PhoneNumber> phoneNumbers) => PhoneNumbers.AddRange(phoneNumbers);
    public void SetTenant(Tenant tenant)
    {
        if (Tenant == null)
            Tenant = tenant;
    }

    public void SetCaptivator(User captivator) => Captivator = captivator;
    public void SetIndicator(Client? indicator) => Indicator = indicator;

    public void SetEmail(Email email) => Email = email;
    public void SetName(Name name) => Name = name;
    public void SetDocument(Document document) => Document = document;
    public void SetBirthDate(DateTime birthDate) => BirthDate = birthDate;
    public void SetOccupation(Occupation? occupation) => Occupation = occupation;
    public void SetIsSmoker(bool isSmoker) => IsSmoker = isSmoker;
    public void SetGender(Gender gender) => Gender = gender;
    public void SetObservation(string observation) => Observation = observation;
    public void SetNewUpdateAt() => UpdatedAt = DateTime.UtcNow;

    public void Deactivate() => IsActive = false;
}