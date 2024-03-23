using CourseManagerApi.Domain.Entities;
using CourseManagerApi.Domain.ValueObjects;
using CourseManagerApi.Domain.Enums;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine;

namespace CourseManagerApi.Tests.Entities;

[TestClass]
public class ClientTests
{
    public static readonly string stringGreater256 = new string('a', 257);

    private readonly Email _email;
    private readonly Name _name;
    private readonly Document _document;
    private readonly PhoneNumber _phoneNumber;
    private readonly Gender _gender;
    private readonly Address _address;
    private readonly Occupation _occupation;
    private readonly Tenant _tenant;

    public ClientTests()
    {
        _email = new Email("test@mail.com.br");
        _name = new Name("TestName", "TestLastName", "TestBadgeName");
        _document = new Document("83312880092", EDocumentType.CPF);
        _phoneNumber = new PhoneNumber("51", "987654321");
        _gender = new Gender(EGenderType.Uninformed);
        _address = new Address("Street Test", "12345A", "Neighborhood Test", "City Test", "State Test", "Country Test");
        _occupation = new Occupation(1, "Developer");
        _tenant = new Tenant("Test Company");
    }

    [TestMethod]
    [TestCategory("Entities - Client")]
    [DataRow("Simple observation Plus", true)]
    public void ShouldReturnErrorWhenInvalidParams(string observation, bool useStringGreater256 = false)
    {
        var value = useStringGreater256 ? stringGreater256 : observation;

        var client = new Client(_email, _name, _document, _gender, _address, DateTime.Now, _occupation, _tenant, false, value);

        Assert.IsFalse(client.IsValid);
    }

    [TestMethod]
    [TestCategory("Entities - Client")]
    public void ShouldReturnSuccessWhenValidParams()
    {
        var client = new Client(_email, _name, _document, _gender, _address, DateTime.Now, _occupation, _tenant, false, "Test");

        Assert.IsTrue(client.IsValid);
    }
}