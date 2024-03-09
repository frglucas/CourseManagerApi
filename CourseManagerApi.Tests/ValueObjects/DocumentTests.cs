using CourseManagerApi.Domain.Enums;
using CourseManagerApi.Domain.ValueObjects;

namespace CourseManagerApi.Tests.ValueObjects;

[TestClass]
public class DocumentTests
{
    [TestMethod]
    [TestCategory("Value Objects - Name")]
    [DataRow("052.164.090-99")]
    [DataRow("32604988094")]
    [DataRow("892.463.050-87")]
    [DataRow("13806252022")]
    [DataRow("310.314.270-99")]
    [DataRow("53637613021")]
    [DataRow("600.395.566-83")]
    [DataRow("49297766038")]
    [DataRow("101.815.052-49")]
    [DataRow("25888225041")]
    [DataRow("965.987.198-00")]
    public void ShouldReturnErrorWhenInvalidCPF(string documentNumber)
    {
        var document = new Document(documentNumber, EDocumentType.CPF);

        Assert.IsFalse(document.Validate());
    }

    [TestMethod]
    [TestCategory("Value Objects - Name")]
    [DataRow("168.755.110-37")]
    [DataRow("32606988094")]
    [DataRow("572.284.780-10")]
    [DataRow("13808252022")]
    [DataRow("388.261.830-23")]
    [DataRow("83312880092")]
    [DataRow("965.927.198-00")]
    [DataRow("49291766038")]
    [DataRow("538.460.483-21")]
    [DataRow("25887225041")]
    [DataRow("966.199.317-31")]
    public void ShouldReturnSuccessWhenValidCPF(string documentNumber)
    {
        var document = new Document(documentNumber, EDocumentType.CPF);

        Assert.IsTrue(document.Validate());
    }
}