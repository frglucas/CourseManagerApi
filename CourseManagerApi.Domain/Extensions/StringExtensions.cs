using System.Text.RegularExpressions;

namespace CourseManagerApi.Domain.Extensions;

public static class StringExtensions
{
    public static bool IsEmail(this string str) => Regex.IsMatch(str, "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");

    public static bool IsAreaCode(this string str) => Regex.IsMatch(str, "^[1-9]{2}$");

    public static bool IsPhoneNumber(this string str) => Regex.IsMatch(str, "^(?:[2-8]|9[0-9])[0-9]{3}[0-9]{4}$");
}