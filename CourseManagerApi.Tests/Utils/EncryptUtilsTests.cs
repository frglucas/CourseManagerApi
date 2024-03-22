using System.Security.Cryptography;
using CourseManagerApi.Domain.Utils;

namespace CourseManagerApi.Tests.Utils;

[TestClass]
public class EncryptUtilsTests
{
    private readonly Aes _aes;

    public EncryptUtilsTests()
    {
        _aes = Aes.Create();
        _aes.GenerateIV();
        _aes.GenerateKey();
    }

    [TestMethod]
    [TestCategory("Utils - EncryptUtils")]
    [DataRow("TextToTest")]
    [DataRow("OtherTextToTest")]
    [DataRow("TextToTestPlus")]
    public void Should(string textToTest)
    {
        var encrypted = EncryptUtils.Encrypt(textToTest, _aes.Key, _aes.IV);
        var decryptor = EncryptUtils.Decrypt(encrypted, _aes.Key, _aes.IV);
        
        Assert.AreEqual(decryptor, textToTest);
    }
}