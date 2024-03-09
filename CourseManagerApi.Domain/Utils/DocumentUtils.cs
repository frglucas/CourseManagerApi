using System.Text.RegularExpressions;
using CourseManagerApi.Domain.Enums;

namespace CourseManagerApi.Domain.Utils;

public static class DocumentUtils
{
    public static bool Validate(this EDocumentType documentType, string document) 
    {
        switch (documentType)
        {
            case EDocumentType.CPF:
                return ValidateCPF(document);
            case EDocumentType.CNPJ:
                return true;
            default:
                return false;
        }
    }

    private static bool ValidateCPF(string document)
    {
        var regex = new Regex(@"[^\d]");
        var digitsOnly = regex.Replace(document, "");

        if (digitsOnly.Length != 11) return false;
        
        if (digitsOnly.Distinct().Count() == 1 || digitsOnly.Equals("12345678909")) return false;

        var sum = digitsOnly.Substring(0, 9).Select(x => x.ToString()).Select((value, index) => decimal.Parse(value) * (11 - (index + 1))).Sum();
        
        var remainder = (sum * 10) % 11;

        if ((remainder == 10) || (remainder == 11)) remainder = 0;

        if (remainder != decimal.Parse(digitsOnly.Substring(9, 1))) return false;

        sum = decimal.Zero;
        sum = digitsOnly.Substring(0, 10).Select(x => x.ToString()).Select((value, index) => decimal.Parse(value) * (12 - (index + 1))).Sum();

        remainder = (sum * 10) % 11;

        if ((remainder == 10) || (remainder == 11)) remainder = 0;

        if (remainder != decimal.Parse(digitsOnly.Substring(10, 1))) return false;

        return true;
    }
}