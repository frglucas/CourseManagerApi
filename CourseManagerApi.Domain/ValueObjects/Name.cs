using CourseManagerApi.Shared.ValueObjects;
using Flunt.Validations;

namespace CourseManagerApi.Domain.ValueObjects;

public class Name : ValueObject
{
    public Name(string firstName, string lastName, string badgeName)
    {
        FirstName = firstName;
        LastName = lastName;
        BadgeName = badgeName;

        VerifyNotifications();
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string BadgeName { get; private set; }

    public override bool Validate()
    {
        VerifyNotifications();
        return IsValid;
    }

    protected override bool VerifyNullValuesToNotifications()
    {
        AddNotifications(
            new Contract<Name>()
                .Requires()
                .IsNotNullOrEmpty(FirstName, "Name.FirstName", "Primeiro nome inválido")
                .IsNotNullOrEmpty(LastName, "Name.LastName", "Sobrenome inválido")
                .IsNotNullOrEmpty(BadgeName, "Name.BadgeName", "Nome do crachá inválido")
        );

        return IsValid;
    }

    protected override void VerifyNotifications()
    {
        if (!VerifyNullValuesToNotifications()) return;

        AddNotifications(
            new Contract<Name>()
                .Requires()
                .IsGreaterOrEqualsThan(FirstName.Length, 3, "Name.FirstName", "Primeiro nome deve conter 3 ou mais caracteres")
                .IsGreaterOrEqualsThan(LastName.Length, 3, "Name.LastName", "Sobrenome deve conter 3 ou mais caracteres")
                .IsGreaterOrEqualsThan(BadgeName.Length, 3, "Name.BadgeName", "Nome do crachá deve conter 3 ou mais caracteres")
                .IsLowerOrEqualsThan(FirstName.Length, 128, "Name.FirstName", "Primeiro nome deve conter 128 ou menos caracteres")
                .IsLowerOrEqualsThan(LastName.Length, 128, "Name.LastName", "Sobrenome deve conter 128 ou menos caracteres")
                .IsLowerOrEqualsThan(BadgeName.Length, 32, "Name.BadgeName", "Nome do crachá deve conter 32 ou menos caracteres")
        );
    }
}