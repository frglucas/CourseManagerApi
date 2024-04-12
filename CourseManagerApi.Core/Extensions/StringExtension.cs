using System.Security.Cryptography;

namespace CourseManagerApi.Core.Extensions;

public static class StringExtensions
{
    public static string ToHashedString(this string str) => Hashing(str);

    public static bool IsMatch(this string hash, string str) => Verify(hash, str);

    private static string Hashing(string str, short saltSize = 16, short keySize = 32, int iterations = 10000, char splitChar = '.')
    {
        if (string.IsNullOrEmpty(str))
            throw new Exception("Value should not be null or empty");

        str += Configuration.Secrets.StringSaltKey;

        using var algorithm = new Rfc2898DeriveBytes(str, saltSize, iterations, HashAlgorithmName.SHA256);
        var key = Convert.ToBase64String(algorithm.GetBytes(keySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return $"{iterations}{splitChar}{salt}{splitChar}{key}";
    }

    private static bool Verify(string hash, string str, short keySize = 32, int iterations = 10000, char splitChar = '.')
    {
        str += Configuration.Secrets.StringSaltKey;

        var parts = hash.Split(splitChar, 3);
        if (parts.Length != 3)
            return false;

        var hashIterations = Convert.ToInt32(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);
        var key = Convert.FromBase64String(parts[2]);

        if (hashIterations != iterations)
            return false;

        using var algorithm = new Rfc2898DeriveBytes(str, salt, iterations, HashAlgorithmName.SHA256);
        var keyToCheck = algorithm.GetBytes(keySize);

        return keyToCheck.SequenceEqual(key);
    }
}