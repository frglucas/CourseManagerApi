using System.Security.Cryptography;
using CourseManagerApi.Domain.Enums;
using CourseManagerApi.Domain.Utils;
using CourseManagerApi.Domain.ValueObjects;

namespace CourseManagerApi.Tests.ValueObjects;

[TestClass]
public class DocumentTests
{
    [TestMethod]
    [TestCategory("Value Objects - Document")]
    [DataRow("25888225041")]
    [DataRow("32604988094")]
    [DataRow("13806252022")]
    [DataRow("53637613021")]
    [DataRow("49297766038")]
    [DataRow("052.164.090-99")]
    [DataRow("892.463.050-87")]
    [DataRow("310.314.270-99")]
    [DataRow("600.395.566-83")]
    [DataRow("101.815.052-49")]
    [DataRow("965.987.198-00")]
    public void ShouldReturnErrorWhenInvalidCPF(string documentNumber)
    {
        var document = new Document(documentNumber, EDocumentType.CPF);

        Assert.IsFalse(document.Validate());
    }

    [TestMethod]
    [TestCategory("Value Objects - Document")]
    [DataRow("32606988094")]
    [DataRow("13808252022")]
    [DataRow("83312880092")]
    [DataRow("49291766038")]
    [DataRow("25887225041")]
    [DataRow("168.755.110-37")]
    [DataRow("572.284.780-10")]
    [DataRow("388.261.830-23")]
    [DataRow("965.927.198-00")]
    [DataRow("538.460.483-21")]
    [DataRow("966.199.317-31")]
    public void ShouldReturnSuccessWhenValidCPF(string documentNumber)
    {
        var document = new Document(documentNumber, EDocumentType.CPF);

        Assert.IsTrue(document.Validate());
    }

    [TestMethod]
    [TestCategory("Value Objects - Document")]
    [DataRow("63518459000120")]
    [DataRow("35121462000170")]
    [DataRow("78094178000107")]
    [DataRow("90242848000104")]
    [DataRow("51127283000192")]
    [DataRow("09621096000138")]
    [DataRow("86671037000138")]
    [DataRow("57.016.957/0001-75")]
    [DataRow("69.078.803/0001-72")]
    [DataRow("64.079.994/0001-59")]
    [DataRow("79.985.565/0001-89")]
    [DataRow("22.303.245/0001-07")]
    [DataRow("65.758.061/0001-04")]
    [DataRow("14.115.372/0001-07")]
    public void ShouldReturnErrorWhenInvalidCNPJ(string documentNumber)
    {
        var document = new Document(documentNumber, EDocumentType.CNPJ);

        Assert.IsFalse(document.Validate());
    }

    [TestMethod]
    [TestCategory("Value Objects - Document")]
    [DataRow("63518439000120")]
    [DataRow("35021462000170")]
    [DataRow("78094198000107")]
    [DataRow("90242834000104")]
    [DataRow("51128283000192")]
    [DataRow("09621056000138")]
    [DataRow("86671057000138")]
    [DataRow("57.012.957/0001-75")]
    [DataRow("69.073.803/0001-72")]
    [DataRow("64.073.994/0001-59")]
    [DataRow("79.988.565/0001-89")]
    [DataRow("22.307.245/0001-07")]
    [DataRow("65.755.061/0001-04")]
    [DataRow("14.117.372/0001-07")]
    public void ShouldReturnSuccessWhenValidCNPJ(string documentNumber)
    {
        var document = new Document(documentNumber, EDocumentType.CNPJ);

        Assert.IsTrue(document.Validate());
    }

    [TestMethod]
    [TestCategory("Value Objects - Document")]
    [DataRow("32606988094", EDocumentType.CPF)]
    public void ShouldReturnCorrectEncryptNumber(string documentNumber, EDocumentType documentType)
    {
        Aes aes = Aes.Create();
        aes.GenerateKey();
        aes.GenerateIV();

        var document = new Document(documentNumber, documentType);
        document.GenerateEncryptNumber(aes.Key, aes.IV);

        var decrypted = EncryptUtils.Decrypt(document.EncryptNumber, aes.Key, aes.IV);

        Assert.AreEqual(documentNumber, decrypted);
        Assert.AreNotEqual(documentNumber, document.EncryptNumber);
    }
}