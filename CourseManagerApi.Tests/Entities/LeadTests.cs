using CourseManagerApi.Domain.Entities;
using CourseManagerApi.Domain.ValueObjects;

namespace CourseManagerApi.Tests.Entities;

[TestClass]
public class LeadTests
{
    private readonly Email _email;
    private readonly Name _name;
    private readonly PhoneNumber _phoneNumber;

    public LeadTests()
    {
        _email = new Email("test@mail.com.br");
        _name = new Name("TestName", "TestLastName");
        _phoneNumber = new PhoneNumber("51", "987654321");
    }

    [TestMethod]
    [TestCategory("Entities - Lead")]
    public void ShouldReturnSuccess()
    {
        var lead = new Lead(_name, _email, _phoneNumber);

        Assert.IsTrue(lead.IsValid);
    }
}