using System.Security.Cryptography;

namespace CourseManagerApi.Domain.Utils;

public static class EncryptUtils
{
    public static string Encrypt(string simpleText, byte[] key, byte[] iv)
    {
        byte[] cipheredText;

        using (Aes aes = Aes.Create())
        {
            ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(simpleText);
                    }

                    cipheredText = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(cipheredText);
    }

    public static string Decrypt(string textEncrypted, byte[] key, byte[] iv)
    {
        string simpleText = String.Empty;
        byte[] cipheredText = Convert.FromBase64String(textEncrypted);

        using (Aes aes = Aes.Create())
        {
            ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);

            using (MemoryStream memoryStream = new MemoryStream(cipheredText))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        simpleText = streamReader.ReadToEnd();
                    }
                }
            }
        }

        return simpleText;
    }
}
