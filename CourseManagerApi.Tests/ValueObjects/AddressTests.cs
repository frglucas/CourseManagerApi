using CourseManagerApi.Domain.ValueObjects;

namespace CourseManagerApi.Tests.ValueObjects;

[TestClass]
public class AddressTests
{
    public static readonly string stringGreater128 = new string('a', 129);
    public static readonly string stringGreater32 = new string('a', 33);

    [TestMethod]
    [TestCategory("ValueObjects - Address")]
    [DataRow("",            "123456",   "Test Neighborhood",    "Test City",    "Test State",   "Test Country")]
    [DataRow("Test Street", "",         "Test Neighborhood",    "Test City",    "Test State",   "Test Country")]
    [DataRow("Test Street", "123456",   "",                     "Test City",    "Test State",   "Test Country")]
    [DataRow("Test Street", "123456",   "Test Neighborhood",    "",             "Test State",   "Test Country")]
    [DataRow("Test Street", "123456",   "Test Neighborhood",    "Test City",    "",             "Test Country")]
    [DataRow("Test Street", "123456",   "Test Neighborhood",    "Test City",    "Test State",   "")]
    public void ShouldReturnErrorWhenEmptyParams(string street, string addressNumber, string neighborhood, string city, string state, string country)
    {
        var address = new Address(street, addressNumber, neighborhood, city, state, country);

        Assert.IsFalse(address.Validate());
    }

    [TestMethod]
    [TestCategory("ValueObjects - Address")]
    [DataRow("st",          "123456",   "Test Neighborhood",    "Test City",    "Test State",   "Test Country")]
    [DataRow("Test Street", "",        "Test Neighborhood",    "Test City",    "Test State",   "Test Country")]
    [DataRow("Test Street", "123456",   "nh",                   "Test City",    "Test State",   "Test Country")]
    [DataRow("Test Street", "123456",   "Test Neighborhood",    "ct",           "Test State",   "Test Country")]
    [DataRow("Test Street", "123456",   "Test Neighborhood",    "Test City",    "st",           "Test Country")]
    [DataRow("Test Street", "123456",   "Test Neighborhood",    "Test City",    "Test State",   "cy")]
    public void ShouldReturnErrorWhenLowerLenghtParams(string street, string addressNumber, string neighborhood, string city, string state, string country)
    {
        var address = new Address(street, addressNumber, neighborhood, city, state, country);

        Assert.IsFalse(address.Validate());
    }

    [TestMethod]
    [TestCategory("ValueObjects - Address")]
    public void ShouldReturnErrorWhenGreaterLenghtStreet()
    {
        var address = new Address(stringGreater128, "123456", "Test Neighborhood", "Test City", "Test State", "Test Country");

        Assert.IsFalse(address.Validate());
    }

    [TestMethod]
    [TestCategory("ValueObjects - Address")]
    public void ShouldReturnErrorWhenGreaterLenghtAddressNumber()
    {
        var valueToTest = "123456A";
        var address = new Address("Test Street", valueToTest, "Test Neighborhood", "Test City", "Test State", "Test Country");

        Assert.IsFalse(address.Validate());
    }

    [TestMethod]
    [TestCategory("ValueObjects - Address")]
    public void ShouldReturnErrorWhenGreaterLenghtNeighborhood()
    {
        var address = new Address("Test Street", "123456", stringGreater32, "Test City", "Test State", "Test Country");

        Assert.IsFalse(address.Validate());
    }
    
    [TestMethod]
    [TestCategory("ValueObjects - Address")]
    public void ShouldReturnErrorWhenGreaterLenghtCity()
    {
        var address = new Address("Test Street", "123456", "Test Neighborhood", stringGreater32, "Test State", "Test Country");

        Assert.IsFalse(address.Validate());
    }
    
    [TestMethod]
    [TestCategory("ValueObjects - Address")]
    public void ShouldReturnErrorWhenGreaterLenghtState()
    {
        var address = new Address("Test Street", "123456", "Test Neighborhood", "Test City", stringGreater32, "Test Country");

        Assert.IsFalse(address.Validate());
    }
    
    [TestMethod]
    [TestCategory("ValueObjects - Address")]
    public void ShouldReturnErrorWhenGreaterLenghtCountry()
    {
        var address = new Address("Test Street", "123456", "Test Neighborhood", "Test City", "Test State", stringGreater32);

        Assert.IsFalse(address.Validate());
    }

    [TestMethod]
    [TestCategory("ValueObjects - Address")]
    [DataRow("98765")]
    [DataRow("98765A00")]
    public void ShouldReturnErrorWhenInvalidZipCode(string zipCode)
    {
        var address = new Address("Test Street", "123456", "Test Neighborhood", "Test City", "Test State", "Test Country", zipCode);

        Assert.IsFalse(address.Validate());
    }

    [TestMethod]
    [TestCategory("ValueObjects - Address")]
    [DataRow("ad")]
    [DataRow("", true)]
    public void ShouldReturnErrorWhenInvalidAddOnAddress(string addOnAddress, bool useStringGreater32 = false)
    {
        var valueToTest = useStringGreater32 ? stringGreater32: addOnAddress;

        var address = new Address("Test Street", "123456", "Test Neighborhood", "Test City", "Test State", "Test Country", "98765000", valueToTest);

        Assert.IsFalse(address.Validate());
    }

    [TestMethod]
    [TestCategory("ValueObjects - Address")]
    [DataRow("Test Street", "123456", "Test Neighborhood", "Test City", "Test State", "Test Country")]
    [DataRow("Street", "1234A", "Neighborhood", "City", "State", "Country")]
    public void ShouldReturnSuccessWhenValidRequiredParams(string street, string addressNumber, string neighborhood, string city, string state, string country)
    {
        var address = new Address(street, addressNumber, neighborhood, city, state, country);

        Assert.IsTrue(address.Validate());
    }

    [TestMethod]
    [TestCategory("ValueObjects - Address")]
    [DataRow("Test Street", "123456", "Test Neighborhood", "Test City", "Test State", "Test Country", "98765000", "Test AddOnAddress")]
    [DataRow("Street", "1234A", "Neighborhood", "City", "State", "Country", "97654010", "AddOnAddress")]
    public void ShouldReturnSuccessWhenValidParamsWithOptionals(string street, string addressNumber, string neighborhood, string city, string state, string country, string zipCode, string addOnAddress)
    {
        var address = new Address(street, addressNumber, neighborhood, city, state, country, zipCode, addOnAddress);

        Assert.IsTrue(address.Validate());
    }
}