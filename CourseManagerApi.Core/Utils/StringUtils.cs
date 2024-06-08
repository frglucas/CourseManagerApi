using System.Text.RegularExpressions;

namespace CourseManagerApi.Core.Utils;

public static class StringUtils
{
    public static string FilterOnlyNumbers(string numberDocument)
    {
        var regex = new Regex(@"[^\d]");
        var digitsOnly = regex.Replace(numberDocument.Trim(), "");
        return digitsOnly;
    }

    public static bool ValidateCPF(string document)
    {
        var digitsOnly = FilterOnlyNumbers(document);

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

    private static int CalcDigits(int[] weights, string digitsOnly)
    {
        int soma = digitsOnly.Take(weights.Length).Select((c, i) => (c - '0') * weights[i]).Sum();
        return soma % 11 < 2 ? 0 : 11 - soma % 11;
    }

    public static bool ValidateCNPJ(string document)
    {
        var digitsOnly = FilterOnlyNumbers(document);

        if (digitsOnly.Length != 14 || digitsOnly.Distinct().Count() == 1) return false;

        int[] weightOne = {5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};
        int[] weightTwo = {6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};

        return digitsOnly[12] - '0' == CalcDigits(weightOne, digitsOnly) && digitsOnly[13] - '0' == CalcDigits(weightTwo, digitsOnly);
    }
}