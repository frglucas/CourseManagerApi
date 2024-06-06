using System.Text.RegularExpressions;
using CourseManagerApi.Shared.Contexts.SharedContext.ValueObjects;

namespace CourseManagerApi.Core.Contexts.LeadContext.ValueObjects;

public partial class PhoneNumber : ValueObject
{
    private const string AreaCodePattern = @"^[1-9]{2}$";
    private const string NumberPattern = @"^(?:[2-8]|9[0-9])[0-9]{3}[0-9]{4}$";

    protected PhoneNumber() {}

    public PhoneNumber(string areaCode, string number)
    {
        if (string.IsNullOrEmpty(areaCode))
            throw new Exception("Código de área inválido");

        if (string.IsNullOrEmpty(number))
            throw new Exception("Número inválido");

        AreaCode = areaCode.Trim();
        Number = number.Trim();

        if (areaCode.Length < 2)
            throw new Exception("Código de área inválido");

        if (number.Length < 8)
            throw new Exception("Número inválido");

        if (!AreaCodeRegex().IsMatch(AreaCode))
            throw new Exception("Código de área inválido");

        if (!NumberRegex().IsMatch(Number))
            throw new Exception("Número inválido");
    }
    
    public string AreaCode { get; private set; } = string.Empty;
    public string Number { get; private set; } = string.Empty;

    public override string ToString() => $"{AreaCode} {Number}";

    [GeneratedRegex(AreaCodePattern)]
    private static partial Regex AreaCodeRegex();

    [GeneratedRegex(NumberPattern)]
    private static partial Regex NumberRegex();
}