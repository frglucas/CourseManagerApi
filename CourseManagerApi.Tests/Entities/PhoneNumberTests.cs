using CourseManagerApi.Domain.Entities;

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

        Assert.IsFalse(phoneNumber.IsValid);
    }

    [TestMethod]
    [TestCategory("ValueObject - PhoneNumber")]
    [DataRow("87654321")]
    [DataRow("97654321")]
    public void ReturnErrorWhenNumberIsInvalid(string number)
    {
        var phoneNumber = new PhoneNumber("51", number);

        Assert.IsFalse(phoneNumber.IsValid);
    }

    [TestMethod]
    [TestCategory("ValueObject - PhoneNumber")]
    [DataRow("51", "987654321", "51", "997654321")]
    [DataRow("53", "987654321", "51", "987654321")]
    [DataRow("53", "997654321", "51", "987654321")]
    public void ReturnErrorWhenNumberNotEquals(string areaCodeOne, string numberOne, string areaCodeTwo, string numberTwo)
    {
        var phoneNumberOne = new PhoneNumber(areaCodeOne, numberOne);
        var phoneNumberTwo = new PhoneNumber(areaCodeTwo, numberTwo);

        Assert.IsTrue(phoneNumberOne.IsValid && phoneNumberTwo.IsValid);
        Assert.IsFalse(phoneNumberOne.Equals(phoneNumberTwo));
    }

    [TestMethod]
    [TestCategory("ValueObject - PhoneNumber")]
    [DataRow("51", "987654321", "51", "987654321")]
    [DataRow("52", "987654322", "52", "987654322")]
    [DataRow("53", "987654323", "53", "987654323")]
    public void ReturnSuccessWhenNumberEquals(string areaCodeOne, string numberOne, string areaCodeTwo, string numberTwo)
    {
        var phoneNumberOne = new PhoneNumber(areaCodeOne, numberOne);
        var phoneNumberTwo = new PhoneNumber(areaCodeTwo, numberTwo);

        Assert.IsTrue(phoneNumberOne.IsValid && phoneNumberTwo.IsValid);
        Assert.IsTrue(phoneNumberOne.Equals(phoneNumberTwo));
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

        Assert.IsTrue(phoneNumber.IsValid);
    }
}