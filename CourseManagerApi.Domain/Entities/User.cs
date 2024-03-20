using CourseManagerApi.Domain.ValueObjects;
using CourseManagerApi.Shared.Entities;
using Flunt.Validations;

namespace CourseManagerApi.Domain.Entities;

public class User : Entity
{
    public User(Email email, Name name, Document document, PhoneNumber phoneNumber, Gender gender, Address address, DateTime birthDate, Occupation occupation, Tenant tenant, bool isSmoker, string observation)
    {
        Email = email;
        Name = name;
        Document = document;
        PhoneNumber = phoneNumber;
        Gender = gender;
        Address = address;
        BirthDate = birthDate;
        Occupation = occupation;
        Tenant = tenant;
        IsSmoker = isSmoker;
        Observation = observation;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Active = true;

        VerifyNotifications();
    }

    public Email Email { get; private set; }
    public Name Name { get; private set; }
    public Document Document { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Gender Gender { get; private set; }
    public Address Address { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Occupation Occupation { get; private set; }
    public Tenant Tenant { get; private set; }
    public bool IsSmoker { get; private set; }
    public string Observation { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool Active { get; private set; }

    protected override void VerifyNotifications()
    {
        AddNotifications(Email, Name, Document, PhoneNumber, Gender, Address, Occupation, Tenant);

        if (!String.IsNullOrEmpty(Observation))
            AddNotifications(
                new Contract<User>()
                    .Requires()
                    .IsLowerOrEqualsThan(Observation.Length, 256, "User.Observation", "Observação deve ser menor ou igual a 256 caracteres")
            );
    }
}