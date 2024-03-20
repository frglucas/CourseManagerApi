using CourseManagerApi.Domain.Entities;

namespace CourseManagerApi.Tests.Entities;

[TestClass]
public class TenantTests
{
    [TestMethod]
    [TestCategory("Entities - Tenant")]
    [DataRow("I")]
    [DataRow("A name for a tenant that is much longer than 128 characters and at the end of an error when testing the entity in unit tests and others")]
    public void ShouldReturnErrorWhenInvalidParams(string name)
    {
        var tenant = new Tenant(name);

        Assert.IsFalse(tenant.IsValid);
    }
    
    [TestMethod]
    [TestCategory("Entities - Tenant")]
    [DataRow("Software Company")]
    [DataRow("Other Company")]
    public void ShouldReturnSuccessWhenValidParams(string name)
    {
        var tenant = new Tenant(name);

        Assert.IsTrue(tenant.IsValid);
    }
}