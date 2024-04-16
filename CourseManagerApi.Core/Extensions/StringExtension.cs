using System.Security.Cryptography;
using System.Text;

namespace CourseManagerApi.Core.Extensions;

public static class StringExtensions
{
    public static string ToHashedString(this string str) => Hashing(str);

    private static string Hashing(string str)
    {
        MD5 md5Hash = MD5.Create();

        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(str));

        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            stringBuilder.Append(data[i].ToString("x2"));
        }

        return stringBuilder.ToString();
    }
}