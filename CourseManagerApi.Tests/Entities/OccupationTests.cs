using System.Numerics;
using CourseManagerApi.Domain.Entities;

namespace CourseManagerApi.Tests.Entities;

[TestClass]
public class OccupationTests
{
    [TestMethod]
    [TestCategory("Entities - Occupation")]
    [DataRow(-1, "Test")]
    [DataRow(1, "ds")]
    [DataRow(1, "A very long description to be longer than 32 characters")]
    public void ShouldReturnErrorWhenInvalidParams(int code, string description)
    {
        var occupation = new Occupation(code, description);

        Assert.IsFalse(occupation.IsValid);
    }
    
    [TestMethod]
    [TestCategory("Entities - Occupation")]
    [DataRow(0, "Developer")]
    [DataRow(1, ".Net Developer")]
    [DataRow(1000, "Doctor")]
    public void ShouldReturnSuccessWhenValidParams(int code, string description)
    {
        var occupation = new Occupation(code, description);

        Assert.IsTrue(occupation.IsValid);
    }
}