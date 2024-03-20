using System.Text.RegularExpressions;
using CourseManagerApi.Shared.ValueObjects;
using Flunt.Validations;

namespace CourseManagerApi.Domain.ValueObjects;

public class Address : ValueObject
{
    public Address(string street, string addressNumber, string neighborhood, string city, string state, string country, string zipCode = "", string addOnAddress = "")
    {
        Street = street;
        AddressNumber = addressNumber;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
        AddOnAddress = addOnAddress;

        VerifyNotifications();
    }

    public string Street { get; private set; }
    public string AddressNumber { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }
    public string AddOnAddress { get; private set; }

    public override bool Validate()
    {
        VerifyNotifications();
        return IsValid;
    }

    protected override void VerifyNotifications()
    {
        if (VerifyNullValuesToNotifications())
            AddNotifications(
                new Contract<Address>()
                    .Requires()
                    .IsGreaterOrEqualsThan(Street.Length, 3, "Address.Street", "Rua deve conter 3 ou mais caracteres")
                    .IsGreaterOrEqualsThan(AddressNumber.Length, 1, "Address.AddressNumber", "Número do endereço deve conter 1 ou mais caracteres")
                    .IsGreaterOrEqualsThan(Neighborhood.Length, 3, "Address.Neighborhood", "Bairro deve conter 3 ou mais caracteres")
                    .IsGreaterOrEqualsThan(City.Length, 3, "Address.City", "Cidade deve conter 3 ou mais caracteres")
                    .IsGreaterOrEqualsThan(State.Length, 3, "Address.State", "Estado deve conter 3 ou mais caracteres")
                    .IsGreaterOrEqualsThan(Country.Length, 3, "Address.Country", "País deve conter 3 ou mais caracteres")
                    .IsLowerOrEqualsThan(Street.Length, 128, "Address.Street", "Rua deve conter 128 ou menos caracteres")
                    .IsLowerOrEqualsThan(AddressNumber.Length, 6, "Address.AddressNumber", "Número do endereço deve conter 6 ou menos caracteres")
                    .IsLowerOrEqualsThan(Neighborhood.Length, 32, "Address.Neighborhood", "Bairro deve conter 32 ou menos caracteres")
                    .IsLowerOrEqualsThan(City.Length, 32, "Address.City", "Cidade deve conter 32 ou menos caracteres")
                    .IsLowerOrEqualsThan(State.Length, 32, "Address.State", "Estado deve conter 32 ou menos caracteres")
                    .IsLowerOrEqualsThan(Country.Length, 32, "Address.Country", "País deve conter 32 ou menos caracteres")
            );

        if (!String.IsNullOrEmpty(ZipCode))
            AddNotifications(
                new Contract<Address>()
                    .Requires()
                    .AreEquals(ZipCode.Length, 8, "Address.ZipCode","CEP inválido")
                    .IsTrue(Regex.IsMatch(ZipCode, "^[0-9]{8}$"), "Address.ZipCode", "Formato do CEP inválido")
            );

        if (!String.IsNullOrEmpty(AddOnAddress))
            AddNotifications(
                new Contract<Address>()
                    .Requires()
                    .IsGreaterOrEqualsThan(AddOnAddress.Length, 3, "Address.AddOnAddress", "Complemento do endereço deve conter 3 ou mais caracteres")
                    .IsLowerOrEqualsThan(AddOnAddress.Length, 32, "Address.AddOnAddress", "Complemento do endereço deve conter 32 ou menos caracteres")
            );
    }

    protected override bool VerifyNullValuesToNotifications()
    {
        AddNotifications(
            new Contract<Address>()
                .Requires()
                .IsNotNullOrEmpty(Street, "Address.Street", "Rua inválido")
                .IsNotNullOrEmpty(AddressNumber, "Address.AddressNumber", "Número do endereço inválido")
                .IsNotNullOrEmpty(Neighborhood, "Address.Neighborhood", "Bairro inválido")
                .IsNotNullOrEmpty(City, "Address.City", "Cidade inválido")
                .IsNotNullOrEmpty(State, "Address.State", "Estado inválido")
                .IsNotNullOrEmpty(Country, "Address.Country", "País inválido")
        );

        return IsValid;
    }
}