using CourseManagerApi.Domain.ValueObjects;

namespace CourseManagerApi.Tests.ValueObjects;

[TestClass]
public class NameTests
{ 
    public static readonly string stringGreater128 = new string('a', 129);

    [TestMethod]
    [TestCategory("ValueObject - Name")]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("Ab")]
    [DataRow("128LenghtTest", true)]
    public void ShouldReturnErrorWhenFirstNameValueIsNullOrEmpty(string firstName, bool useStringGreater128 = false)
    {
        var value = useStringGreater128 ? stringGreater128 : firstName;
        
        var name = new Name(value, "TestLastName", "TestName");
        
        Assert.IsFalse(name.IsValid);
        Assert.AreEqual(name.Notifications.Count, 1);
    }

    [TestMethod]
    [TestCategory("ValueObject - Name")]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("Ab")]
    [DataRow("128LenghtTest", true)]
    public void ShouldReturnErrorWhenLastNameValueIsNullOrEmpty(string lastName, bool useStringGreater128 = false)
    {
        var value = useStringGreater128 ? stringGreater128 : lastName;
        
        var name = new Name(value, "TestLastName", "TestName");
        
        Assert.IsFalse(name.IsValid);
        Assert.AreEqual(name.Notifications.Count, 1);
    }

    [TestMethod]
    [TestCategory("ValueObject - Name")]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("Ab")]
    [DataRow("128LenghtTest", true)]
    public void ShouldReturnErrorWhenBadgeNameValueIsNullOrEmpty(string badgeName, bool useStringGreater128 = false)
    {
        var value = useStringGreater128 ? stringGreater128 : badgeName;
        
        var name = new Name(value, "TestLastName", "TestName");
        
        Assert.IsFalse(name.IsValid);
        Assert.AreEqual(name.Notifications.Count, 1);
    }

    [TestMethod]
    [TestCategory("ValueObject - Name")]
    public void ShouldReturnSuccess()
    {
        var name = new Name("TestFirstName", "TestLastName", "TestName");
        
        Assert.IsTrue(name.IsValid);
        Assert.AreEqual(name.Notifications.Count, 0);
    }
}