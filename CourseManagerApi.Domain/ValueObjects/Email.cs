using System.Text.RegularExpressions;
using CourseManagerApi.Domain.Extensions;
using CourseManagerApi.Shared.ValueObjects;
using Flunt.Validations;

namespace CourseManagerApi.Domain.ValueObjects;

public class Email : ValueObject
{
    public Email(string value)
    {
        Value = value;

        VerifyNotifications();
    }

    public string Value { get; private set; }

    public override bool Validate()
    {
        VerifyNotifications();
        return IsValid;
    }

    protected override void VerifyNotifications()
    {
        if (VerifyNullValuesToNotifications())
            AddNotifications(
                new Contract<Email>()
                    .Requires()
                    .IsTrue(Value.IsEmail(), "Email.Value", "Email em formato inválido")
            );
    }

    protected override bool VerifyNullValuesToNotifications()
    {
        AddNotifications(
            new Contract<Email>()
                .Requires()
                .IsNotNullOrEmpty(Value, "Email.Value", "Email não pode ser nulo")
        );

        return IsValid;
    }
}