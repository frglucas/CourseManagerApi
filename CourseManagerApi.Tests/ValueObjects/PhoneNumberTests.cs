using CourseManagerApi.Domain.ValueObjects;

namespace CourseManagerApi.Tests.ValueObjects;

[TestClass]
public class PhoneNumberTests
{
    [TestMethod]
    [TestCategory("ValueObject - PhoneNumber")]
    [DataRow("1")]
    [DataRow(" 1")]
    [DataRow("  ")]
    [DataRow("00")]
    public void ReturnErrorWhenAreaCodeIsInvalid(string areaCode)
    {
        var phoneNumber = new PhoneNumber(areaCode, "987654321");

        Assert.IsFalse(phoneNumber.Validate());
    }

    [TestMethod]
    [TestCategory("ValueObject - PhoneNumber")]
    [DataRow("87654321")]
    [DataRow("97654321")]
    public void ReturnErrorWhenNumberIsInvalid(string number)
    {
        var phoneNumber = new PhoneNumber("51", number);

        Assert.IsFalse(phoneNumber.Validate());
    }

    [TestMethod]
    [TestCategory("ValueObject - PhoneNumber")]
    [DataRow("11", "987654321")]
    [DataRow("21", "988654321")]
    [DataRow("31", "988754321")]
    [DataRow("41", "988784321")]
    [DataRow("53", "988884321")]
    public void ReturnSuccessWhenIsValid(string areaCode, string number)
    {
        var phoneNumber = new PhoneNumber(areaCode, number);

        Assert.IsTrue(phoneNumber.Validate());
    }
}