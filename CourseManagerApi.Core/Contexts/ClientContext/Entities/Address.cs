using System.Text.RegularExpressions;
using CourseManagerApi.Core.Utils;
using CourseManagerApi.Shared.Contexts.SharedContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClientContext.Entities;

public partial class Address : Entity
{
    private const string Pattern = @"^[0-9]{8}$";

    protected Address() { }

    public Address(string street, string number, string neighborhood, string city, string state, string country, string zipCode, string addOnAddress, Client client)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        ZipCode = StringUtils.FilterOnlyNumbers(zipCode);
        AddOnAddress = addOnAddress;
        Client = client;

        if (!string.IsNullOrEmpty(zipCode) && !ZipCodeRegex().IsMatch(ZipCode))
            throw new Exception("CEP inválido");
    }

    public string Street { get; private set; } = string.Empty;
    public string Number { get; private set; } = string.Empty;
    public string Neighborhood { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    public string ZipCode { get; private set; } = string.Empty;
    public string AddOnAddress { get; private set; } = string.Empty;
    public Client Client { get; private set; } = null!;
    public Guid ClientId { get; private set; } = Guid.Empty;

    [GeneratedRegex(Pattern)]
    private static partial Regex ZipCodeRegex();
}