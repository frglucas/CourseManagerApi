using System.Text.RegularExpressions;
using CourseManagerApi.Shared.Contexts.SharedContext.ValueObjects;

namespace CourseManagerApi.Core.Contexts.ClientContext.ValueObjects;

public partial class Email : ValueObject
{
    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    protected Email() { }

    public Email(string address)
    {
        if (string.IsNullOrEmpty(address))
            throw new Exception("E-mail inválido");

        Address = address.Trim().ToLower();

        if (Address.Length < 5)
            throw new Exception("E-mail inválido");

        if (!EmailRegex().IsMatch(Address))
            throw new Exception("E-mail inválido");
    }

    public string Address { get; } = string.Empty;

    public static implicit operator string(Email email)
        => email.ToString();

    public static implicit operator Email(string address)
        => new(address);

    public override string ToString() => Address;

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();
}