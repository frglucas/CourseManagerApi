using CourseManagerApi.Domain.ValueObjects;
using CourseManagerApi.Shared.Entities;
using Flunt.Validations;

namespace CourseManagerApi.Domain.Entities;

public class Client : Entity
{
    private IList<PhoneNumber> _phoneNumbers;

    public Client(Email email, Name name, Document document, Gender gender, Address address, DateTime birthDate, Occupation occupation, Tenant tenant, bool isSmoker, string observation)
    {
        _phoneNumbers = new List<PhoneNumber>();
        Email = email;
        Name = name;
        Document = document;
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
    public IReadOnlyCollection<PhoneNumber> PhoneNumbers { get { return _phoneNumbers.ToArray(); } }
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

    public void AddPhoneNumber(PhoneNumber phoneNumber)
    {
        if (_phoneNumbers.Where(x => x.Equals(phoneNumber)).Count() > 0)
            AddNotification("Client.PhoneNumbers", "Número de celular já cadastrado");

        if (IsValid)
            _phoneNumbers.Add(phoneNumber);
    }

    protected override void VerifyNotifications()
    {
        AddNotifications(Email, Name, Document, Gender, Address, Occupation, Tenant,
            new Contract<Client>()
                .Requires()
                .IsNotNull(BirthDate, "Client.BirthDate", "Data de nascimento deve ser informado")
        );

        if (!String.IsNullOrEmpty(Observation))
            AddNotifications(
                new Contract<Client>()
                    .Requires()
                    .IsLowerOrEqualsThan(Observation.Length, 256, "Client.Observation", "Observação deve ser menor ou igual a 256 caracteres")
            );
    }
}