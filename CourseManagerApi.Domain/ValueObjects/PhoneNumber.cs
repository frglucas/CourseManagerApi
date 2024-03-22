using System.Text.RegularExpressions;
using CourseManagerApi.Shared.ValueObjects;
using Flunt.Validations;

namespace CourseManagerApi.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
    public PhoneNumber(string areaCode,string number)
    {
        AreaCode = areaCode.Trim();
        Number = number.Trim();

        VerifyNotifications();
    }

    public string AreaCode { get; private set; }
    public string Number { get; private set; }

    public override bool Validate()
    {
        VerifyNotifications();
        return IsValid;
    }

    protected override void VerifyNotifications()
    {
        if (VerifyNullValuesToNotifications())
            AddNotifications(
                new Contract<PhoneNumber>()
                    .Requires()
                    .AreEquals(AreaCode.Length, 2, "PhoneNumber.AreaCode", "Código de área deve possuir 2 digitos")
                    .IsTrue(Regex.IsMatch(AreaCode, "^[1-9]{2}$"), "PhoneNumber.AreaCode", "Formato do código de área inválido")
                    .AreEquals(Number.Length, 9, "PhoneNumber.Number", "Número de celular deve possuir 9 dígitos")
                    .IsTrue(Regex.IsMatch(Number, "^(?:[2-8]|9[0-9])[0-9]{3}[0-9]{4}$"), "PhoneNumber.Number", "Formato do número de celular inválido")
            );
    }

    protected override bool VerifyNullValuesToNotifications()
    {
        AddNotifications(
            new Contract<PhoneNumber>()
                .Requires()
                .IsNotNullOrEmpty(AreaCode, "PhoneNumber.AreaCode", "Código de área inválido")
                .IsNotNullOrEmpty(Number, "PhoneNumber.Number", "Número de celular inválido")
        );

        return IsValid;
    }

    public override bool Equals(object? obj)
    {
        return obj is PhoneNumber number &&
                    AreaCode == number.AreaCode &&
                    Number == number.Number;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(AreaCode, Number);
    }
}