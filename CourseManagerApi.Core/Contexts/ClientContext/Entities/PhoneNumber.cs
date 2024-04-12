using System.Text.RegularExpressions;
using CourseManagerApi.Shared.Contexts.SharedContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClientContext.Entities;

public partial class PhoneNumber : Entity
{
    private const string Pattern = @"^[1-9]{2}(?:[2-8]|9[0-9])[0-9]{3}[0-9]{4}$";

    protected PhoneNumber() { }
    public PhoneNumber(string number, Client client)
    {
        if (!PhoneNumberRegex().IsMatch(number))
            throw new Exception("Número de celular inválido");

        Number = number;
        Client = client;
    }

    public string Number { get; private set; } = string.Empty;
    public Client Client { get; private set; } = null!;
    public Guid ClientId { get; private set; } = Guid.Empty;

    [GeneratedRegex(Pattern)]
    private static partial Regex PhoneNumberRegex();
}