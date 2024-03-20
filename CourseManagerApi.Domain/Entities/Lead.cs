using CourseManagerApi.Domain.ValueObjects;
using CourseManagerApi.Shared.Entities;

namespace CourseManagerApi.Domain.Entities;

public class Lead : Entity
{
    public Lead(Name name, Email email, PhoneNumber phoneNumber)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;

        VerifyNotifications();
    }

    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }

    protected override void VerifyNotifications()
    {
        AddNotifications(Name, Email, PhoneNumber);
    }
}